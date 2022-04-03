using UnityEngine;

public abstract class Equipment : MonoBehaviour
{
    [SerializeField] private MouseCursor _cursor = null;
    public MouseCursor cursor { get { return _cursor; } }

    public abstract void Fire(); 
    public abstract void Aim();
    public abstract void Reload();
}
