using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegularArrow : Arrows
{   
    private void OnEnable() //TODO AJEITAR
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

        if(other.gameObject.GetComponent<IDamageable>() != null)
        {
            Damage(other.gameObject.GetComponent<IDamageable>()); 
            StartCoroutine(DisableObject(0)); //disable object instantly when hit an enemy
        }
        else
        {
            StartCoroutine(DisableObject(_destroyDelay));
        }
    }

    protected override IEnumerator DisableObject(float time)
    {
        yield return new WaitForSeconds(time);
        gameObject.SetActive(false);
    }
    
}
