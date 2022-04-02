// GENERATED AUTOMATICALLY FROM 'Assets/InputSystem/PlayerInputActions.inputactions'

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
            ""name"": ""ThirdPersonPlayer"",
            ""id"": ""04c5bd5e-1105-489b-9cc5-38b99e9b1726"",
            ""actions"": [
                {
                    ""name"": ""Jump"",
                    ""type"": ""Button"",
                    ""id"": ""82f8c956-cda7-4e16-9c65-11c762c03d8b"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Movement"",
                    ""type"": ""Value"",
                    ""id"": ""61c8c34c-271c-4dc3-8a42-510b7aaaba48"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Attack"",
                    ""type"": ""Button"",
                    ""id"": ""caa45e38-67b5-4c58-8612-2a79afcef59e"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Look"",
                    ""type"": ""PassThrough"",
                    ""id"": ""6f9c4486-2556-42d8-9b49-02b636afbfba"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Sprint"",
                    ""type"": ""Button"",
                    ""id"": ""29e94567-d15f-4b0e-952a-337036118733"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Trigger"",
                    ""type"": ""PassThrough"",
                    ""id"": ""cfe1076e-82cc-4f1c-896d-cfc613323ac5"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Stance"",
                    ""type"": ""PassThrough"",
                    ""id"": ""5f443ecc-4d8f-484e-9de0-04636434d199"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Hold(duration=1),Tap""
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""6119035e-a6d0-49c0-a3f0-af85c8f3333d"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""08226680-90a7-44fb-b11d-fe5b714aa6df"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""c2af196c-dcdc-4fbf-8db8-217286e52dd6"",
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
                    ""id"": ""224db84a-fbd3-4e15-b8c1-b2cbb6d5dc06"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""c545e306-4bd7-450a-a80e-60e725a3d52e"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""1aa6c79e-b995-46cf-be7c-70846cd82799"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""4740f7d4-088a-4c30-8fb2-c7e882c7b738"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""2af4d650-5285-4a99-9306-b4ce145a095b"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""3ac34d76-261a-4c36-b8f8-8785c36db89f"",
                    ""path"": ""<Keyboard>/q"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Attack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e5f291d0-ce63-4a1f-8de6-30f0155ecb45"",
                    ""path"": ""<Gamepad>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Attack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b2041ac8-813c-443b-a738-94c622d71ae9"",
                    ""path"": ""<Gamepad>/rightStick"",
                    ""interactions"": """",
                    ""processors"": ""InvertVector2"",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Look"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a2b022e0-60bb-4088-baf7-09dac18b78e8"",
                    ""path"": ""<Mouse>/delta"",
                    ""interactions"": """",
                    ""processors"": ""InvertVector2"",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Look"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f0099c10-9803-408e-990d-508aa3c3017f"",
                    ""path"": ""<Gamepad>/leftTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Sprint"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""80a83b59-92b2-4d86-a24a-1639f61de0b5"",
                    ""path"": ""<Keyboard>/shift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Sprint"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""61223d0c-cd5c-423b-a81b-626544edfc73"",
                    ""path"": ""<Gamepad>/buttonWest"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Trigger"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""3f038bc5-0921-4734-ba26-af11d06e6702"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Trigger"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""366a177e-9030-4bfb-9890-ddf5c2e47b3f"",
                    ""path"": ""<Gamepad>/buttonNorth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Stance"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""891658f1-6a1c-4723-a95d-4501c63eef72"",
                    ""path"": ""<Keyboard>/leftCtrl"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Stance"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Gamepad"",
            ""bindingGroup"": ""Gamepad"",
            ""devices"": [
                {
                    ""devicePath"": ""<Gamepad>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        },
        {
            ""name"": ""Keyboard"",
            ""bindingGroup"": ""Keyboard"",
            ""devices"": []
        }
    ]
}");
        // ThirdPersonPlayer
        m_ThirdPersonPlayer = asset.FindActionMap("ThirdPersonPlayer", throwIfNotFound: true);
        m_ThirdPersonPlayer_Jump = m_ThirdPersonPlayer.FindAction("Jump", throwIfNotFound: true);
        m_ThirdPersonPlayer_Movement = m_ThirdPersonPlayer.FindAction("Movement", throwIfNotFound: true);
        m_ThirdPersonPlayer_Attack = m_ThirdPersonPlayer.FindAction("Attack", throwIfNotFound: true);
        m_ThirdPersonPlayer_Look = m_ThirdPersonPlayer.FindAction("Look", throwIfNotFound: true);
        m_ThirdPersonPlayer_Sprint = m_ThirdPersonPlayer.FindAction("Sprint", throwIfNotFound: true);
        m_ThirdPersonPlayer_Trigger = m_ThirdPersonPlayer.FindAction("Trigger", throwIfNotFound: true);
        m_ThirdPersonPlayer_Stance = m_ThirdPersonPlayer.FindAction("Stance", throwIfNotFound: true);
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

    // ThirdPersonPlayer
    private readonly InputActionMap m_ThirdPersonPlayer;
    private IThirdPersonPlayerActions m_ThirdPersonPlayerActionsCallbackInterface;
    private readonly InputAction m_ThirdPersonPlayer_Jump;
    private readonly InputAction m_ThirdPersonPlayer_Movement;
    private readonly InputAction m_ThirdPersonPlayer_Attack;
    private readonly InputAction m_ThirdPersonPlayer_Look;
    private readonly InputAction m_ThirdPersonPlayer_Sprint;
    private readonly InputAction m_ThirdPersonPlayer_Trigger;
    private readonly InputAction m_ThirdPersonPlayer_Stance;
    public struct ThirdPersonPlayerActions
    {
        private @PlayerInputActions m_Wrapper;
        public ThirdPersonPlayerActions(@PlayerInputActions wrapper) { m_Wrapper = wrapper; }
        public InputAction @Jump => m_Wrapper.m_ThirdPersonPlayer_Jump;
        public InputAction @Movement => m_Wrapper.m_ThirdPersonPlayer_Movement;
        public InputAction @Attack => m_Wrapper.m_ThirdPersonPlayer_Attack;
        public InputAction @Look => m_Wrapper.m_ThirdPersonPlayer_Look;
        public InputAction @Sprint => m_Wrapper.m_ThirdPersonPlayer_Sprint;
        public InputAction @Trigger => m_Wrapper.m_ThirdPersonPlayer_Trigger;
        public InputAction @Stance => m_Wrapper.m_ThirdPersonPlayer_Stance;
        public InputActionMap Get() { return m_Wrapper.m_ThirdPersonPlayer; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(ThirdPersonPlayerActions set) { return set.Get(); }
        public void SetCallbacks(IThirdPersonPlayerActions instance)
        {
            if (m_Wrapper.m_ThirdPersonPlayerActionsCallbackInterface != null)
            {
                @Jump.started -= m_Wrapper.m_ThirdPersonPlayerActionsCallbackInterface.OnJump;
                @Jump.performed -= m_Wrapper.m_ThirdPersonPlayerActionsCallbackInterface.OnJump;
                @Jump.canceled -= m_Wrapper.m_ThirdPersonPlayerActionsCallbackInterface.OnJump;
                @Movement.started -= m_Wrapper.m_ThirdPersonPlayerActionsCallbackInterface.OnMovement;
                @Movement.performed -= m_Wrapper.m_ThirdPersonPlayerActionsCallbackInterface.OnMovement;
                @Movement.canceled -= m_Wrapper.m_ThirdPersonPlayerActionsCallbackInterface.OnMovement;
                @Attack.started -= m_Wrapper.m_ThirdPersonPlayerActionsCallbackInterface.OnAttack;
                @Attack.performed -= m_Wrapper.m_ThirdPersonPlayerActionsCallbackInterface.OnAttack;
                @Attack.canceled -= m_Wrapper.m_ThirdPersonPlayerActionsCallbackInterface.OnAttack;
                @Look.started -= m_Wrapper.m_ThirdPersonPlayerActionsCallbackInterface.OnLook;
                @Look.performed -= m_Wrapper.m_ThirdPersonPlayerActionsCallbackInterface.OnLook;
                @Look.canceled -= m_Wrapper.m_ThirdPersonPlayerActionsCallbackInterface.OnLook;
                @Sprint.started -= m_Wrapper.m_ThirdPersonPlayerActionsCallbackInterface.OnSprint;
                @Sprint.performed -= m_Wrapper.m_ThirdPersonPlayerActionsCallbackInterface.OnSprint;
                @Sprint.canceled -= m_Wrapper.m_ThirdPersonPlayerActionsCallbackInterface.OnSprint;
                @Trigger.started -= m_Wrapper.m_ThirdPersonPlayerActionsCallbackInterface.OnTrigger;
                @Trigger.performed -= m_Wrapper.m_ThirdPersonPlayerActionsCallbackInterface.OnTrigger;
                @Trigger.canceled -= m_Wrapper.m_ThirdPersonPlayerActionsCallbackInterface.OnTrigger;
                @Stance.started -= m_Wrapper.m_ThirdPersonPlayerActionsCallbackInterface.OnStance;
                @Stance.performed -= m_Wrapper.m_ThirdPersonPlayerActionsCallbackInterface.OnStance;
                @Stance.canceled -= m_Wrapper.m_ThirdPersonPlayerActionsCallbackInterface.OnStance;
            }
            m_Wrapper.m_ThirdPersonPlayerActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Jump.started += instance.OnJump;
                @Jump.performed += instance.OnJump;
                @Jump.canceled += instance.OnJump;
                @Movement.started += instance.OnMovement;
                @Movement.performed += instance.OnMovement;
                @Movement.canceled += instance.OnMovement;
                @Attack.started += instance.OnAttack;
                @Attack.performed += instance.OnAttack;
                @Attack.canceled += instance.OnAttack;
                @Look.started += instance.OnLook;
                @Look.performed += instance.OnLook;
                @Look.canceled += instance.OnLook;
                @Sprint.started += instance.OnSprint;
                @Sprint.performed += instance.OnSprint;
                @Sprint.canceled += instance.OnSprint;
                @Trigger.started += instance.OnTrigger;
                @Trigger.performed += instance.OnTrigger;
                @Trigger.canceled += instance.OnTrigger;
                @Stance.started += instance.OnStance;
                @Stance.performed += instance.OnStance;
                @Stance.canceled += instance.OnStance;
            }
        }
    }
    public ThirdPersonPlayerActions @ThirdPersonPlayer => new ThirdPersonPlayerActions(this);
    private int m_GamepadSchemeIndex = -1;
    public InputControlScheme GamepadScheme
    {
        get
        {
            if (m_GamepadSchemeIndex == -1) m_GamepadSchemeIndex = asset.FindControlSchemeIndex("Gamepad");
            return asset.controlSchemes[m_GamepadSchemeIndex];
        }
    }
    private int m_KeyboardSchemeIndex = -1;
    public InputControlScheme KeyboardScheme
    {
        get
        {
            if (m_KeyboardSchemeIndex == -1) m_KeyboardSchemeIndex = asset.FindControlSchemeIndex("Keyboard");
            return asset.controlSchemes[m_KeyboardSchemeIndex];
        }
    }
    public interface IThirdPersonPlayerActions
    {
        void OnJump(InputAction.CallbackContext context);
        void OnMovement(InputAction.CallbackContext context);
        void OnAttack(InputAction.CallbackContext context);
        void OnLook(InputAction.CallbackContext context);
        void OnSprint(InputAction.CallbackContext context);
        void OnTrigger(InputAction.CallbackContext context);
        void OnStance(InputAction.CallbackContext context);
    }
}
