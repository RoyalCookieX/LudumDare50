using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;


public class Player : MonoBehaviour
{
    private bool _isPlacingTurret = false;
    private string _isPlacingTurretType;

    public bool IsPlacingTurret { get { return _isPlacingTurret; } }
    public string IsPlacingTurretType { get { return _isPlacingTurretType; } }

    public void ToggleSetTurretPlaceActive()
    {
        _isPlacingTurret = !_isPlacingTurret;
        
        if (!_isPlacingTurret) _isPlacingTurretType = null;
    }

    public void SetTurretPlaceType(string turretType)
    {
        _isPlacingTurretType = turretType;
    }
}