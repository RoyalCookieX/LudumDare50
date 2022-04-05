using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerHealth : MonoBehaviour
{
    private Tower tower;
    private Image healthbar;
    private float MaxHealth;

    void Awake()
    {
        tower = GetComponent<Tower>();
        MaxHealth = tower.Health;
        healthbar = GetComponentInChildren<Image>();
    }
    private void Start()
    {
        FindObjectOfType<Inventory>().CanBuy = true;
    }
    void Update()
    {
        healthbar.fillAmount = tower.Health/ MaxHealth;
    }
}
