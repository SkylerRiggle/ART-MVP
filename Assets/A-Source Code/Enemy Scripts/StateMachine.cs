using UnityEngine;

public class StateMachine : MonoBehaviour
{
    [SerializeField] private StateNode defaultState = null;
    private StateNode currentState;

    private void Awake() => currentState = defaultState;

    private void Update() => currentState = currentState?.RunCurrentState();
}
