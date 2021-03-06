using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyArrow : Arrows
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
            Damage(damageable); 
            AudioController.sInstance.PlayAudio(_damageAudio, transform.position);
            StartCoroutine(DisableObject(0)); //disable object instantly when hit an enemy
        }
        else
        {
            AudioController.sInstance.PlayAudio(_impactAudio, transform.position);
            StartCoroutine(DisableObject(_destroyDelay));
        }
    }

    protected override IEnumerator DisableObject(float time)
    {
        yield return new WaitForSeconds(time);
        gameObject.SetActive(false);
    }
}
