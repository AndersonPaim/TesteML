using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;

public class InputListener : MonoBehaviour
{

    public delegate void InputHandler(InputData inputData);
    public InputHandler OnInput;

    public delegate void PauseHandler();
    public PauseHandler OnPause;

    public delegate void InteractHandler();
    public InteractHandler OnInteract;

    public delegate void ShootHandler();
    public ShootHandler OnShoot;

    [SerializeField] private Vector2 _movement;
    [SerializeField] private float _lookX;
    [SerializeField] private float _lookY;

    [SerializeField] private objectsTag _arrow;

    [SerializeField] private bool _jump;
    [SerializeField] private bool _run;
    [SerializeField] private bool _walk;
    [SerializeField] private bool _shoot;
    [SerializeField] private bool _aim;

    private PlayerInputActions _input;

    private InputData _inputData;

    private void Awake()
    {
        Initialize();
    }

    private void Update()
    {
        CreateInputStruct();
        OnInput?.Invoke(_inputData);
        _inputData = new InputData();
        Movement();
    }

    private void OnEnable()
    {
        _input.Enable();
    }

    private void OnDisable()
    {
        _input.Disable();
    }

    private void OnDestroy() //TODO AJEITAR ISSO EM UMA FUNÇÃO
    {
        _input.Player.Jump.performed -= ctx => Jump(ctx);
        _input.Player.Run.performed -= ctx => Run(ctx);
        _input.Player.Run.canceled -= ctx => Run(ctx);
        _input.Player.Aim.performed -= ctx => Aim(ctx);
        _input.Player.Aim.canceled -= ctx => Aim(ctx); 

        _input.Player.Shoot.canceled -= _ => Shoot();
        _input.Player.Pause.performed -= _ => Pause();
        _input.Player.LookX.performed -= _ => LookX();
        _input.Player.LookY.performed -= _ => LookY();
        _input.Player.Arrow1.performed -= _ => SelectArrow1();
        _input.Player.Arrow2.performed -= _ => SelectArrow2();
        _input.Player.Arrow3.performed -= _ => SelectArrow3();
    }

    private void Initialize()
    {
        _input = new PlayerInputActions();
        _input.Player.Jump.performed += ctx => Jump(ctx);
        _input.Player.Run.performed += ctx => Run(ctx);
        _input.Player.Run.canceled += ctx => Run(ctx);
        _input.Player.Aim.performed += ctx => Aim(ctx);
        _input.Player.Aim.canceled += ctx => Aim(ctx);

        _input.Player.Shoot.canceled += _ => Shoot();
        _input.Player.Pause.performed += _ => Pause();
        _input.Player.LookX.performed += _ => LookX();
        _input.Player.LookY.performed += _ => LookY(); 
        _input.Player.Arrow1.performed += _ => SelectArrow1();
        _input.Player.Arrow2.performed += _ => SelectArrow2();
        _input.Player.Arrow3.performed += _ => SelectArrow3();

        _arrow = objectsTag.RegularArrow;
    }

    private void CreateInputStruct()
    {
        _inputData.Jump = _jump;
        _inputData.Run = _run;
        _inputData.Movement = _movement;
        _inputData.Walk = _walk;
        _inputData.LookX = _lookX;
        _inputData.LookY = _lookY;
        _inputData.Aim = _aim;
        _inputData.Shoot = _shoot;
        _inputData.Arrow = _arrow;

        if (_jump)
        {
            _jump = false;
        }
        else if(_shoot)
        {
            _shoot = false;
        }
    }

    private void Aim(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            _aim = true;
        }
        else
        {
            _aim = false;
        }
    }

    private void Shoot()
    {
        _shoot = true;
    }

    private void SelectArrow1() //TODO JUNTAR ESSES INPUTS NUMA FUNÇÃO SÓ
    {
        _arrow = objectsTag.RegularArrow;
    }

    private void SelectArrow2() 
    {
        _arrow = objectsTag.PiercingArrow;
    }

    private void SelectArrow3() 
    {        
        _arrow = objectsTag.ExplosiveArrow;
    }

    private void LookX() //TODO JUNTAR EM UM INPUT SÓ
    {
        _lookX = _input.Player.LookX.ReadValue<float>();
    }

    private void LookY()
    {
        _lookY = _input.Player.LookY.ReadValue<float>();
    }

    private void Movement()
    {
        _movement = _input.Player.Movement.ReadValue<Vector2>();
        if (_movement.y != 0)
        {
            _walk = true;
        }
        else
        {
            _walk = false;
        }
    }


    private void Jump(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            _jump = true;
        }
    }

    private void Run(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            _run = true;
        }
        else if (ctx.canceled)
        {
            _walk = true;
            _run = false;
        }
    }

    private void Pause()
    {
        OnPause?.Invoke();
    }


}