using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    [SerializeField] private Button _healButton;
    [SerializeField] private Button _takeDamageButton;
    [SerializeField] private float _value;

    public event Action<float,float> ValueChanged;

    private float _healPoint = 10;
    private float _damagePoint = 10;
    private float _currentValue;

    private Coroutine _coroutine;

    private void OnEnable()
    {
        _healButton.onClick.AddListener(Heal);
        _takeDamageButton.onClick.AddListener(TakeDamage);
    }

    private void Start()
    {
        _currentValue = _value;
    }

    private void OnDisable()
    {
        _takeDamageButton?.onClick.RemoveListener(TakeDamage);
        _healButton?.onClick.RemoveListener(Heal);
    }

    public void Heal()
    {
        float finalChangedValue = _currentValue + _healPoint;

        if(finalChangedValue > _value)
            finalChangedValue = _value;

        StartChangeValue(finalChangedValue);
    }

    public void TakeDamage()
    {
        float finalChangedValue = _currentValue - _damagePoint;

        if (finalChangedValue < 0)
            finalChangedValue = 0;

        StartChangeValue(finalChangedValue);
    }

    private void StartChangeValue(float finalValue)
    {
        if (_coroutine != null)
            StopCoroutine(_coroutine);

        _coroutine = StartCoroutine(ChangeValue(finalValue));
    }

    private IEnumerator ChangeValue(float finalValue)
    {
        float deltaValue = 0.1f;

        while (_currentValue != finalValue)
        {
            _currentValue = Mathf.MoveTowards(_currentValue, finalValue, deltaValue);
            ValueChanged?.Invoke(_currentValue, _value);
            yield return null;
        }
    }
}
