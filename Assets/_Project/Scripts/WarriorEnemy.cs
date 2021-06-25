using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarriorEnemy : Enemy
{
    [Tooltip("Cooldown to move after attack")]
    [SerializeField] protected float _movementCooldown;

    [Tooltip("distance at which the attack deals damage")]
    [SerializeField] protected float _attackRange; 

    protected override void Update() 
    {
        base.Update();
        Patrol();
    }

    protected override void OnEnable() 
    {
        _health = _enemyBalancer.health;
        _isAttacking = false;
        _canMove = true;
        _isDead = false;
        _collider.enabled = true;
        _navMeshAgent.isStopped = false; 
    }

    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);
        
        if(!_isDead)
        {
            _animator.SetTrigger(EnemyAnimationParameters.TakeDamage);
        }
    }

    protected override void AwakeInitialize()
    {
        base.AwakeInitialize();
    }

    protected override void Patrol()
    {
        if(_distance >= _enemyBalancer.attackDistance && _canMove) //follow player
        {
            _animator.SetBool(EnemyAnimationParameters.IsRunning, true);
            _navMeshAgent.SetDestination(_attackTarget.transform.position);
            _isWalking = true;
        }
        else 
        {
            _animator.SetBool(EnemyAnimationParameters.IsRunning, false);
            _isWalking = false;
        }

        _distance = Vector3.Distance(_attackTarget.transform.position, transform.position);
        
        if(_distance <= _enemyBalancer.attackDistance && _canAttack && !_isDead) //attack range
        {
            Attack();
        }
    }

    protected override void Attack()  
    {   
        base.Attack(); 

        _animator.SetTrigger(EnemyAnimationParameters.Attack);
        _canMove = false; 
        StartCoroutine(MovementCooldown());
    }

    protected override void Death()  
    { 
        base.Death();
        _navMeshAgent.isStopped = true; 
        _canMove = false;
        _animator.SetTrigger(EnemyAnimationParameters.Death);
        OnDeath?.Invoke(ObjectsTag.WarriorEnemy);
    }

    private void Hit() //run on warrior attack animation event
    {
        if(_distance <= _attackRange)
        {
            _hasHittedPlayer = true;
            Damage(_attackTarget.GetComponent<IDamageable>());     
        }
    }

    private IEnumerator MovementCooldown() //movement cooldown after attack
    {
        yield return new WaitForSeconds(_movementCooldown);
        _canMove = true;
    }

}