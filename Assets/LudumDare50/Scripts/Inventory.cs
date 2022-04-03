using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Inventory : MonoBehaviour
{
    [SerializeField] private TMP_Text WoodNumber;
    [SerializeField] private TMP_Text StoneNumber;
    public Dictionary<string, int> inventory = new Dictionary<string, int>();
    public bool CanBuy = true;
    private void Awake()
    {
        inventory.Add("Wood", 0);
        inventory.Add("Stone", 0);
    }
    public void AddSubtractWood(int value)
    {
        inventory["Wood"] += value;

        if (inventory["Wood"] > 99)
            inventory["Wood"] = 99;

        UpdateUI();
    }
    public void AddSubtractStone(int value)
    {
        inventory["Stone"] += value;

        if (inventory["Stone"] > 99)
            inventory["Stone"] = 99;

        UpdateUI();
    }
    public void UpdateUI()
    {
        WoodNumber.text = inventory["Wood"].ToString();
        StoneNumber.text = inventory["Stone"].ToString();
    }
}
