using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeControl : MonoBehaviour
{
    // default time scale setting
    [SerializeField] private float _startTimeScale = 1f;

    private void Start()
    {
        Time.timeScale = _startTimeScale;
    }

    // set current time scale
    public void SetTimeScale(float timeScale)
    {
        Time.timeScale = timeScale;
    }
}
