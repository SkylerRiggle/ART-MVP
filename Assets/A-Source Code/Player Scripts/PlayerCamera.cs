using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField] private Transform target = null;
    [SerializeField] private float targetWeight = 5;

    private Camera mainCamera;
    [SerializeField] private float cursorWeight = 1;

    private Vector3 targetPosition;
    private float weightDivider;
    [SerializeField] private float cameraSpeed = 5;

    private void Awake()
    {
        //Gather a reference to the scenes main camera.
        mainCamera = Camera.main;

        //Set the z offset and weight divider for the camera's movement.
        targetPosition.z = transform.position.z;
        weightDivider = 1f / (targetWeight + cursorWeight);
    }

    private void FixedUpdate() => Move(Time.fixedDeltaTime);

    private void Move(float delta)
    {
        //Grab the cursor's position.
        Vector2 cursorPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);

        //Calculate the target position for the camera.
        targetPosition.x = ((target.position.x * targetWeight) + (cursorPosition.x * cursorWeight)) * weightDivider;
        targetPosition.y = ((target.position.y * targetWeight) + (cursorPosition.y * cursorWeight)) * weightDivider;

        //Move towards the target position.
        transform.position = Vector3.Lerp(transform.position, targetPosition, cameraSpeed * delta);
    }
}
