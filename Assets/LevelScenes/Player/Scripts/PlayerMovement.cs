using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float decreaseStamina,
        increaseStamina,
        decreaseSpeed,
        increaseSpeed,
        decreaseSpeedThreshold;
    
    [NonSerialized] public float baseSpeed;
    [NonSerialized] public float maxStamina;
    public float _speed = 3f;
    public float _stamina = 100f;
    
    private bool _running;

    private void Start()
    {
        maxStamina =_stamina;
        baseSpeed = _speed;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
            _running = true;
        if(Input.GetMouseButton(0))
            PressDown();
        if (Input.GetMouseButtonUp(0))
            _running = false;
        PressUp();
    }

    void PressDown()
    {
        _stamina -= Time.deltaTime * decreaseStamina;
        if (_stamina <= decreaseSpeedThreshold)
        {
            if (_speed > 0)
                _speed -= Time.deltaTime * decreaseSpeed;
            if (_stamina <= 0)
                Lose();
        }
    }

    void Lose()
    {
        Destroy(gameObject);
    }
    void GetColored()
    {
        float AplhaRange = _stamina;
    }
    void PressUp()
    {
        if (!_running && _stamina <=maxStamina)
        {
            _stamina += Time.deltaTime * increaseStamina;
            if (_stamina >= decreaseSpeedThreshold)
                _speed = baseSpeed;
            else if (_stamina <= 75 && _speed <= baseSpeed)
                _speed += Time.deltaTime * increaseSpeed;
        }
    }
}
