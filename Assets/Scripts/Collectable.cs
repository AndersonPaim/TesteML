using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Collectable : MonoBehaviour
{
    [SerializeField] private AudioClip _collectAudio;
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
        _collider.enabled = true;
    }
 
    protected abstract void OnTriggerEnter(Collider other);

    protected virtual void CollectItem(GameObject obj)
    {
        _collider.enabled = false;
        gameObject.SetActive(false);
    }

}
