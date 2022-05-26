using System;
public class EventManager : Singleton<EventManager>
{
    public event Action OnMoved;
    public event Action OnCoin;
    void Update()
    {
        OnCoin?.Invoke();
        if (GameManager.Instance.Gamestate == GameManager.GAMESTATE.Ingame)
        {
            OnMoved?.Invoke();
        }
    }
}
