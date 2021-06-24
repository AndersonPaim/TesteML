using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegularArrow : Arrows
{   
    private void OnEnable()
    {
        _collider.enabled = true;
        _rb.isKinematic = false;
    }
    protected override void Initialize()
    {
        base.Initialize();
    }

    protected override void OnCollisionEnter(Collision other) 
    {
        _collider.enabled = false;
        _rb.isKinematic = true;

        IDamageable damageable = other.gameObject.GetComponent<IDamageable>();

        if(damageable != null)
        {
            Damage(damageable); 
            StartCoroutine(DisableObject(0)); //disable object instantly when hit an enemy
        }
        else
        {
            StartCoroutine(DisableObject(_destroyDelay)); //disable object with delay when hit other things
        }
    }

    protected override IEnumerator DisableObject(float time)
    {
        yield return new WaitForSeconds(time);
        gameObject.SetActive(false);
    }
    
}
