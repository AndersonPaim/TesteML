using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageText : MonoBehaviour
{
    [SerializeField] private float _disableTime;

    private void OnEnable() 
    {
        StartCoroutine(DisableObject());
    }
    private IEnumerator DisableObject()
    {
        yield return new WaitForSeconds(_disableTime);
        gameObject.SetActive(false);
    }
}
