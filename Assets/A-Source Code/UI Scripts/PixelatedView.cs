using UnityEngine;
using UnityEngine.UI;

public class PixelatedView : MonoBehaviour
{
    [SerializeField] private Camera worldCamera = null;
    [SerializeField] private FilterMode filterMode = FilterMode.Point;
    [SerializeField] [Range(0.01f, 1)] private float screenScale = 0.25f;

    private RawImage displayImage;
    private RenderTexture renderTexture;
    public static Vector2 screenSize;

    [SerializeField] private GameEvent resizeEvent = null;

    private void Awake()
    {
        //Grab the Raw Image component for displaying the scaled render texture.
        displayImage = GetComponent<RawImage>();

        //Set the initial render texture using the current screen size.
        SetTexture();
    }

    private void Update()
    {
        //Check if the screen has been resized and set the texture if needed.
        if (screenSize.x != Screen.width || screenSize.y != Screen.height)
        { SetTexture(); }
    }

    private void SetTexture()
    {
        //Read in the screen size.
        screenSize.x = Screen.width;
        screenSize.y = Screen.height;

        //Trigger the screen resize event.
        resizeEvent.Invoke();

        //Get the new render texture size.
        Vector2Int textureSize = new Vector2Int(Mathf.FloorToInt(screenSize.x * screenScale), Mathf.FloorToInt(screenSize.y * screenScale));

        //Create the new scaled render texture.
        renderTexture = new RenderTexture(textureSize.x, textureSize.y, 0);
        renderTexture.filterMode = filterMode;
        worldCamera.targetTexture = renderTexture;

        //Set the display image using the new texture.
        displayImage.texture = renderTexture;
    }
}
