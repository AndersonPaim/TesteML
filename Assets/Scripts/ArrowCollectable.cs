using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowCollectable : Collectable
{
    public delegate void CollectArrowHandler(objectsTag arrowType, float arrowAmount);
    public static CollectArrowHandler OnCollectArrow;

    [SerializeField] private float _arrowAmount;

    [SerializeField] private objectsTag _arrowType;

    protected override void Initialize()
    {
        base.Initialize();
    }

    protected override void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.GetComponent<IHealable>() != null) //TODO ajeitar essa interface
        {
            CollectItem(other.gameObject);
        }
    }

    protected override void CollectItem(GameObject obj)
    {
        base.CollectItem(obj);

        OnCollectArrow?.Invoke(_arrowType, _arrowAmount);

        StartCoroutine(DisableObject());
    }

    protected override IEnumerator DisableObject()
    {
        yield return new WaitForSeconds(_disableDelay);
        gameObject.SetActive(false);
    }

}