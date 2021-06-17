using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarriorEnemy : Enemy
{
    [Tooltip("Cooldown to move after attack")]
    [SerializeField] protected float _movementCooldown;
    protected override void Update() 
    {
        base.Update();
    }

    protected override void OnEnable() 
    {
        _health =  _enemyBalancer.health;
        _isAttacking = false;
        _canMove = true;
        _isDead = false;
        _collider.enabled = true;
        _navMeshAgent.isStopped = false; 
    }  //TODO COLOCAR ABSTRACT PRA TODOS DPS

    protected override void Initialize()
    {
        base.Initialize();

        _navMeshAgent.stoppingDistance = _enemyBalancer.attackDistance;
    }

    protected override void Movement()
    {        
        if(_distance >= _enemyBalancer.attackDistance && _canMove)
        {
            _animator.SetBool(EnemyAnimationParameters.ISRUNNING, true);
            _navMeshAgent.SetDestination(_attackTarget.transform.position);
        }
    }

    protected override void Patrol()
    {
        _distance = Vector3.Distance(_attackTarget.transform.position, transform.position);
        
        if(_distance <= _enemyBalancer.attackDistance && !_isAttacking && !_isDead)
        {
            Attack();
        }
    }

    protected override void Attack()  
    {   
        base.Attack(); 

        _animator.SetTrigger(EnemyAnimationParameters.ATTACK);
        _animator.SetBool(EnemyAnimationParameters.ISRUNNING, false);
        _canMove = false;
        StartCoroutine(MovementCooldown());
        Debug.Log("ATACOU");
        Damage(_attackTarget.GetComponent<IDamageable>());     
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
        _navMeshAgent.isStopped = true; 
        _canMove = false;
        _animator.SetTrigger(EnemyAnimationParameters.DEATH);
        OnEnemyDeath?.Invoke(objectsTag.WarriorEnemy);
    }

    private IEnumerator MovementCooldown()
    {
        yield return new WaitForSeconds(_movementCooldown);
        _canMove = true;
    }

}