// GENERATED AUTOMATICALLY FROM 'Assets/PlayerInputActions.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @PlayerInputActions : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerInputActions()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerInputActions"",
    ""maps"": [
        {
            ""name"": ""Player"",
            ""id"": ""afc7b66b-93f3-40ee-99e5-b94d4e0e16af"",
            ""actions"": [
                {
                    ""name"": ""Movement"",
                    ""type"": ""Value"",
                    ""id"": ""c43274e0-c5ed-41ea-b936-5e7fd49e83db"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Jump"",
                    ""type"": ""Button"",
                    ""id"": ""55ad06af-b23d-4513-9076-0e6236d17f4b"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Pause"",
                    ""type"": ""Button"",
                    ""id"": ""4f5335df-f493-49fe-a460-10dc0bcfd867"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Run"",
                    ""type"": ""Button"",
                    ""id"": ""91b0a255-f9b0-4ad6-8046-da6e1fb0d7e7"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Interact"",
                    ""type"": ""Button"",
                    ""id"": ""eae9016d-6eec-485d-8541-84ab0741fc16"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""LookX"",
                    ""type"": ""PassThrough"",
                    ""id"": ""80422973-31b5-4743-a60f-0015941748d2"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""LookY"",
                    ""type"": ""PassThrough"",
                    ""id"": ""bdb161a0-4c41-4ef5-8fa6-bf7f09ef0475"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Arrow1"",
                    ""type"": ""Button"",
                    ""id"": ""8815e5b9-8580-4021-8540-c4333a961011"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Arrow2"",
                    ""type"": ""Button"",
                    ""id"": ""9edb9953-c006-4667-8d0f-fb4cb3c65bbb"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Arrow3"",
                    ""type"": ""Button"",
                    ""id"": ""52a2fe9f-a554-4417-84db-7ca31fc80e4a"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Shoot"",
                    ""type"": ""Button"",
                    ""id"": ""17d5d137-6a08-4f36-9449-fea41dd398ef"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Aim"",
                    ""type"": ""Button"",
                    ""id"": ""57db8488-b5e8-4389-8586-3dd61fb015de"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Hold""
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""Keyboard"",
                    ""id"": ""f50be35f-20bb-4bb6-9f9e-05b8c0ea01dc"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""cc64e53f-cdba-4bf6-a40d-fd9f7941b326"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""0f1eabf3-848b-40ec-a0e1-dd2aa9b284d1"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""67c8f5e2-3fa7-4fda-ae63-fd120139da7c"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""99bd1989-5a58-4cd7-ae34-2e0a8c482d3b"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""e6e77ced-3ca6-4ed8-8475-30d2c86bdee6"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d1ef8812-a426-45eb-9ed8-1e28e5ab7d94"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Pause"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c6967a14-f897-4900-9109-f6915bb11220"",
                    ""path"": ""<Keyboard>/leftShift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Run"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""bf6fdd54-bd2e-423a-a831-ca7b271501fd"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Interact"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b98ddd38-a0b0-4c8f-b29d-e5a0f890495d"",
                    ""path"": ""<Mouse>/delta/x"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""LookX"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e6e00d85-5ff1-49ca-a800-b63231ac964a"",
                    ""path"": ""<Mouse>/delta/y"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""LookY"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ac90c2b1-9bea-4dc5-bb37-2af3b0f46531"",
                    ""path"": ""<Keyboard>/2"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Arrow2"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e9b6344c-b621-4016-9047-7772889ebf0b"",
                    ""path"": ""<Keyboard>/3"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Arrow3"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e9046b91-a757-4d5e-9652-c8653cb5344c"",
                    ""path"": ""<Keyboard>/1"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Arrow1"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""85081052-1072-4856-9a48-5333761610f2"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Aim"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""432ca4d5-1cf5-47f2-ad3e-a8b933b8862c"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Shoot"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Player
        m_Player = asset.FindActionMap("Player", throwIfNotFound: true);
        m_Player_Movement = m_Player.FindAction("Movement", throwIfNotFound: true);
        m_Player_Jump = m_Player.FindAction("Jump", throwIfNotFound: true);
        m_Player_Pause = m_Player.FindAction("Pause", throwIfNotFound: true);
        m_Player_Run = m_Player.FindAction("Run", throwIfNotFound: true);
        m_Player_Interact = m_Player.FindAction("Interact", throwIfNotFound: true);
        m_Player_LookX = m_Player.FindAction("LookX", throwIfNotFound: true);
        m_Player_LookY = m_Player.FindAction("LookY", throwIfNotFound: true);
        m_Player_Arrow1 = m_Player.FindAction("Arrow1", throwIfNotFound: true);
        m_Player_Arrow2 = m_Player.FindAction("Arrow2", throwIfNotFound: true);
        m_Player_Arrow3 = m_Player.FindAction("Arrow3", throwIfNotFound: true);
        m_Player_Shoot = m_Player.FindAction("Shoot", throwIfNotFound: true);
        m_Player_Aim = m_Player.FindAction("Aim", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }

    // Player
    private readonly InputActionMap m_Player;
    private IPlayerActions m_PlayerActionsCallbackInterface;
    private readonly InputAction m_Player_Movement;
    private readonly InputAction m_Player_Jump;
    private readonly InputAction m_Player_Pause;
    private readonly InputAction m_Player_Run;
    private readonly InputAction m_Player_Interact;
    private readonly InputAction m_Player_LookX;
    private readonly InputAction m_Player_LookY;
    private readonly InputAction m_Player_Arrow1;
    private readonly InputAction m_Player_Arrow2;
    private readonly InputAction m_Player_Arrow3;
    private readonly InputAction m_Player_Shoot;
    private readonly InputAction m_Player_Aim;
    public struct PlayerActions
    {
        private @PlayerInputActions m_Wrapper;
        public PlayerActions(@PlayerInputActions wrapper) { m_Wrapper = wrapper; }
        public InputAction @Movement => m_Wrapper.m_Player_Movement;
        public InputAction @Jump => m_Wrapper.m_Player_Jump;
        public InputAction @Pause => m_Wrapper.m_Player_Pause;
        public InputAction @Run => m_Wrapper.m_Player_Run;
        public InputAction @Interact => m_Wrapper.m_Player_Interact;
        public InputAction @LookX => m_Wrapper.m_Player_LookX;
        public InputAction @LookY => m_Wrapper.m_Player_LookY;
        public InputAction @Arrow1 => m_Wrapper.m_Player_Arrow1;
        public InputAction @Arrow2 => m_Wrapper.m_Player_Arrow2;
        public InputAction @Arrow3 => m_Wrapper.m_Player_Arrow3;
        public InputAction @Shoot => m_Wrapper.m_Player_Shoot;
        public InputAction @Aim => m_Wrapper.m_Player_Aim;
        public InputActionMap Get() { return m_Wrapper.m_Player; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerActions set) { return set.Get(); }
        public void SetCallbacks(IPlayerActions instance)
        {
            if (m_Wrapper.m_PlayerActionsCallbackInterface != null)
            {
                @Movement.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMovement;
                @Movement.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMovement;
                @Movement.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMovement;
                @Jump.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnJump;
                @Jump.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnJump;
                @Jump.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnJump;
                @Pause.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnPause;
                @Pause.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnPause;
                @Pause.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnPause;
                @Run.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnRun;
                @Run.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnRun;
                @Run.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnRun;
                @Interact.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnInteract;
                @Interact.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnInteract;
                @Interact.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnInteract;
                @LookX.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnLookX;
                @LookX.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnLookX;
                @LookX.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnLookX;
                @LookY.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnLookY;
                @LookY.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnLookY;
                @LookY.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnLookY;
                @Arrow1.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnArrow1;
                @Arrow1.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnArrow1;
                @Arrow1.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnArrow1;
                @Arrow2.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnArrow2;
                @Arrow2.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnArrow2;
                @Arrow2.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnArrow2;
                @Arrow3.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnArrow3;
                @Arrow3.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnArrow3;
                @Arrow3.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnArrow3;
                @Shoot.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnShoot;
                @Shoot.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnShoot;
                @Shoot.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnShoot;
                @Aim.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnAim;
                @Aim.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnAim;
                @Aim.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnAim;
            }
            m_Wrapper.m_PlayerActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Movement.started += instance.OnMovement;
                @Movement.performed += instance.OnMovement;
                @Movement.canceled += instance.OnMovement;
                @Jump.started += instance.OnJump;
                @Jump.performed += instance.OnJump;
                @Jump.canceled += instance.OnJump;
                @Pause.started += instance.OnPause;
                @Pause.performed += instance.OnPause;
                @Pause.canceled += instance.OnPause;
                @Run.started += instance.OnRun;
                @Run.performed += instance.OnRun;
                @Run.canceled += instance.OnRun;
                @Interact.started += instance.OnInteract;
                @Interact.performed += instance.OnInteract;
                @Interact.canceled += instance.OnInteract;
                @LookX.started += instance.OnLookX;
                @LookX.performed += instance.OnLookX;
                @LookX.canceled += instance.OnLookX;
                @LookY.started += instance.OnLookY;
                @LookY.performed += instance.OnLookY;
                @LookY.canceled += instance.OnLookY;
                @Arrow1.started += instance.OnArrow1;
                @Arrow1.performed += instance.OnArrow1;
                @Arrow1.canceled += instance.OnArrow1;
                @Arrow2.started += instance.OnArrow2;
                @Arrow2.performed += instance.OnArrow2;
                @Arrow2.canceled += instance.OnArrow2;
                @Arrow3.started += instance.OnArrow3;
                @Arrow3.performed += instance.OnArrow3;
                @Arrow3.canceled += instance.OnArrow3;
                @Shoot.started += instance.OnShoot;
                @Shoot.performed += instance.OnShoot;
                @Shoot.canceled += instance.OnShoot;
                @Aim.started += instance.OnAim;
                @Aim.performed += instance.OnAim;
                @Aim.canceled += instance.OnAim;
            }
        }
    }
    public PlayerActions @Player => new PlayerActions(this);
    public interface IPlayerActions
    {
        void OnMovement(InputAction.CallbackContext context);
        void OnJump(InputAction.CallbackContext context);
        void OnPause(InputAction.CallbackContext context);
        void OnRun(InputAction.CallbackContext context);
        void OnInteract(InputAction.CallbackContext context);
        void OnLookX(InputAction.CallbackContext context);
        void OnLookY(InputAction.CallbackContext context);
        void OnArrow1(InputAction.CallbackContext context);
        void OnArrow2(InputAction.CallbackContext context);
        void OnArrow3(InputAction.CallbackContext context);
        void OnShoot(InputAction.CallbackContext context);
        void OnAim(InputAction.CallbackContext context);
    }
}
