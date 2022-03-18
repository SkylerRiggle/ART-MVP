using UnityEngine;

public class PlayerGameInput : MonoBehaviour
{
    private Camera mainCamera;

    #region Input Names
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

    private void Awake() => mainCamera = Camera.main;

    private void Update()
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
}
