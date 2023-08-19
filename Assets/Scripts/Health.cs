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

    public event Action<float,float,float> ValueChanged;

    private float _healPoint = 10;
    private float _damagePoint = 10;
    private float _currentValue;
        
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
     
        ValueChanged?.Invoke(_currentValue, _value, finalChangedValue);

        _currentValue = finalChangedValue;
    }

    public void TakeDamage()
    {
        float finalChangedValue = _currentValue - _damagePoint;

        if (finalChangedValue < 0)
            finalChangedValue = 0;
   
        ValueChanged?.Invoke(_currentValue, _value, finalChangedValue);

        _currentValue = finalChangedValue;
    }
}
