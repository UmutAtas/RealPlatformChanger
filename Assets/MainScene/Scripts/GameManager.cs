using Happy.Analytics;
using NaughtyAttributes;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : Singleton<GameManager>
{
    public float CountDown = 2f;
    int asyncSceneIndex = 2;
    public bool taptic = true;
    
    [SerializeField] private TextMeshProUGUI rewardText;
    private bool finishPanel;
    
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
        HappyAnalytics.LevelStartEvent(PlayerPrefs.GetInt("Level", 1));
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
        GetRewardMoneyAmount();
    }
    void GameOver()
    {
        CountDown -= Time.deltaTime;
    }
    public void RestartButton()
    {
        SceneManager.UnloadSceneAsync(asyncSceneIndex);
        SceneManager.LoadSceneAsync(asyncSceneIndex, LoadSceneMode.Additive);
        HappyAnalytics.LevelFailEvent(PlayerPrefs.GetInt("Level", 1));
        Gamestate = GAMESTATE.Start;
        CountDown = 2;
        DestroyParticles();
        finishPanel = false;
    }
    public void NextLevelButton()
    {
        if (SceneManager.sceneCountInBuildSettings == asyncSceneIndex + 1)
        {
            SceneManager.UnloadSceneAsync(asyncSceneIndex);
            asyncSceneIndex = 2;
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
        UIManager.Instance.SetLevel(1);
        PlayerPrefs.SetInt("SaveScene",asyncSceneIndex);
        HappyAnalytics.LevelCompleteEvent(PlayerPrefs.GetInt("Level", 1));
        Gamestate = GAMESTATE.Start;
        CountDown = 2;
        DestroyParticles();
        finishPanel = false;
    }

    public void StartInGame()
    {
        Gamestate = GAMESTATE.Ingame;
    }

    private void DestroyParticles()
    {
        foreach (var particle in ButtonManager.Instance.upgradeParticleList)
        {
            Destroy(particle);
        }
        ButtonManager.Instance.upgradeParticleList.Clear();
    }

    private void GetRewardMoneyAmount()
    {
        if (!finishPanel)
        {
            var rewardAmount = Random.Range(PlayerPrefs.GetInt("minNextLevelRewardAmount", 25),
                PlayerPrefs.GetInt("maxNextLevelRewardAmount", 75));
            rewardText.text = "+ " + rewardAmount;
            UIManager.Instance.SetCoin(rewardAmount);
            finishPanel = true;
        }
    }

    #endregion
    void OnValueChanged()
    {
        Gamestate = _gamestate;
    }
}