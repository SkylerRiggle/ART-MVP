using UnityEngine;
using UnityEngine.Events;

public class GameEventListener : MonoBehaviour
{
    [SerializeField] private GameEvent gameEvent = null;
    [SerializeField] private UnityEvent actions = null;

    public void Invoke() => actions?.Invoke();

    private void Awake() => gameEvent?.Register(this);
    private void OnDestroy() => gameEvent?.DeRegister(this);
}
