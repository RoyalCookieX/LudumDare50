using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Buying : MonoBehaviour
{
    public UnityEvent Buy;
    [SerializeField] private Inventory UIInventory;

    [Header("Price")]
    [SerializeField] private int Wood;
    [SerializeField] private int Stone;
    public bool canBuy = true;

    public void TryBuy()
    {
        Debug.Log("Trying to buy");
        if (UIInventory.inventory["Wood"] >= Wood && UIInventory.inventory["Stone"] >= Stone && canBuy)
        {
            canBuy = false;

            UIInventory.inventory["Wood"] -= Wood;
            UIInventory.inventory["Stone"] -= Stone;
            UIInventory.UpdateUI();
            Debug.Log("Baught");
            Buy.Invoke();
        }
    }
}
