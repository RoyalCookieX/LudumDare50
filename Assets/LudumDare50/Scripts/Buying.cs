using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buying : MonoBehaviour
{
    private PlayerInteraction PI;

    private void Awake()
    {
        PI = FindObjectOfType<PlayerInteraction>();
    }
    public void Buy(string name)
    {
        PI.TryGetTower(name);
    }
}
