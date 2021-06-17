using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ArcherEnemy : Enemy
{
    [SerializeField] private Transform _shootPosition;

    [SerializeField] private float _shootForce; //TODO colocar scriptable diferente entre os inimigos

    [SerializeField] private objectsTag _arrow;
    
    private GameObject[] _waypoints;    

    private NavMeshObstacle _navMeshObstacle;

    private NavMeshPath _navMeshPath;

    private Transform _closestWaypoint; 

    private bool _pathAvailable;
    private bool _isMoving = true;
    
    protected override void Update() 
    {
        base.Update();
    }

    protected override void OnEnable() //reset settings to default to reused the object in the object pooler
    {
        _isDead = false;
        _canMove = true;
        _isAttacking = false;
        _isMoving = true;
        _health =  _enemyBalancer.health;
        _navMeshObstacle.enabled = false;
        _navMeshAgent.isStopped = false; //TODO VER SE PRECISA
        _navMeshAgent.enabled = true;
        FindClosestWaypoint();
    }

    protected override void Initialize()
    {
        base.Initialize();

        _waypoints = GameObject.FindGameObjectsWithTag("waypoints"); //TODO ARRUMAR !!!!!!!!!!!!! IMPORTANTE
        
        _navMeshPath = new NavMeshPath();
        _navMeshObstacle = GetComponent<NavMeshObstacle>();
        //FindClosestWaypoint();
    }

    protected override void Movement()
    {      
        if(_isMoving)
        {
            if(_navMeshAgent.remainingDistance < 0.5f)
            {
                _navMeshAgent.isStopped = true;
                _navMeshAgent.enabled = false;
                _navMeshObstacle.enabled = true;
                _isMoving = false;

                _animator.SetBool(EnemyAnimationParameters.ISRUNNING, false);
            }

            if(_isMoving)
            {
                _pathAvailable = _navMeshAgent.CalculatePath(_closestWaypoint.position, _navMeshPath);

                if(!_pathAvailable)
                {
                    FindClosestWaypoint();
                }
            }
        }
    }

    protected override void Patrol()
    {
        if(!_isMoving)
        {   
            gameObject.transform.LookAt(_attackTarget.transform.position);
            _distance = Vector3.Distance(_attackTarget.transform.position, transform.position);

            if(_distance <=  _enemyBalancer.attackDistance && !_isAttacking)
            {
                Attack();
            }
        }
    }

    private void FindClosestWaypoint()
    {
        float currentWaypointDistance;
        float closestWaypointDistance = 0;

        for(int i = 0; i < _waypoints.Length; i++)
        {
            
            currentWaypointDistance = Vector3.Distance(_waypoints[i].transform.position, transform.position);

            bool pathAvailable = _navMeshAgent.CalculatePath(_waypoints[i].transform.position, _navMeshPath);

            if(closestWaypointDistance == 0)
            {
                if(pathAvailable)
                {
                    _closestWaypoint = _waypoints[i].transform;
                    closestWaypointDistance = currentWaypointDistance;
                }
            }
            else if(currentWaypointDistance < closestWaypointDistance)
            {   
                if(pathAvailable)
                {
                    _closestWaypoint = _waypoints[i].transform;
                    closestWaypointDistance = currentWaypointDistance;
                }
            }
        }

        _navMeshAgent.SetDestination(_closestWaypoint.position);
        _animator.SetBool(EnemyAnimationParameters.ISRUNNING, true);
    }

    protected override void Attack()  
    {   
        base.Attack(); 
        _animator.SetTrigger(EnemyAnimationParameters.ATTACK); 
    }

    private void Shoot() //animation event
    {
        GameObject obj = _objectPooler.SpawnFromPool(_arrow);
        obj.transform.position = _shootPosition.transform.position;
        obj.transform.rotation = _shootPosition.transform.rotation; 

        Rigidbody rb = obj.GetComponent<Rigidbody>();
        rb.velocity = _shootPosition.transform.forward * _shootForce;
    }

    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);
        
        if(!_isDead)
        {
            _animator.SetTrigger(EnemyAnimationParameters.DAMAGE);
        }
    }

    protected override void Death()  
    { 
        base.Death();

        if(_isMoving)
        {
            _navMeshAgent.isStopped = true; //avoid archer to walk after death
        }

        OnEnemyDeath?.Invoke(objectsTag.ArcherEnemy);
        _animator.SetTrigger(EnemyAnimationParameters.DEATH);
    }
}
