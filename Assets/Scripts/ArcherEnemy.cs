using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ArcherEnemy : Enemy
{
    [SerializeField] private Transform _shootPosition;

    [SerializeField] private float _shootForce; //TODO colocar scriptable diferente entre os inimigos

    [SerializeField] private ObjectsTag _arrow;
    
    private GameObject[] _waypoints;    

    private NavMeshObstacle _navMeshObstacle;

    private NavMeshPath _navMeshPath;

    private Transform _closestWaypoint; 

    private bool _pathAvailable;

    
    protected override void Update() 
    {
        base.Update();

        if(!_isWalking)
        {  
            Patrol();
        }
        else
        {
            Walking(); 
        }
    }

    protected override void OnEnable() //reset settings to default to reused the object in the object pooler
    {
        _isDead = false;
        _canMove = true;
        _isAttacking = false;
        _isWalking = true;
        _health =  _enemyBalancer.health;
        _navMeshObstacle.enabled = false;
        _navMeshAgent.enabled = true;
        _collider.enabled = true;

        if(_navMeshAgent.isStopped)
        {
            _navMeshAgent.isStopped = false;
        }

        FindWaypoint();
    }

    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);
        
        if(!_isDead)
        {
            _animator.SetTrigger(EnemyAnimationParameters.DAMAGE);
        }
    }

    protected override void Initialize()
    {
        base.Initialize();

        _waypoints = GameObject.FindGameObjectsWithTag("waypoints"); //TODO ARRUMAR !!!!!!!!!!!!! IMPORTANTE
        
        _navMeshPath = new NavMeshPath();
        _navMeshObstacle = GetComponent<NavMeshObstacle>();
        //FindClosestWaypoint();
    }

    protected override void Patrol()
    { 
        gameObject.transform.LookAt(_attackTarget.transform.position);
        _distance = Vector3.Distance(_attackTarget.transform.position, transform.position);

        if(_distance <=  _enemyBalancer.attackDistance && _canAttack)
        {
            Attack();
        }
    }

    protected override void Attack()  
    {   
        base.Attack(); 
        _animator.SetTrigger(EnemyAnimationParameters.ATTACK); 
    }

    protected override void Death()  
    { 
        base.Death();
        _navMeshAgent.isStopped = true;
       //TODO avoid archer to walk after death
        
        OnEnemyDeath?.Invoke(ObjectsTag.ArcherEnemy);
        _animator.SetTrigger(EnemyAnimationParameters.DEATH);
    }

    private void Walking()
    {      
        if(_isWalking)
        {
            if(_navMeshAgent.remainingDistance < 0.5f)
            {
                _navMeshAgent.isStopped = true;
                _navMeshAgent.enabled = false;
                _navMeshObstacle.enabled = true;
                _isWalking = false;

                _animator.SetBool(EnemyAnimationParameters.ISRUNNING, false);
            }

            if(_isWalking)
            {
                _pathAvailable = _navMeshAgent.CalculatePath(_closestWaypoint.position, _navMeshPath);

                if(!_pathAvailable)
                {
                    FindWaypoint();
                }
            }
        }
    }

    private void FindWaypoint()
    {
        float currentWaypointDistance;
        float closestWaypointDistance = 0;

        for(int i = 0; i < _waypoints.Length; i++)
        {
            currentWaypointDistance = Vector3.Distance(_waypoints[i].transform.position, transform.position);

            bool isPathAvailable = _navMeshAgent.CalculatePath(_waypoints[i].transform.position, _navMeshPath);

            if(closestWaypointDistance == 0)
            {
                if(isPathAvailable)
                {
                    _closestWaypoint = _waypoints[i].transform;
                    closestWaypointDistance = currentWaypointDistance;
                }
            }
            else if(currentWaypointDistance < closestWaypointDistance)
            {   
                if(isPathAvailable)
                {
                    _closestWaypoint = _waypoints[i].transform;
                    closestWaypointDistance = currentWaypointDistance;
                }
            }
        }

        _navMeshAgent.SetDestination(_closestWaypoint.position);
        _animator.SetBool(EnemyAnimationParameters.ISRUNNING, true);
    }

    private void Shoot() //animation event
    {
        GameObject obj = _objectPooler.SpawnFromPool(_arrow);
        obj.transform.position = _shootPosition.transform.position;
        obj.transform.rotation = _shootPosition.transform.rotation; 

        Rigidbody rb = obj.GetComponent<Rigidbody>();
        rb.velocity = _shootPosition.transform.forward * _shootForce;
    }
}
