using NaughtyAttributes;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : Singleton<GameManager>
{
    public float CountDown = 2f;
    int asyncSceneIndex = 1;
    public bool taptic = true;
    
    public int moneyAmount;
    public float stamina, speed;

    [SerializeField] private int newMoneyAmount;
    [SerializeField] private float newSpeed, newStamina;
    #region GameState
    public enum GAMESTATE
    {
        Start,
        Ingame,
        Finish,
        GameOver
    }
    [OnValueChanged("OnValueChanged")]
    [SerializeField]GAMESTATE _gamestate;

    public GAMESTATE Gamestate
    {
        get { return _gamestate;}
        set
        {
            _gamestate = value;
            UIManager.Instance.PanelController(_gamestate);
        }
    }
    #endregion
    void Start()
    {
        Gamestate = GAMESTATE.Start;
        if (!PlayerPrefs.HasKey("MoneyAmount"))
            PlayerPrefs.SetInt("MoneyAmount", moneyAmount);
        moneyAmount = PlayerPrefs.GetInt("MoneyAmount");
        
        if (!PlayerPrefs.HasKey("Stamina"))
            PlayerPrefs.SetFloat("Stamina", stamina);
        stamina = PlayerPrefs.GetFloat("Stamina");
        
        if (!PlayerPrefs.HasKey("Speed"))
            PlayerPrefs.SetFloat("Speed", speed);
        speed = PlayerPrefs.GetFloat("Speed");
    }
    void Update()
    {
        switch (_gamestate)
        {
            case GAMESTATE.Start:
                GameStart();
                break;
            case GAMESTATE.Ingame:
                GameIngame();
                break;
            case GAMESTATE.Finish:
                GameFinish();
                break;
            case GAMESTATE.GameOver:
                GameOver();
                break;
        }
        //if (Input.anyKeyDown && Gamestate == GAMESTATE.Start)
           // Gamestate = GAMESTATE.Ingame;
    }
    #region States
    
    void GameStart()
    {
        asyncSceneIndex = PlayerPrefs.GetInt("SaveScene",asyncSceneIndex);
        if(SceneManager.sceneCount < 2)
            SceneManager.LoadSceneAsync(asyncSceneIndex, LoadSceneMode.Additive);
    }
    void GameIngame()
    {
   
    }
    void GameFinish()
    {
        CountDown -= Time.deltaTime;
    }
    void GameOver()
    {
        CountDown -= Time.deltaTime;
    }
    public void RestartButton()
    {
        SceneManager.UnloadSceneAsync(asyncSceneIndex);
        SceneManager.LoadSceneAsync(asyncSceneIndex, LoadSceneMode.Additive);
        Gamestate = GAMESTATE.Start;
        CountDown = 2;
    }
    public void NextLevelButton()
    {
        if (SceneManager.sceneCountInBuildSettings == asyncSceneIndex + 1)
        {
            SceneManager.UnloadSceneAsync(asyncSceneIndex);
            asyncSceneIndex = 1;
            SceneManager.LoadSceneAsync(asyncSceneIndex, LoadSceneMode.Additive);
        }
        else
        {
            if (SceneManager.sceneCount > 1)
            {
                SceneManager.UnloadSceneAsync(asyncSceneIndex);
                asyncSceneIndex += 1;                
            }

            SceneManager.LoadSceneAsync(asyncSceneIndex, LoadSceneMode.Additive);
        }
        UIManager.Instance.SetLevel();
        PlayerPrefs.SetInt("SaveScene",asyncSceneIndex);
        Gamestate = GAMESTATE.Start;
        CountDown = 2;
    }

    public void MoneyUpgradeButton()
    {
        var currentMoney = PlayerPrefs.GetInt("MoneyAmount");
        PlayerPrefs.SetInt("MoneyAmount", currentMoney + newMoneyAmount);
        moneyAmount = PlayerPrefs.GetInt("MoneyAmount");
    }
    
    public void StaminaUpgradeButton()
    {
        var currentStamina = PlayerPrefs.GetFloat("Stamina");
        PlayerPrefs.SetFloat("Stamina", currentStamina + newStamina);
        stamina = PlayerPrefs.GetInt("Stamina");
    }
    
    public void SpeedUpgradeButton()
    {
        var currentSpeed = PlayerPrefs.GetFloat("Speed");
        PlayerPrefs.SetFloat("Speed", currentSpeed + newSpeed);
        speed = PlayerPrefs.GetInt("Speed");
    }
    
    #endregion
    void OnValueChanged()
    {
        Gamestate = _gamestate;
    }
}