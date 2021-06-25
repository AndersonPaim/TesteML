using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using TMPro;

public abstract class Enemy : MonoBehaviour, IDamageable
{
    public delegate void EnemyDeathHandle(ObjectsTag enemyTag);
    public static EnemyDeathHandle OnDeath;

    public delegate void EnemyKillPoints(float points);
    public static EnemyKillPoints OnKill;
    
    public delegate void EnemyDataHandler(EnemyData enemyData);
    public EnemyDataHandler OnUpdateEnemyData;

    public delegate void EnemyDamageHandler();
    public EnemyDamageHandler OnTakeDamage;

    [SerializeField] protected EnemyBalancer _enemyBalancer;

    [SerializeField] protected GameObject _armor;

    [SerializeField] protected float _killPoints;

    protected float _health;
    protected float _distance;

    protected GameObject _attackTarget;

    protected Collider _collider;

    protected NavMeshAgent _navMeshAgent;

    protected Animator _animator;

    protected ObjectPooler _objectPooler;

    protected EnemyData _enemyData;

    protected bool _isAttacking = false;
    protected bool _isTakingDamage = false;
    protected bool _isWalking = true;
    protected bool _hasHittedPlayer = false;
    protected bool _canMove = true;
    protected bool _canAttack = true;
    protected bool _isDead = false;

    protected void Awake()
    {
        Initialize(); 
    }

    private void Start() 
    {
        StartInitialize();
    }

    protected virtual void Update()
    {
        CreateEnemyStruct();
    }
    public virtual void TakeDamage(float damage)
    {
        _health -= damage;

        GameObject obj = _objectPooler.SpawnFromPool(ObjectsTag.DamageText);

        obj.transform.position = transform.position;
        obj.transform.LookAt(_attackTarget.transform.position);
        obj.GetComponentInChildren<TextMeshPro>().text = damage.ToString();

        OnTakeDamage?.Invoke();

        if(_health <= 0 && !_isDead)
        {
            Death();
        }
        else
        {
            _isTakingDamage = true;
        }
    }

    protected abstract void OnEnable();

    protected abstract void Patrol();

    protected void Damage(IDamageable idamageable)
    {
        idamageable.TakeDamage(_enemyBalancer.damage);
    }

    protected virtual void Initialize()
    {
        _animator = GetComponent<Animator>();
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _collider = GetComponent<Collider>();
        _health = _enemyBalancer.health;
        _navMeshAgent.speed = _enemyBalancer.speed;
        RandomSkin();
    }

    protected virtual void StartInitialize()
    {
        _objectPooler = GameManager.sInstance.ObjectPooler;
        _attackTarget = GameManager.sInstance.Player;
    }

    protected virtual void CreateEnemyStruct()
    {
        _enemyData = new EnemyData();

        _enemyData.Walking = _isWalking;
        _enemyData.Attacking = _isAttacking;
        _enemyData.HittedPlayer = _hasHittedPlayer;

        if(_isTakingDamage)
        {
            _isTakingDamage = false;
        }
        if(_isAttacking)
        {
            _isAttacking = false;
        }
        if(_hasHittedPlayer)
        {
            _hasHittedPlayer = false;
        }

        OnUpdateEnemyData?.Invoke(_enemyData);
    }

    protected virtual void Attack()
    {
        _isAttacking = true;
        _canAttack = false;

        StartCoroutine(AttackCooldown());
    }

    protected virtual void Death()
    {
        OnKill?.Invoke(_killPoints);
        _isDead = true;
        _collider.enabled = false;
        StartCoroutine(DisableObject());
        DropItem();
    }

    protected virtual void DropItem()
    {
        int dropRandom = Random.Range(1, 100);

        if(dropRandom <= _enemyBalancer.dropChance)
        {
            int randomItem = Random.Range(0, _enemyBalancer.itensDrop.Length);
            GameObject obj = _objectPooler.SpawnFromPool(_enemyBalancer.itensDrop[randomItem]);
            obj.transform.position = gameObject.transform.position;
        }
    }
    
    protected IEnumerator AttackCooldown()
    {
        yield return new WaitForSeconds(_enemyBalancer.attackCooldown);
        _canAttack = true;
    }

    protected IEnumerator DisableObject()
    {
        yield return new WaitForSeconds(_enemyBalancer.destroyDelay);
        gameObject.SetActive(false);
    }
    private void RandomSkin()
    {
        int randomSkin = Random.Range(0, _enemyBalancer.armorTexture.Length);
        _armor.GetComponent<SkinnedMeshRenderer>().material = _enemyBalancer.armorTexture[randomSkin];
    }

}