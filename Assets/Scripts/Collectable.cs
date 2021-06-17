using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Collectable : MonoBehaviour
{
    [SerializeField] protected float _disableDelay;

    private Collider _collider;

    public virtual void Awake() 
    {
        Initialize();
    }

    protected virtual void Initialize()
    {
        _collider = GetComponent<Collider>();
    }

    protected virtual void OnEnable()
    {
        Collider.enabled = true;
    }

    //rotected abstract void CollectItem();
 
    protected abstract void OnTriggerEnter(Collider other);

    protected virtual void CollectItem(GameObject obj)
    {
        //CollectItem();
        Collider.enabled = false;
    }

    protected abstract IEnumerator DisableObject();

    public Collider Collider
    {
        get { return _collider; }
    }
}
