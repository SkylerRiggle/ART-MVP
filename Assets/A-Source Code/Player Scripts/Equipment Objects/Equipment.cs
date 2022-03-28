using UnityEngine;

[CreateAssetMenu(fileName = "NewEquipment", menuName = "Create New Equipment")]
public class Equipment : ScriptableObject
{
    [SerializeField] private MouseCursor _cursor = null;

    public MouseCursor cursor { get { return _cursor; } }
}
