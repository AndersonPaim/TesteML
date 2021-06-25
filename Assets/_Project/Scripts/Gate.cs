using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : MonoBehaviour
{
    [SerializeField] SoundEffect _openAudio;

    private Animation _animation;

    private void Start()
    {
        SetupDelegates();
        Initialize();
    }

    private void OnDestroy() 
    {
        RemoveDelegates();
    }

    private void Initialize()
    {
        _animation = GetComponent<Animation>();
    }

    private void SetupDelegates()
    {
        GameManager.sInstance.Timer.OnFinishCountdownTimer += OpenGate;
    }

    private void RemoveDelegates()
    {
        GameManager.sInstance.Timer.OnFinishCountdownTimer -= OpenGate;
    }
    
    private void OpenGate()
    {
        _animation.Play();

        AudioController.sInstance.PlayAudio(_openAudio, transform.position);
    }
}
