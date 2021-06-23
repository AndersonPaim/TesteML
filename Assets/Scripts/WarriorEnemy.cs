using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarriorEnemy : Enemy
{
    [Tooltip("Cooldown to move after attack")]
    [SerializeField] protected float _movementCooldown;
    [SerializeField] protected float _attackRange; //distance at which the attack deals damage

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
            _animator.SetTrigger(EnemyAnimationParameters.DAMAGE);
        }
    }

    protected override void Initialize()
    {
        base.Initialize();
    }

    protected override void Patrol()
    {
        if(_distance >= _enemyBalancer.attackDistance && _canMove)
        {
            _animator.SetBool(EnemyAnimationParameters.ISRUNNING, true);
            _navMeshAgent.SetDestination(_attackTarget.transform.position);
            _isWalking = true;
        }
        else
        {
            _animator.SetBool(EnemyAnimationParameters.ISRUNNING, false);
            _isWalking = false;
        }

        _distance = Vector3.Distance(_attackTarget.transform.position, transform.position);
        
        if(_distance <= _enemyBalancer.attackDistance && _canAttack && !_isDead)
        {
            Attack();
        }
    }

    protected override void Attack()  
    {   
        base.Attack(); 

        _animator.SetTrigger(EnemyAnimationParameters.ATTACK);
        _canMove = false;
        StartCoroutine(MovementCooldown());
    }

    protected override void Death()  
    { 
        base.Death();
        _navMeshAgent.isStopped = true; 
        _canMove = false;
        _animator.SetTrigger(EnemyAnimationParameters.DEATH);
        OnEnemyDeath?.Invoke(ObjectsTag.WarriorEnemy);
    }

    private IEnumerator MovementCooldown()
    {
        yield return new WaitForSeconds(_movementCooldown);
        _canMove = true;
    }
    private void Hit() //animation event
    {
        if(_distance <= _attackRange)
        {
            _hasHittedPlayer = true;
            Damage(_attackTarget.GetComponent<IDamageable>());     
        }
    }

}