using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Arrows : MonoBehaviour
{
    [SerializeField] protected float _damage;
    [SerializeField] protected float _destroyDelay;

    protected Rigidbody _rb;

    protected Collider _collider;

    protected ObjectPooler _objectPooler;
    
    protected void Awake()
    {
        Initialize(); 
    }

    protected virtual void Initialize()
    {
        _rb = GetComponent<Rigidbody>();
        _collider = GetComponent<Collider>();
        _objectPooler = GameManager.sInstance.ObjectPooler;
    }

    protected abstract void OnCollisionEnter(Collision other);

    protected void Damage(IDamageable idamageable)
    {   
        idamageable.TakeDamage(_damage);
    }

    protected abstract IEnumerator DisableObject(float time);

}
