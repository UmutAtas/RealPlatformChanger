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

    private bool _running;

    [SerializeField] private GameObject playerShader;
    private Material playerMat;
    [Range(0,1)]
    private float fillAmount;

    private ButtonManager BM;

    [SerializeField] private GameObject ragDoll;
    private void Awake()
    {
        playerMat = playerShader.GetComponent<SkinnedMeshRenderer>().material;
    }

    private void Start()
    {
        BM = ButtonManager.Instance;
        maxStamina =BM.Stamina;
        baseSpeed = BM.Speed;
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
        BM.Stamina -= Time.deltaTime * decreaseStamina;
        if (BM.Stamina <= decreaseSpeedThreshold)
        {
            if (BM.Speed > BM.Speed * 0.75f)
            {
                BM.Speed -= Time.deltaTime * decreaseSpeed;
            }
            if (BM.Stamina <= 0)
                Lose();
        }
    }

    void Lose()
    {
       gameObject.SetActive(false);
       Instantiate(ragDoll, transform.position, Quaternion.identity);
    }

    void GetFill()
    {
        var fill = BM.Stamina / maxStamina;
        fillAmount = 1f - fill;
        playerMat.SetFloat("_Fill", fillAmount);
    }
    
    void PressUp()
    {
        if (!_running && BM.Stamina <= maxStamina)
        {
            BM.Stamina += Time.deltaTime * increaseStamina;
            if (BM.Stamina >= decreaseSpeedThreshold)
            {
                BM.Speed = baseSpeed;
            }
            else if (BM.Stamina <= 75 && BM.Speed <= baseSpeed)
            {
                BM.Speed += Time.deltaTime * increaseSpeed;
            }
        }
    }
}
