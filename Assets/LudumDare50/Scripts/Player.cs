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

    public bool IsPlacingTurret { get => _isPlacingTurret; }
    public string IsPlacingTurretType { get { return _isPlacingTurretType; } }
    public Vector2 TurretPlacePositionPassthrough { get; set; }

    public void ToggleSetTurretPlaceActive()
    {
        _isPlacingTurret = !_isPlacingTurret;
        Debug.Log(_isPlacingTurret.ToString());
    }

    public void SetTurretPlaceTypeRadius(string turretType)
    {
        _isPlacingTurretType = "Radius";
    }

    public void SetTurretPlaceTypeShotgun(string turretType)
    {
        _isPlacingTurretType = "Shotgun";
    }

    public void CoordinatePassthrough(Vector2 passthrough)
    {
        if (_isPlacingTurretType == "Radius")
        {
            _radiusTurret.Place(passthrough);
        }
        else if (_isPlacingTurretType == "Shotgun")
        {
            _shotgunTurret.Place(passthrough);
        }
    }
}