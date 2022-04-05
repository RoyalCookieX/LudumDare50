using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CoolDown))]
public class InteractToCoolDown : MonoBehaviour
{
    [SerializeField] InputActionReference Interact;
    [SerializeField] float WaitTime = 5f;
    [SerializeField] bool useCooldown = true; 

    public UnityEvent Interacted;

    private bool CanInteract = true;
    private CoolDown coolDown;

    private void Awake()
    {
        coolDown = GetComponent<CoolDown>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerController>() != null)
        {
            Interact.action.performed += Interactable;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerController>() != null)
        {
            Interact.action.performed -= Interactable;
        }
    }

    private void Interactable(InputAction.CallbackContext obj)
    {
        if (CanInteract)
        {
            Interacted.Invoke();
            if (useCooldown)
            {
                CanInteract = false;
                coolDown.Cooldown(WaitTime);
            }
        }
    }
    public void SetCanInteract(bool _canInteract)
    {
        CanInteract = _canInteract;
    }
}
