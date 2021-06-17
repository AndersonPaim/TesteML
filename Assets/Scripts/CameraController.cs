using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraController : MonoBehaviour
{
    public delegate void CameraRotateHandler(float yRot, float xRot);
    public CameraRotateHandler OnCameraRotate;

    [SerializeField] private float _sensibility;
    [SerializeField] private float _maxVerticalRot;
    [SerializeField] private float _minVerticalRot;

    [SerializeField] private GameObject _player;
    
    private bool _isPaused = false;

    private float _verticalAxis;
    private void Start()
    {
        Initialize();
        SetupDelegates();
    }

    private void Update() 
    {
        
    }

    private void OnDestroy() 
    {
        RemoveDelegates();
    }

    private void Initialize()
    {
       //
    }


    private void SetupDelegates()
    {
        GameManager.sInstance.InGameMenus.OnPause += Pause;
        GameManager.sInstance.InputListener.OnInput += ReceiveInputs;
    }

    private void RemoveDelegates()
    {
        GameManager.sInstance.InGameMenus.OnPause -= Pause;
        GameManager.sInstance.InputListener.OnInput -= ReceiveInputs;
    }

    private void Pause(bool isPaused)
    {
        _isPaused = isPaused;
    }

    private void ReceiveInputs(InputData inputData)
    {
        if (!_isPaused)
        {
            CamMovement(inputData.LookX, inputData.LookY);
        }
    }

    private void CamMovement(float lookX, float lookY)
    {
        float mouseX = lookX * _sensibility * Time.deltaTime;
        float mouseY = lookY * -_sensibility * Time.deltaTime;

        if(_player.transform.rotation.eulerAngles.x + mouseY > _maxVerticalRot|| _player.transform.rotation.eulerAngles.x + mouseY < _minVerticalRot) 
        {
            OnCameraRotate?.Invoke(mouseX, mouseY);
        }
    }
}
