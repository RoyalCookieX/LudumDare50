using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buying : MonoBehaviour
{
    private PlayerInteraction PI;
    private Inventory inventory;

    private void Awake()
    {
        PI = FindObjectOfType<PlayerInteraction>();
        inventory = FindObjectOfType<Inventory>();
    }
    public void Buy(string name)
    {
        if (inventory.CanBuy && PI.TryGetTower(name))
            inventory.CanBuy = false;
    }
}
