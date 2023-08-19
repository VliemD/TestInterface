using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Slider _slider;
    [SerializeField] private Health _health;

    private Coroutine _coroutine;

    private void OnEnable()
    {
        _health.ValueChanged += OnValueChanged;
        _slider.value = 1;
    }

    private void OnDisable()
    {
        _health.ValueChanged -= OnValueChanged;
    }

    private void OnValueChanged(float value, float maxValue, float finalValue)
{
        if (_coroutine != null)
            StopCoroutine(_coroutine);

        _coroutine = StartCoroutine(ChangeValue(value, maxValue, finalValue));
    }

    private IEnumerator ChangeValue(float value, float maxValue, float finalValue)
    {
        float deltaValue = 0.1f;

        while (value != finalValue)
        {
            yield return null;
            value = Mathf.MoveTowards(value, finalValue, deltaValue);
            _slider.value = value / maxValue;
        }
    }
}
