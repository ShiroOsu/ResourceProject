//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.1.1
//     from Assets/ActionMaps (Inputs)/Player/PauseControls.inputactions
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public partial class @PauseControls : IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @PauseControls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PauseControls"",
    ""maps"": [
        {
            ""name"": ""Keyboard"",
            ""id"": ""76f36a25-c307-45e4-9106-d89b93d5ed18"",
            ""actions"": [
                {
                    ""name"": ""ESCPause"",
                    ""type"": ""Button"",
                    ""id"": ""f84b7398-3aa8-4065-88d7-025212e2c9aa"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""4dc9ad8e-5320-422b-a562-737f94f1d194"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ESCPause"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Keyboard
        m_Keyboard = asset.FindActionMap("Keyboard", throwIfNotFound: true);
        m_Keyboard_ESCPause = m_Keyboard.FindAction("ESCPause", throwIfNotFound: true);
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
    public IEnumerable<InputBinding> bindings => asset.bindings;

    public InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false)
    {
        return asset.FindAction(actionNameOrId, throwIfNotFound);
    }
    public int FindBinding(InputBinding bindingMask, out InputAction action)
    {
        return asset.FindBinding(bindingMask, out action);
    }

    // Keyboard
    private readonly InputActionMap m_Keyboard;
    private IKeyboardActions m_KeyboardActionsCallbackInterface;
    private readonly InputAction m_Keyboard_ESCPause;
    public struct KeyboardActions
    {
        private @PauseControls m_Wrapper;
        public KeyboardActions(@PauseControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @ESCPause => m_Wrapper.m_Keyboard_ESCPause;
        public InputActionMap Get() { return m_Wrapper.m_Keyboard; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(KeyboardActions set) { return set.Get(); }
        public void SetCallbacks(IKeyboardActions instance)
        {
            if (m_Wrapper.m_KeyboardActionsCallbackInterface != null)
            {
                @ESCPause.started -= m_Wrapper.m_KeyboardActionsCallbackInterface.OnESCPause;
                @ESCPause.performed -= m_Wrapper.m_KeyboardActionsCallbackInterface.OnESCPause;
                @ESCPause.canceled -= m_Wrapper.m_KeyboardActionsCallbackInterface.OnESCPause;
            }
            m_Wrapper.m_KeyboardActionsCallbackInterface = instance;
            if (instance != null)
            {
                @ESCPause.started += instance.OnESCPause;
                @ESCPause.performed += instance.OnESCPause;
                @ESCPause.canceled += instance.OnESCPause;
            }
        }
    }
    public KeyboardActions @Keyboard => new KeyboardActions(this);
    public interface IKeyboardActions
    {
        void OnESCPause(InputAction.CallbackContext context);
    }
}