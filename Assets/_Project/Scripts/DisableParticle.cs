using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DisableParticle : MonoBehaviour 
{
    [SerializeField] private ParticleSystem _particle;
    private void OnEnable() 
    {
        StartCoroutine(Disable(_particle.main.duration));
    }

    private IEnumerator Disable(float time)
    {
        yield return new WaitForSeconds(time);
        gameObject.SetActive(false);
    }
}