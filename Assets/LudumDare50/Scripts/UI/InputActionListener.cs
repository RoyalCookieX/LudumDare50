using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;
using System;
using UnityEngine.UI;

public class InputActionListener : MonoBehaviour
{
    // specific input to listen to
    [SerializeField] private InputActionReference _actionReference;

    // optional button to activate
    [SerializeField] private Button _activateButton;

    // unity event to invoke when action is performed
    public UnityEvent OnPerformed;

    public void InvokeOnPerformed()
    {
        OnPerformed?.Invoke();
    }

    private void OnEnable()
    {
        // subscribe to action's 'performed' event
        _actionReference.action.performed += InputActionPerformed;
    }

    // subscribed to input action's performed event in OnEnable
    private void InputActionPerformed(InputAction.CallbackContext obj)
    {
        OnPerformed.Invoke();
        // actovate button if not null
        _activateButton?.onClick.Invoke();
    }

    private void OnDisable()
    {
        // unsubscribed from 'performed' event when component is disabled
        _actionReference.action.performed -= InputActionPerformed;
    }
}
