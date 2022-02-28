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
                    ""processors"": ""NormalizeVector2,StickDeadzone"",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Movement"",
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
    public struct ThirdPersonPlayerActions
    {
        private @PlayerInputActions m_Wrapper;
        public ThirdPersonPlayerActions(@PlayerInputActions wrapper) { m_Wrapper = wrapper; }
        public InputAction @Jump => m_Wrapper.m_ThirdPersonPlayer_Jump;
        public InputAction @Movement => m_Wrapper.m_ThirdPersonPlayer_Movement;
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
    }
}
