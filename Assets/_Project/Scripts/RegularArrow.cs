using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegularArrow : Arrows
{   
    [SerializeField] private SoundEffect _impactAudio;
    [SerializeField] private SoundEffect _damageAudio;

    private void OnEnable()
    {
        _collider.enabled = true;
        _rb.isKinematic = false;
        _rb.collisionDetectionMode = CollisionDetectionMode.Continuous;
    }

    protected override void Initialize()
    {
        base.Initialize();
    }

    protected override void OnCollisionEnter(Collision other) 
    {
        _collider.enabled = false;
        _rb.collisionDetectionMode = CollisionDetectionMode.ContinuousSpeculative; //change collision detection to enable isKinematic, ContinuousSpeculative doesn't look good in game
        _rb.isKinematic = true;

        IDamageable damageable = other.gameObject.GetComponent<IDamageable>();

        if(damageable != null)
        {
            HitParticle();
            Damage(damageable); 
            StartCoroutine(DisableObject(0)); //disable object instantly when hit an enemy
            AudioController.sInstance.PlayAudio(_damageAudio, transform.position);
        }
        else
        {
            StartCoroutine(DisableObject(_destroyDelay)); //disable object with delay when hit other things
            AudioController.sInstance.PlayAudio(_impactAudio, transform.position);
        }
    }

    protected override IEnumerator DisableObject(float time)
    {
        yield return new WaitForSeconds(time);
        gameObject.SetActive(false);
    }

    private void HitParticle()
    {
        GameObject obj = _objectPooler.SpawnFromPool(ObjectsTag.HitParticle);
        obj.transform.position = transform.position;
    }
}
