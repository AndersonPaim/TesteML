using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPotionCollectable : Collectable
{
    [SerializeField] private float _healthAmount;

    private IHealable _healable;

    private IInjured _injured;

    protected override void OnTriggerEnter(Collider other)
    {
        _healable = other.gameObject.GetComponent<IHealable>();  
        _injured = other.gameObject.GetComponent<IInjured>();

        if(_healable != null) 
        {
            if(CanHeal(_injured)) //check if the player is not full heath
            {
                CollectItem(other.gameObject);
            }
        }
    }

    protected override void CollectItem(GameObject obj)
    {
        base.CollectItem(obj);

        Heal(_healable); 
    }

    private void Heal(IHealable ihealable)
    {
        ihealable.ReceiveHealing(_healthAmount);
    }

    private bool CanHeal(IInjured injured)
    {
        if(injured.IsInjured())
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}