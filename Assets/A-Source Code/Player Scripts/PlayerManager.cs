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
    [SerializeField] private string fireButton = "Fire";
    [SerializeField] private string reloadButton = "Reload";
    #endregion

    #region Input Get Methods
    public Vector2 moveInput { get { return ReadInputVector(horizontalAxis, verticalAxis); } }
    public Vector2 mousePosition { get { return mainCamera.ScreenToWorldPoint(Input.mousePosition); } }
    public bool sprintInput { get { return Input.GetButton(sprintButton); } }
    public bool crouchInput { get { return Input.GetButton(crouchButton); } }
    public bool fireInput { get { return Input.GetButton(fireButton); } }
    public bool reloadInput { get { return Input.GetButton(reloadButton); } }
    public Vector2 scrollInput { get { return Input.mouseScrollDelta; } }
    #endregion

    #region Visual & Audible Exposure Scores
    public float visualExposure { get { return CalculateVisualExposure(); } }
    private float _audibleExposure;
    public float audibleExposure { get { return _audibleExposure; } }

    [Header("Exposure Controls:")]
    [SerializeField] private float delayPerVolume = 0.5f;
    [SerializeField] private float lightDetectionRadius = 20;
    [SerializeField] [Range(0, 3)] private int lightBlendStyle = 0;
    #endregion

    private void Awake() => mainCamera = Camera.main;

    private Vector2 ReadInputVector(string xAxisName, string yAxisName) 
        => new Vector2(Input.GetAxisRaw(xAxisName), Input.GetAxisRaw(yAxisName)).normalized;

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
