using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowCollectable : Collectable
{
    public delegate void CollectArrowHandler(ObjectsTag arrowType, float arrowAmount);
    public static CollectArrowHandler OnCollectArrow;

    [SerializeField] private float _arrowAmount;

    [SerializeField] private ObjectsTag _arrowType;

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
    }

}