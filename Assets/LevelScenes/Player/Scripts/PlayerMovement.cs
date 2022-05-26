using System;
using System.Collections;
using System.Collections.Generic;
using AmplifyShaderEditor;
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

    [SerializeField] private GameObject playerShader;
    private Material playerMat;
    [Range(0,1)]
    private float fillAmount;

    private void Awake()
    {
        playerMat = playerShader.GetComponent<SkinnedMeshRenderer>().material;
    }

    private void Start()
    {
        maxStamina =_stamina;
        baseSpeed = _speed;
        fillAmount = 0f;
    }

    void Update()
    {
        if (GameManager.Instance.Gamestate == GameManager.GAMESTATE.Ingame)
        {
            if (Input.GetMouseButtonDown(0))
                _running = true;
            if(Input.GetMouseButton(0))
                PressDown();
            if (Input.GetMouseButtonUp(0))
                _running = false;
            PressUp();
            GetFill();
        }
    }

    void PressDown()
    {
        _stamina -= Time.deltaTime * decreaseStamina;
        if (_stamina <= decreaseSpeedThreshold)
        {
            if (_speed > _speed * 0.75f)
            {
                _speed -= Time.deltaTime * decreaseSpeed;
            }
            if (_stamina <= 0)
                Lose();
        }
    }

    void Lose()
    {
        Destroy(gameObject);
    }

    void GetFill()
    {
        var fill = _stamina / maxStamina;
        fillAmount = 1f - fill;
        playerMat.SetFloat("_Fill", fillAmount);
    }
    
    void PressUp()
    {
        if (!_running && _stamina <= maxStamina)
        {
            _stamina += Time.deltaTime * increaseStamina;
            if (_stamina >= decreaseSpeedThreshold)
            {
                _speed = baseSpeed;
            }
            else if (_stamina <= 75 && _speed <= baseSpeed)
            {
                _speed += Time.deltaTime * increaseSpeed;
            }
        }
    }
}
