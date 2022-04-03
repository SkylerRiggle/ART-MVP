using UnityEngine;

public abstract class StateNode : MonoBehaviour
{
    public abstract StateNode RunCurrentState();
}
