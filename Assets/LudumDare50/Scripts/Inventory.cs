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
    private void Awake()
    {
        inventory.Add("Wood", 0);
        inventory.Add("Stone", 0);
    }
    public void AddSubtractWood(int value)
    {
        inventory["Wood"] += value;
        UpdateUI();
    }
    public void AddSubtractStone(int value)
    {
        inventory["Stone"] += value;
        UpdateUI();
    }
    private void UpdateUI()
    {
        WoodNumber.text = inventory["Wood"].ToString();
        StoneNumber.text = inventory["Stone"].ToString();
    }
}
