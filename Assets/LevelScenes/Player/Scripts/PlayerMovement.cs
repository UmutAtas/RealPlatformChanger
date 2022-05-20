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
        baseSpeed,
        decreaseSpeedThreshold,
        maxStamina;

    public float speed;
    private bool _running;
    private float _stamina;

    void Awake()
    {
        if (PlayerPrefs.HasKey("Speed"))
            baseSpeed = PlayerPrefs.GetFloat("Speed");
        else
            baseSpeed = 3f;
        
        if (PlayerPrefs.HasKey("Stamina"))
            maxStamina = PlayerPrefs.GetFloat("Stamina");
        else
            maxStamina = 100f;
    }

    private void Start()
    {
        _stamina = maxStamina;
        speed = baseSpeed;
    }

    void Update()
    {
        print(speed);
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
            if (speed > 0)
                speed -= Time.deltaTime * decreaseSpeed;
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
                speed = baseSpeed;
            else if (_stamina <= 75 && speed <= baseSpeed)
                speed += Time.deltaTime * increaseSpeed;
        }
    }
}
