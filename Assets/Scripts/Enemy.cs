using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class Enemy : MonoBehaviour, IDamageable
{
    public delegate void EnemyDeathHandle(objectsTag enemyTag);
    public static EnemyDeathHandle OnEnemyDeath;

    [SerializeField] protected EnemyBalancer _enemyBalancer;

    [SerializeField] protected GameObject _armor;

    protected float _health;
    protected float _distance;

    protected GameObject _attackTarget;

    protected Collider _collider;

    protected NavMeshAgent _navMeshAgent;

    protected Animator _animator;

    protected ObjectPooler _objectPooler;

    protected bool _isAttacking = false;
    protected bool _canMove = true;
    protected bool _isDead = false;

    protected void Awake()
    {
        Initialize(); 
    }

    protected virtual void Update()
    {
        Movement();
        Patrol();
    }

    protected abstract void OnEnable();

    protected virtual void Initialize()
    {
        _health = _enemyBalancer.health;
        _objectPooler = GameManager.sInstance.ObjectPooler;
        _animator = GetComponent<Animator>();
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _collider = GetComponent<Collider>();
        _navMeshAgent.speed = _enemyBalancer.speed;
        _armor.GetComponent<SkinnedMeshRenderer>().material = _enemyBalancer.armorTexture;
        _attackTarget = GameManager.sInstance.Player;
        //_objectPooler = GameManager.sInstance.GetObjectPooler();
    }

    protected abstract void Movement();

    protected abstract void Patrol();

    protected virtual void Attack()
    {
        _isAttacking = true;

        StartCoroutine(AttackCooldown());
    }
      
    protected IEnumerator AttackCooldown()
    {
        yield return new WaitForSeconds(_enemyBalancer.attackCooldown);
        _isAttacking = false;
    }
    
    protected void Damage(IDamageable idamageable)
    {
        idamageable.TakeDamage(_enemyBalancer.damage);
    }

    public virtual void TakeDamage(float damage)
    {
        _health -= damage;

        /*GameObject obj = _objectPooler.SpawnFromPool(7);

        obj.transform.position = transform.position;
        obj.transform.LookAt(_player.transform.position);
        obj.GetComponentInChildren<TextMesh>().text = damage.ToString();
        obj.GetComponent<FloatingText>().Initialize();*/

        if(_health <= 0 && !_isDead)
        {
            _isDead = true;
            Death();
        }
    }

    protected virtual void Death()
    {
        _collider.enabled = false;
        StartCoroutine(DisableObject());
        DropItem();
    }

    public virtual void DropItem()
    {
        int dropRandom = Random.Range(1, 100);

        if(dropRandom <= _enemyBalancer.dropChance)
        {
            int randomItem = Random.Range(0, _enemyBalancer.itensDrop.Length);
            GameObject obj = _objectPooler.SpawnFromPool(_enemyBalancer.itensDrop[randomItem]);
            obj.transform.position = gameObject.transform.position;
        }

    }

    protected IEnumerator DisableObject()
    {
        yield return new WaitForSeconds(_enemyBalancer.destroyDelay);
        gameObject.SetActive(false);
    }
}