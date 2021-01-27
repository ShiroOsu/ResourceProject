// GENERATED AUTOMATICALLY FROM 'Assets/Inputs/Player/PlayerControls.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @PlayerControls : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerControls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerControls"",
    ""maps"": [
        {
            ""name"": ""Player"",
            ""id"": ""03ab447f-7b8a-45e1-8034-7403f6b48162"",
            ""actions"": [
                {
                    ""name"": ""LeftMouse"",
                    ""type"": ""Button"",
                    ""id"": ""f2fdd651-90fb-4817-89f4-b5cff07b566c"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""RightMouse"",
                    ""type"": ""Button"",
                    ""id"": ""2b842f97-fc42-498e-9f86-d0cc92e5d08e"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""4a0b027f-6f50-4f31-bfc4-399ebb467618"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""LeftMouse"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""1e8eb8a0-24e0-4623-94a4-1b00c77a991b"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""RightMouse"",
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
        m_Player_LeftMouse = m_Player.FindAction("LeftMouse", throwIfNotFound: true);
        m_Player_RightMouse = m_Player.FindAction("RightMouse", throwIfNotFound: true);
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
    private readonly InputAction m_Player_LeftMouse;
    private readonly InputAction m_Player_RightMouse;
    public struct PlayerActions
    {
        private @PlayerControls m_Wrapper;
        public PlayerActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @LeftMouse => m_Wrapper.m_Player_LeftMouse;
        public InputAction @RightMouse => m_Wrapper.m_Player_RightMouse;
        public InputActionMap Get() { return m_Wrapper.m_Player; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerActions set) { return set.Get(); }
        public void SetCallbacks(IPlayerActions instance)
        {
            if (m_Wrapper.m_PlayerActionsCallbackInterface != null)
            {
                @LeftMouse.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnLeftMouse;
                @LeftMouse.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnLeftMouse;
                @LeftMouse.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnLeftMouse;
                @RightMouse.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnRightMouse;
                @RightMouse.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnRightMouse;
                @RightMouse.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnRightMouse;
            }
            m_Wrapper.m_PlayerActionsCallbackInterface = instance;
            if (instance != null)
            {
                @LeftMouse.started += instance.OnLeftMouse;
                @LeftMouse.performed += instance.OnLeftMouse;
                @LeftMouse.canceled += instance.OnLeftMouse;
                @RightMouse.started += instance.OnRightMouse;
                @RightMouse.performed += instance.OnRightMouse;
                @RightMouse.canceled += instance.OnRightMouse;
            }
        }
    }
    public PlayerActions @Player => new PlayerActions(this);
    public interface IPlayerActions
    {
        void OnLeftMouse(InputAction.CallbackContext context);
        void OnRightMouse(InputAction.CallbackContext context);
    }
}
