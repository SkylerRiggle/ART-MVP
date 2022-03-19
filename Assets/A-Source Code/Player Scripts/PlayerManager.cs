using System.Collections;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class PlayerManager : MonoBehaviour
{
    private Camera mainCamera;

    #region Input Names
    [Header("Input Names:")]
    [SerializeField] private string horizontalAxis = "Horizontal";
    [SerializeField] private string verticalAxis = "Vertical";
    [SerializeField] private string sprintButton = "Sprint";
    [SerializeField] private string crouchButton = "Crouch";
    #endregion

    #region Input Store Variables
    private Vector2 _moveInput;
    private Vector2 _mousePosition;
    private bool _sprintInput;
    private bool _crouchInput;
    #endregion

    #region Input Get Methods
    public Vector2 moveInput { get { return _moveInput; } }
    public Vector2 mousePosition { get { return _mousePosition; } }
    public bool sprintInput { get { return _sprintInput; } }
    public bool crouchInput { get { return _crouchInput; } }
    #endregion

    #region Visual & Audible Exposure Scores
    private static float _visualExposure;
    private static float _audibleExposure;
    public static float visualExposure { get { return _visualExposure; } }
    public static float audibleExposure { get { return _audibleExposure; } }

    [Header("Exposure Controls:")]
    [SerializeField] private float delayPerVolume = 0.5f;
    [SerializeField] private float lightDetectionRadius = 20;
    [SerializeField] [Range(0, 3)] private int lightBlendStyle = 0;
    #endregion

    private void Awake() => mainCamera = Camera.main;

    private void Update()
    {
        //Read in player input.
        HandleInput();

        //Calculate the player's visual exposure.
        _visualExposure = CalculateVisualExposure();
    }

    private void HandleInput()
    {
        //Read in a normalized move input vector.
        _moveInput.x = Input.GetAxisRaw(horizontalAxis);
        _moveInput.y = Input.GetAxisRaw(verticalAxis);
        _moveInput = _moveInput.normalized;

        //Read in the mouse position in world coordinates.
        _mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);

        //Get the player's sprint and crouch input.
        _sprintInput = Input.GetButton(sprintButton);
        _crouchInput = Input.GetButton(crouchButton);
    }

    private float CalculateVisualExposure()
    {
        //Initialize the result to zero.
        float result = 0;

        //Grab all nearby colliders.
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, lightDetectionRadius);

        foreach (Collider2D collider in colliders)
        {
            //Calculate the light value for all surrounding lights.
            Light2D light;
            if (collider.TryGetComponent(out light))
            {
                float distance = Vector2.Distance(transform.position, light.transform.position);
                if (light.blendStyleIndex == lightBlendStyle && distance <= light.pointLightOuterRadius)
                {
                    result += light.intensity / distance;
                }
            }
        }

        //Return the calculated result.
        return result;
    }

    public void SetPlayerAudioExposure(float volume) => StartCoroutine(AudioExposureDelay(volume));

    private IEnumerator AudioExposureDelay(float volume)
    {
        //Increase the audible exposure.
        _audibleExposure += volume;

        //Wait for some time depending on the volume of the audio and reset the audible exposure.
        yield return new WaitForSeconds(delayPerVolume * volume);
        _audibleExposure -= volume;

        //Return null.
        yield return null;
    }
}
