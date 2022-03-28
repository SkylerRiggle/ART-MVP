using UnityEngine;

[CreateAssetMenu(fileName = "NewCursor", menuName = "Create New Cursor")]
public class MouseCursor : ScriptableObject
{
    [SerializeField] private Sprite _normalSprite = null;
    [SerializeField] private Sprite _clickedSprite = null;

    public Sprite normalSprite { get { return _normalSprite; } }
    public Sprite clickedSprite { get { return _clickedSprite; } }
}
