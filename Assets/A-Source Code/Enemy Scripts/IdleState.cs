using UnityEngine;

public class IdleState : StateNode
{
    public override StateNode RunCurrentState()
    {
        transform.rotation = Quaternion.Euler(transform.eulerAngles + Vector3.forward * 3);
        return this;
    }
}
