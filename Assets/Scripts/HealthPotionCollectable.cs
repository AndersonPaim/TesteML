using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPotionCollectable : Collectable
{
    [SerializeField] private float _healthAmount;

    protected override void Initialize()
    {
        base.Initialize();
    }

    protected override void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.GetComponent<IHealable>() != null)
        {
            CollectItem(other.gameObject);
        }
    }

    protected override void CollectItem(GameObject obj)
    {
        base.CollectItem(obj);

        Heal(obj.GetComponent<IHealable>()); 
        StartCoroutine(DisableObject());
    }

    private void Heal(IHealable ihealable)
    {
        ihealable.ReceiveHealing(_healthAmount);
    }

    protected override IEnumerator DisableObject()
    {
        yield return new WaitForSeconds(_disableDelay);
        gameObject.SetActive(false);
    }

}