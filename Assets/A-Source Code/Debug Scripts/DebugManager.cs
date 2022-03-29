using System.Collections;
using UnityEngine;

public class DebugManager : Singleton<DebugManager>
{
    #region Debug Local Properties
    private DebugConsole console;

    private bool showConsole = false;
    private bool isUnlocked = false;

    private float lastTimeScale = 0;
    #endregion

    #region Debug Access
    [Header("Debug Access:")]
    [SerializeField] private string consolePassword = "purplegengar1";
    [SerializeField] private KeyCode consoleKey = KeyCode.BackQuote;
    #endregion

    #region Console Preferences
    [Header("Console Preferences:")]
    [SerializeField] private Color textColor = Color.green;
    [SerializeField] private Color feedbackColor = Color.white;
    [SerializeField] private Color backgroundColor = new Color(0, 0, 0, 0.5f);
    #endregion

    #region Debug Features
    [Header("Debug Features Settings:")]
    [SerializeField] private float fpsDelay = 0.1f;
    private float fps;
    private int fpsCap = int.MaxValue;

    private bool _capFPS = false;
    public bool capFPS{ get { return _capFPS; } }

    private bool _displayFPS = false;
    public bool displayFPS{ get { return _displayFPS; } }

    private bool _displayExposure = false;
    public bool displayExposure { get { return _displayExposure; } }
    #endregion

    private void Awake()
    {
        //Unity singleton pattern.
        Initialize();

        //Create and style the debug console.
        console = new DebugConsole();
        console.CreateStyles(textColor, feedbackColor, backgroundColor);
    }

    private void OnGUI()
    {
        //Check for console toggle input.
        if (Event.current.keyCode == consoleKey && Event.current.type == EventType.KeyDown) {
            showConsole = !showConsole;

            float timeScale = Time.timeScale;
            Time.timeScale = lastTimeScale;
            lastTimeScale = timeScale;
        }

        //Display console check.
        if (showConsole) {
            if (isUnlocked) {console.DisplayConsole();} //Console Display
            else {isUnlocked = console.DisplayPasswordPanel(consolePassword); return;} //Console Password Protection
        }

        //Display the current Frames Per Second.
        if (_displayFPS) {console.DisplayFPS("FPS: " + fps.ToString("00"));}

        //Display the player's exposure values.
        if (_displayExposure) {console.DisplayExposure("Visual: " + PlayerManager.visualExposure.ToString("00") + '\n' + 
            "Audio: " + PlayerManager.audibleExposure.ToString("00"));}
    }

    public void ToggleFPS(bool toggle) 
    {
        //Toggle the fps boolean and start measuring the fps.
        _displayFPS = toggle;
        StartCoroutine(MeasureFPS());
    }

    private IEnumerator MeasureFPS() 
    {
        //The fps for the game is equivalent to the inverse of the time between frames.
        //This is measured at a delay to help with readability on high performance machines.
        while (_displayFPS) {
            fps = 1.0f / Time.deltaTime;
            yield return new WaitForSeconds(fpsDelay);
        }

        //Return null.
        yield return null;
    }

    public void ToggleFPSCap(bool toggle) => ToggleFPSCap(toggle, fpsCap);

    public void ToggleFPSCap(bool toggle, int cap) 
    {
        //Set the incoming values.
        _capFPS = toggle;
        fpsCap = cap;

        //Cap the framerate as needed.
        if (_capFPS) {
            Application.targetFrameRate = fpsCap;
            return;
        }

        //Otherwise, set the target frame rate to the maximum.
        Application.targetFrameRate = int.MaxValue;
    }

    public void ToggleExposure(bool toggle) => _displayExposure = toggle;
}
