using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingText : MonoBehaviour
{

    [SerializeField] private float _disableTime;

    public void Initialize()
    {
        StartCoroutine(DisableObject());
    }
    private IEnumerator DisableObject()
    {
        yield return new WaitForSeconds(_disableTime);
        gameObject.SetActive(false);
    }
}
