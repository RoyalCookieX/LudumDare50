using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SetMixerParameter : MonoBehaviour
{
    [SerializeField] private AudioMixer _mixer;
    [SerializeField] private string _parameterName = "Missing Parameter";
    [SerializeField] private float _valueMultiplier = 10f;

    private void OnEnable()
    {
        // get current mixer value and assign to slider
        if(_mixer.GetFloat(_parameterName, out float value) && TryGetComponent(out Slider slider))
        {
            slider.SetValueWithoutNotify(value / _valueMultiplier);
        }
    }

    // set parameter in mixer
    public void SetValue(float value)
    {
        _mixer.SetFloat(_parameterName, value * _valueMultiplier);
    }
}
