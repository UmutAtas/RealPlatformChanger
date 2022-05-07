using System;
public class EventManager : Singleton<EventManager>
{
    public event Action OnMoved;
    public event Action<int> OnCoin;
    void Update()
    {
        if (GameManager.Instance.Gamestate == GameManager.GAMESTATE.Ingame)
        {
            OnMoved?.Invoke();
            OnCoin?.Invoke(0);
        }
    }
}
