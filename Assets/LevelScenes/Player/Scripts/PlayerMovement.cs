using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float stamina,
        decreaseStamina,
        increaseStamina,
        decreaseSpeed,
        increaseSpeed,
        baseSpeed,
        decreaseSpeedThreshold,
        maxStamina;

    public float speed;
    private bool _running;

    void Awake()
    {
        baseSpeed = speed;
        maxStamina = stamina;
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
        stamina -= Time.deltaTime * decreaseStamina;
        if (stamina <= decreaseSpeedThreshold)
        {
            if (speed > 0)
                speed -= Time.deltaTime * decreaseSpeed;
            if (stamina <= 0)
                Lose();
        }
    }

    void Lose()
    {
        Destroy(gameObject);
    }
    void GetColored()
    {
        float AplhaRange = stamina;
    }
    void PressUp()
    {
        if (!_running && stamina <=maxStamina)
        {
            stamina += Time.deltaTime * increaseStamina;
            if (stamina >= decreaseSpeedThreshold)
                speed = baseSpeed;
            else if (stamina <= 75 && speed <= baseSpeed)
                speed += Time.deltaTime * increaseSpeed;
        }
    }
}
