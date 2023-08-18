using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Slider _slider;
    [SerializeField] private Health _health;

    private void OnEnable()
    {
        _health.ValueChanged += OnValueChanged;
        _slider.value = 1;
    }

    private void OnDisable()
    {
        _health.ValueChanged -= OnValueChanged;
    }
    public void OnValueChanged(float value, float maxValue)
    {
        _slider.value = value / maxValue;
    }
}
