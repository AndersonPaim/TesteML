using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveArrow : Arrows
{
    [SerializeField] private ParticleSystem _explosionParticle;
    
    [SerializeField] private SoundEffect _soundEffect;

    [SerializeField] private float _blastRadius;

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
        DisableObject(0);

        AudioController.sInstance.PlayAudio(_soundEffect, transform.position);
        //create damage area
        Collider[] colliders = Physics.OverlapSphere(transform.position, _blastRadius);
        //find for damageable entities in area
        foreach (Collider enemies in colliders)
        {
            IDamageable damageable = enemies.GetComponent<IDamageable>();

            if(damageable != null)
            {
                Damage(damageable); 
            }
        }

        _explosionParticle.Play();
    }

    protected override IEnumerator DisableObject(float time)
    {
        yield return new WaitForEndOfFrame();

        gameObject.SetActive(false);
    }
    
}
