using UnityEngine;
using UnityEngine.UI;

public class CursorManager : Singleton<CursorManager>
{
    private RectTransform rectTransform;
    private Image cursorImage;
    private MouseCursor currentCursor;

    [SerializeField] private MouseCursor defaultCursor = null;
    [SerializeField] private float percentageSize = 0.05f;

    private void Awake()
    {
        Initialize();
        SetCursor(defaultCursor);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
        {
            cursorImage.sprite = currentCursor.clickedSprite;
        } else if (Input.GetMouseButtonUp(0) || Input.GetMouseButtonUp(1))
        {
            cursorImage.sprite = currentCursor.normalSprite;
        }

        transform.position = Input.mousePosition;
    }

    public void ResizeCursor()
    {
        if (rectTransform == null) { rectTransform = GetComponent<RectTransform>(); }

        Vector2 screenSize = PixelatedView.screenSize;
        float scaledSize;

        if (screenSize.x  > screenSize.y)
        { scaledSize = screenSize.x * percentageSize; } 
        else { scaledSize = screenSize.y * percentageSize; }

        rectTransform.sizeDelta = Vector2.one * scaledSize;
    }

    public void SetCursor(MouseCursor newCursor)
    {
        if (newCursor == null) { SetCursor(defaultCursor); return; }

        if (cursorImage == null) { cursorImage = GetComponent<Image>(); }

        currentCursor = newCursor;
        cursorImage.sprite = currentCursor.normalSprite;
    }

    private void OnApplicationFocus(bool focus) => Cursor.visible = !focus;
}
