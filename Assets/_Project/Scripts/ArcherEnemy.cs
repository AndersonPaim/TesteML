using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ArcherEnemy : Enemy
{
    [SerializeField] private Transform _shootPosition;

    [SerializeField] private float _shootForce; 
    [SerializeField] private ObjectsTag _arrow;
    
    private List<Transform> _waypoints;    

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
        _canAttack = true;
        _isAttacking = false;
        _isWalking = true;
        _health =  _enemyBalancer.health;
        _collider.enabled = true;
        _navMeshObstacle.enabled = false;
        _navMeshAgent.enabled = true;
        _navMeshAgent.speed = _enemyBalancer.speed;

        FindWaypoint();
    }

    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);
        
        if(!_isDead)
        {
            _animator.SetTrigger(EnemyAnimationParameters.TakeDamage);
        }
    }

    protected override void Initialize()
    {
        base.Initialize();
        
        _waypoints = GameManager.sInstance.waypoints;
        _navMeshPath = new NavMeshPath();
        _navMeshObstacle = GetComponent<NavMeshObstacle>();
    }

    protected override void Patrol()
    { 
        Vector3 target = new Vector3(_attackTarget.transform.position.x, transform.position.y, _attackTarget.transform.position.z);
        gameObject.transform.LookAt(target);
        
        _distance = Vector3.Distance(_attackTarget.transform.position, transform.position);

        if(_distance <=  _enemyBalancer.attackDistance && _canAttack)
        {
            Attack();
        }
    }

    protected override void Attack()  
    {   
        base.Attack(); 
        _animator.SetTrigger(EnemyAnimationParameters.Attack); 
    }

    protected override void Death()  
    { 
        base.Death();
        _navMeshAgent.speed = 0;    //avoid archer to walk after death

        OnDeath?.Invoke(ObjectsTag.ArcherEnemy);
        _animator.SetTrigger(EnemyAnimationParameters.Death);
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

                _animator.SetBool(EnemyAnimationParameters.IsRunning, false);
            }

            if(_isWalking)
            {
                _pathAvailable = _navMeshAgent.CalculatePath(_closestWaypoint.position, _navMeshPath);

                if(!_pathAvailable) //if waypoints is obstructed with another enemy finds another waypoint
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

        for(int i = 0; i < _waypoints.Count; i++) //Find an available waypoint in the waipoints list
        {
            currentWaypointDistance = Vector3.Distance(_waypoints[i].transform.position, transform.position);

            bool isPathAvailable = _navMeshAgent.CalculatePath(_waypoints[i].transform.position, _navMeshPath);

            if(closestWaypointDistance == 0) //first waypoint tested
            {
                if(isPathAvailable)
                {
                    _closestWaypoint = _waypoints[i].transform;
                    closestWaypointDistance = currentWaypointDistance;
                }
            }
            else if(currentWaypointDistance < closestWaypointDistance) //compare to others waypoints to find the closest path
            {   
                if(isPathAvailable)
                {
                    _closestWaypoint = _waypoints[i].transform;
                    closestWaypointDistance = currentWaypointDistance;
                }
            }
        }

        _navMeshAgent.SetDestination(_closestWaypoint.position);
        _animator.SetBool(EnemyAnimationParameters.IsRunning, true);
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