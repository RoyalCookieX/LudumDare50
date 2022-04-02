using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(CoolDown))]
public class InteractToCoolDown : MonoBehaviour
{
    [SerializeField] float WaitTime = 5f;
    public UnityEvent Interacted;
    private bool CanInteract = true;
    private CoolDown coolDown;
    private void Awake()
    {
        coolDown = GetComponent<CoolDown>();
    }
    private void Start()
    {
        Interact();
    }
    public void Interact() //Change to on trigger enter when press Interact Button
    {
        if (CanInteract)
        {
            CanInteract = false;
            Interacted.Invoke();
            coolDown.Cooldown(WaitTime);
        }
    }
    public void SetCanInteract(bool _canInteract)
    {
        CanInteract = _canInteract;
    }
}
