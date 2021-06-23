using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.Audio;


public class PlayerController : MonoBehaviour, IDamageable, IHealable, IInjured
{
    public delegate void PlayerDataHandler(PlayerData playerData);
    public PlayerDataHandler OnPlayerDataUpdate;

    public delegate void PlayerHealthHandler(float currentHealth, float maxHealth);
    public PlayerHealthHandler OnUpdateHealth;

    public delegate void HealingHandler();
    public HealingHandler OnReceiveHealing;

    public delegate void PlayerDeathHandler();
    public PlayerDeathHandler OnPlayerDeath;


    [SerializeField] private PlayerBalancer _playerBalancer;
    [SerializeField] private GameObject _playerPivot;

    [SerializeField] private AudioSource _footstepsAudioSource;

    private AudioMixerGroup _audioMixer;

    //[SerializeField] private AudioClip _jumpAudio;
    //[SerializeField] private AudioClip _walkAudio;
    //[SerializeField] private AudioClip _runAudio;

    //[SerializeField] private AudioMixerGroup _audioMixer;
    private float _ammo;
    private float _speed;
    private float _health;
    private float _maxHealth;
    private float _selectedArrow;

    private Vector2 _movement;

    private bool _isAiming = false;
    private bool _isGrounded = true;
    private bool _isWalking = false;
    private bool _isRunning = false;
    private bool _isJumping = false;
    private bool _isPaused = false;

    private Rigidbody _rb;

    private Vector3 _vel;

    private PlayerData _playerData;
    
    private ObjectPooler _objectPooler;

    private void Start()
    {
        Initialize();
    }


    private void Update()
    {
        GroundCheck();
        CreatePlayerStruct();
        _playerData = new PlayerData();
    }

    private void OnDestroy()
    {
        RemoveDelegates();
    }

    private void SetupDelegates()
    {
        GameManager.sInstance.InputListener.OnInput += ReceiveInputs;
        GameManager.sInstance.CameraController.OnCameraRotate += Rotate;
    }

    private void RemoveDelegates()
    {
        GameManager.sInstance.InputListener.OnInput -= ReceiveInputs;
        GameManager.sInstance.CameraController.OnCameraRotate -= Rotate;
    }

    private void Initialize()
    {
        Cursor.lockState = CursorLockMode.Locked;
        _rb = GetComponent<Rigidbody>();
        _audioMixer = GetComponent<AudioMixerGroup>();
        _playerData = new PlayerData();
        _vel = _rb.velocity;
        SetupDelegates();
        _speed = _playerBalancer.walkSpeed;
        _health = _playerBalancer.health;
        _maxHealth = _playerBalancer.health;
        _objectPooler = GameManager.sInstance.ObjectPooler;
    }


    private void PauseInputs(bool isPaused)
    {
        _isPaused = isPaused;
    }

    private void ReceiveInputs(InputData inputData)
    {
        if (!_isPaused)
        {
            Movement(inputData.Movement);
            Jump(inputData.Jump);
            Run(inputData.Run);
            Aim(inputData.Aim);
        }
    }

    private void CreatePlayerStruct()
    {
        _playerData.Walk = _isWalking;
        _playerData.OnGround = _isGrounded;
        _playerData.Jump = _isJumping;
        _playerData.Ammo = _ammo;
        _playerData.Movement = _movement;
        _playerData.Speed = _speed;

        if(_isJumping)
        {
            _isJumping = false;
        }
      
        OnPlayerDataUpdate?.Invoke(_playerData);
    }

    private void Aim(bool isAiming)
    {
        _isAiming = isAiming;
    }
    
    private void Movement(Vector2 movement)
    {
        _movement = movement;
        SetVelocity(movement.y);

        Vector3 vel = _rb.velocity;
        float yVel = vel.y;

        Vector3 verticalVel = gameObject.transform.rotation * Vector3.forward * movement.y * _speed * _playerBalancer.generalSpeed;
        Vector3 horizontalVel = gameObject.transform.rotation * Vector3.right * movement.x * _playerBalancer.walkSpeed * _playerBalancer.generalSpeed;

        vel = verticalVel + horizontalVel;


        vel.y = yVel;
        _rb.velocity = vel;
    }

    private void SetVelocity(float direction)
    {
        if (direction > 0) //in movement
        {
            if (_isRunning && !_isAiming) //running
            {
                if (_speed < _playerBalancer.runSpeed)
                {
                    _speed += Time.deltaTime * _playerBalancer.acceleration;
                }
            }
            else //walking
            {
                if (_speed <= _playerBalancer.walkSpeed)
                {
                    _speed += Time.deltaTime * _playerBalancer.acceleration;

                    if (_speed > _playerBalancer.walkSpeed)
                    {
                        _speed = _playerBalancer.walkSpeed;
                    }
                }
                else
                {
                    _speed -= Time.deltaTime * _playerBalancer.deceleration;
                }
            }
        }
        else if (direction == 0) //idle
        {
            if (_speed > 0)
            {
                _speed -= Time.deltaTime * _playerBalancer.deceleration;
            }
            else if (_speed <= 0)
            {
                _speed = 0;
            }
        }
        else if(direction < 0) //walking backwards
        {
            _speed = _playerBalancer.walkSpeed;
        }
    }

    public virtual void TakeDamage(float damage)
    {
        _health -= damage;

        OnUpdateHealth?.Invoke(_health, _maxHealth);

        if(_health <= 0)
        {
            OnPlayerDeath?.Invoke();
        }
    }

    public virtual void ReceiveHealing(float heal)
    {        
        if(_health != _maxHealth)
        {
            if(_health + heal >= _maxHealth)
            {
                _health = _maxHealth;
                OnReceiveHealing?.Invoke();
                OnUpdateHealth?.Invoke(_health, _maxHealth);
            }
            else
            {
                _health += heal;
                OnReceiveHealing?.Invoke();
                OnUpdateHealth?.Invoke(_health, _maxHealth);
            }
        }
    }

    public virtual bool IsInjured()
    {
        if(_health != _maxHealth)
        {
            return true;
        }
        else
        {
            return false;
        }

    }

    private void Rotate(float xRot, float yRot)
    {
        gameObject.transform.Rotate(Vector3.up * xRot);
        _playerPivot.transform.Rotate(Vector3.right * yRot);
    }

    private void Run(bool isRunning)
    {
        if (isRunning)
        {   
           _isRunning = true;
        }
        else
        {
            _isRunning = false;
        }
    }


    private void Jump(bool isJumping)
    {
        if (isJumping)
        {
            if (_isGrounded)
            {
                _isJumping = true;
                _rb.AddForce(Vector3.up * _playerBalancer.jumpForce, ForceMode.Impulse);
            }
        }
    }

    private void GroundCheck()
    {
        _isGrounded = Physics.Raycast(transform.position, Vector3.down, 1.3f);
    }
}