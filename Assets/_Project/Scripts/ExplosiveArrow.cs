using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveArrow : Arrows
{
    public delegate void ArrowHitHandler(Vector3 pos);
    public static ArrowHitHandler OnArrowHit;

    [SerializeField] private ParticleSystem _explosionParticle;

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
        OnArrowHit?.Invoke(transform.position);
        DisableObject(0);

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
