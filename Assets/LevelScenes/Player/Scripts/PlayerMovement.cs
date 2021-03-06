using System;
using DG.Tweening;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float decreaseStamina,
        increaseStamina,
        decreaseSpeed,
        increaseSpeed,
        decreaseSpeedThreshold;
    
    public static float baseSpeed;
    public static float maxStamina; 
    public static float decreaseStaminaOnMouseUp;

    private bool _running;

    [SerializeField] private GameObject playerShader;
    private Material playerMat;
    [Range(0,1)]
    private float fillAmount;

    private ButtonManager BM;

    [SerializeField] private GameObject ragDoll;
    [SerializeField] private GameObject playerParent;

    [SerializeField] private GameObject sweatParticle;
    [NonSerialized] public static bool canSweat = true;
    [SerializeField] private Transform sweatScaleTransform;
    [SerializeField] private float sweatScale;
    [SerializeField] private float breathingTime;
    private Vector3 _startScale;
    private bool canScale = true;
    
    private void Awake()
    {
        playerMat = playerShader.GetComponent<SkinnedMeshRenderer>().material;
        _startScale = Vector3.one;
    }

    private void Start()
    {
        canSweat = true;
        BM = ButtonManager.Instance;
        BM.Speed = PlayerPrefs.GetFloat("Speed", 1.3f);
        BM.Stamina = PlayerPrefs.GetFloat("Stamina", 100);
        BM.StaminaDecrease = PlayerPrefs.GetFloat("StaminaDecrease", 10); //basmayı bıraktığında stamina azalışını artırma.
        maxStamina = BM.Stamina;
        baseSpeed = BM.Speed;
        decreaseStaminaOnMouseUp = BM.StaminaDecrease;
        fillAmount = 0f;
    }

    private bool firstClick = false;
    void Update()
    {
        if (GameManager.Instance.Gamestate == GameManager.GAMESTATE.Ingame)
        {
            if (Input.GetMouseButtonDown(0))
                _running = true;
            if(Input.GetMouseButton(0))
                PressDown();
            if (Input.GetMouseButtonUp(0))
            {
                _running = false;
                if (firstClick)
                    decreaseStamina += decreaseStaminaOnMouseUp;
                firstClick = true;
            }
            PressUp();
            GetFill();
            GetSweatParticle();
        }
    }

    void PressDown()
    {
        BM.Stamina -= Time.deltaTime * decreaseStamina;
        if (BM.Stamina <= decreaseSpeedThreshold)
        {
            if (BM.Speed > baseSpeed * 0.75f)
                BM.Speed -= Time.deltaTime * decreaseSpeed;
            if (BM.Stamina <= 0)
                Lose();
        }
    }

    void PressUp()
    {
        if (!_running && BM.Stamina <= maxStamina)
        {
            BM.Stamina += Time.deltaTime * increaseStamina;
            if (BM.Stamina >= decreaseSpeedThreshold)
                BM.Speed = baseSpeed;
            else if (BM.Stamina <= 75 && BM.Speed <= baseSpeed)
                BM.Speed += Time.deltaTime * increaseSpeed;
        }
    }
    
    void Lose()
    {
        playerParent.SetActive(false);
        Instantiate(ragDoll, transform.position, Quaternion.identity);
        if (GameManager.Instance.taptic)
            Taptic.Heavy();
    }

    void GetFill()
    {
        if (PlayerController.Instance.isLevelEnd)
            fillAmount = 0;
        else
        {
            var fill = BM.Stamina / maxStamina;
            fillAmount = 1f - fill;
        }
        playerMat.SetFloat("_Fill", fillAmount);
    }

    void GetSweatParticle()
    {
        if (BM.Stamina <= decreaseSpeedThreshold && canSweat)
        {
            sweatParticle.SetActive(true);
            if (canScale)
            {
                canScale = false;
                sweatScaleTransform.DOScale(Vector3.one * sweatScale, breathingTime).OnComplete(() =>
                {
                    sweatScaleTransform.DOScale(_startScale, breathingTime).OnComplete(() =>
                    {
                        canScale = true;
                    });
                });
            }
        }
        else
            sweatParticle.SetActive(false);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Obstacle"))
        {
            Lose();
        }
    }
}
