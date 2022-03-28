using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewGameEvent", menuName = "Create New Game Event")]
public class GameEvent : ScriptableObject
{
    private HashSet<GameEventListener> listeners = new HashSet<GameEventListener>();

    public void Invoke()
    {
        foreach (GameEventListener listener in listeners)
        {
            listener.Invoke();
        }
    }

    public void Register(GameEventListener newListener) => listeners.Add(newListener);
    public void DeRegister(GameEventListener listener) => listeners.Remove(listener);
}
