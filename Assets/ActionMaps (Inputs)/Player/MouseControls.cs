// GENERATED AUTOMATICALLY FROM 'Assets/ActionMaps (Inputs)/Player/MouseControls.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @MouseControls : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @MouseControls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""MouseControls"",
    ""maps"": [
        {
            ""name"": ""Mouse"",
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
        },
        {
            ""name"": ""Structure"",
            ""id"": ""0b10dc59-6aee-476e-b6c2-0059b9e1166d"",
            ""actions"": [
                {
                    ""name"": ""RightMouse"",
                    ""type"": ""Button"",
                    ""id"": ""e8bdbee6-79e6-4f7a-ba6f-0187c54fee4b"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""44b488dd-73a1-44ef-a700-7bfef51c9df9"",
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
        // Mouse
        m_Mouse = asset.FindActionMap("Mouse", throwIfNotFound: true);
        m_Mouse_LeftMouse = m_Mouse.FindAction("LeftMouse", throwIfNotFound: true);
        m_Mouse_RightMouse = m_Mouse.FindAction("RightMouse", throwIfNotFound: true);
        // Structure
        m_Structure = asset.FindActionMap("Structure", throwIfNotFound: true);
        m_Structure_RightMouse = m_Structure.FindAction("RightMouse", throwIfNotFound: true);
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

    // Mouse
    private readonly InputActionMap m_Mouse;
    private IMouseActions m_MouseActionsCallbackInterface;
    private readonly InputAction m_Mouse_LeftMouse;
    private readonly InputAction m_Mouse_RightMouse;
    public struct MouseActions
    {
        private @MouseControls m_Wrapper;
        public MouseActions(@MouseControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @LeftMouse => m_Wrapper.m_Mouse_LeftMouse;
        public InputAction @RightMouse => m_Wrapper.m_Mouse_RightMouse;
        public InputActionMap Get() { return m_Wrapper.m_Mouse; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(MouseActions set) { return set.Get(); }
        public void SetCallbacks(IMouseActions instance)
        {
            if (m_Wrapper.m_MouseActionsCallbackInterface != null)
            {
                @LeftMouse.started -= m_Wrapper.m_MouseActionsCallbackInterface.OnLeftMouse;
                @LeftMouse.performed -= m_Wrapper.m_MouseActionsCallbackInterface.OnLeftMouse;
                @LeftMouse.canceled -= m_Wrapper.m_MouseActionsCallbackInterface.OnLeftMouse;
                @RightMouse.started -= m_Wrapper.m_MouseActionsCallbackInterface.OnRightMouse;
                @RightMouse.performed -= m_Wrapper.m_MouseActionsCallbackInterface.OnRightMouse;
                @RightMouse.canceled -= m_Wrapper.m_MouseActionsCallbackInterface.OnRightMouse;
            }
            m_Wrapper.m_MouseActionsCallbackInterface = instance;
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
    public MouseActions @Mouse => new MouseActions(this);

    // Structure
    private readonly InputActionMap m_Structure;
    private IStructureActions m_StructureActionsCallbackInterface;
    private readonly InputAction m_Structure_RightMouse;
    public struct StructureActions
    {
        private @MouseControls m_Wrapper;
        public StructureActions(@MouseControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @RightMouse => m_Wrapper.m_Structure_RightMouse;
        public InputActionMap Get() { return m_Wrapper.m_Structure; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(StructureActions set) { return set.Get(); }
        public void SetCallbacks(IStructureActions instance)
        {
            if (m_Wrapper.m_StructureActionsCallbackInterface != null)
            {
                @RightMouse.started -= m_Wrapper.m_StructureActionsCallbackInterface.OnRightMouse;
                @RightMouse.performed -= m_Wrapper.m_StructureActionsCallbackInterface.OnRightMouse;
                @RightMouse.canceled -= m_Wrapper.m_StructureActionsCallbackInterface.OnRightMouse;
            }
            m_Wrapper.m_StructureActionsCallbackInterface = instance;
            if (instance != null)
            {
                @RightMouse.started += instance.OnRightMouse;
                @RightMouse.performed += instance.OnRightMouse;
                @RightMouse.canceled += instance.OnRightMouse;
            }
        }
    }
    public StructureActions @Structure => new StructureActions(this);
    public interface IMouseActions
    {
        void OnLeftMouse(InputAction.CallbackContext context);
        void OnRightMouse(InputAction.CallbackContext context);
    }
    public interface IStructureActions
    {
        void OnRightMouse(InputAction.CallbackContext context);
    }
}
