using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;


public class Player : MonoBehaviour
{
    [Header("Place Turret Scriptable Objects")]
    [SerializeField] private PlaceTurret _radiusTurret;
    [SerializeField] private PlaceTurret _shotgunTurret;

    private bool _isPlacingTurret = false;
    private string _isPlacingTurretType;

    private Inventory _inventory;

    public bool IsPlacingTurret { get => _isPlacingTurret; }
    public string IsPlacingTurretType { get { return _isPlacingTurretType; } }
    public Vector2 TurretPlacePositionPassthrough { get; set; }

    private void Awake()
    {
        _radiusTurret.SetPlayerMovementScript(GetComponent<PlayerMovement>());
        _shotgunTurret.SetPlayerMovementScript(GetComponent<PlayerMovement>());
        _inventory = FindObjectOfType<Inventory>();
    }

    public void ToggleSetTurretPlaceActive()
    {
        _isPlacingTurret = !_isPlacingTurret;
        Debug.Log(_isPlacingTurret.ToString());

        if (!_isPlacingTurret)
            _inventory.CanBuy = true;
    }

    public void SetTurretPlaceTypeRadius()
    {
        _isPlacingTurretType = "Radius";
    }

    public void SetTurretPlaceTypeShotgun()
    {
        _isPlacingTurretType = "Shotgun";
    }

    public void CoordinatePassthrough(Vector2 passthrough)
    {
        if (_isPlacingTurretType == "Radius" && _radiusTurret.Place(passthrough)) // I made _radiusTurret.Place(passthrough) a bool method to see if you actually laced anything
        {
            ToggleSetTurretPlaceActive();
        }
        else if (_isPlacingTurretType == "Shotgun" && _shotgunTurret.Place(passthrough))
        {
            ToggleSetTurretPlaceActive();
        }
    }
}