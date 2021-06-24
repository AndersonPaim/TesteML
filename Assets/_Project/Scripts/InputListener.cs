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

    [SerializeField] private bool _jump;
    [SerializeField] private bool _run;
    [SerializeField] private bool _walk;
    [SerializeField] private bool _shoot;
    [SerializeField] private bool _aim;

    [SerializeField] private ObjectsTag _arrow = ObjectsTag.RegularArrow;

    private PlayerInputActions _input;

    private InputData _inputData;

    private void Awake()
    {
        StartInputs();
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

    private void OnDestroy() 
    {
       DestroyInputs();
    }

    private void StartInputs()
    {
        _input = new PlayerInputActions();
        _input.Player.Jump.performed += ctx => Jump(ctx);
        _input.Player.Run.performed += ctx => Run(ctx);
        _input.Player.Run.canceled += ctx => Run(ctx);
        _input.Player.Aim.performed += ctx => Aim(ctx);
        _input.Player.Aim.canceled += ctx => Aim(ctx);
        _input.Player.Shoot.performed += ctx => Shoot(ctx);
        _input.Player.Shoot.canceled += ctx => Shoot(ctx);

        _input.Player.Pause.performed += _ => Pause();
        _input.Player.LookX.performed += _ => LookX();
        _input.Player.LookY.performed += _ => LookY(); 
        _input.Player.Arrow1.performed += _ => SelectArrow1();
        _input.Player.Arrow2.performed += _ => SelectArrow2();
        _input.Player.Arrow3.performed += _ => SelectArrow3();
    }

    private void DestroyInputs()
    {
        _input.Player.Jump.performed -= ctx => Jump(ctx);
        _input.Player.Run.performed -= ctx => Run(ctx);
        _input.Player.Run.canceled -= ctx => Run(ctx);
        _input.Player.Aim.performed -= ctx => Aim(ctx);
        _input.Player.Aim.canceled -= ctx => Aim(ctx); 
        _input.Player.Shoot.performed -= ctx => Shoot(ctx);
        _input.Player.Shoot.canceled -= ctx => Shoot(ctx);

        _input.Player.Pause.performed -= _ => Pause();
        _input.Player.LookX.performed -= _ => LookX();
        _input.Player.LookY.performed -= _ => LookY();
        _input.Player.Arrow1.performed -= _ => SelectArrow1();
        _input.Player.Arrow2.performed -= _ => SelectArrow2();
        _input.Player.Arrow3.performed -= _ => SelectArrow3();
    }

    private void CreateInputStruct()
    {
        _inputData.isJumping = _jump;
        _inputData.isRunning = _run;
        _inputData.Movement = _movement;
        _inputData.isWalking = _walk;
        _inputData.LookX = _lookX;
        _inputData.LookY = _lookY;
        _inputData.isAiming = _aim;
        _inputData.isShooting = _shoot;
        _inputData.Arrow = _arrow;

        //reset variables that only need to be read once
        if (_jump)
        {
            _jump = false;
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

    private void Shoot(InputAction.CallbackContext ctx)
    {
        if(ctx.performed)
        {
            _shoot = true;
        }
        else
        {
            _shoot = false;
        }
    }

    //equip arrows
    private void SelectArrow1()
    {
        _arrow = ObjectsTag.RegularArrow;
    }

    private void SelectArrow2() 
    {
        _arrow = ObjectsTag.PiercingArrow;
    }

    private void SelectArrow3() 
    {        
        _arrow = ObjectsTag.ExplosiveArrow;
    }

    //camera view
    private void LookX()
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