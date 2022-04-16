using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField] private Transform target = null;
    [SerializeField] private float targetWeight = 5;

    private Camera mainCamera;
    [SerializeField] private float cursorWeight = 1;

    private Vector3 targetPosition;
    private float weightDivider;
    [SerializeField] private float cameraSpeed = 5;

    [SerializeField] private float shockTransitionSpeed = 1.5f;

    [SerializeField] private VolumeProfile profile = null;
    private ChromaticAberration aberration = null;

    [SerializeField] private float defaultShake = 0.1f;
    [SerializeField] private float shockShake = 0.75f;
    private float currentShake;

    private void Awake()
    {
        //Gather a reference to the scenes main camera.
        mainCamera = Camera.main;

        //Set the z offset and weight divider for the camera's movement.
        targetPosition.z = transform.position.z;
        weightDivider = 1f / (targetWeight + cursorWeight);

        //Get the chromatic aberration filter.
        profile.TryGet(out aberration);
        aberration.intensity.Override(0);

        //Set the default screen shake offset.
        currentShake = defaultShake;
    }

    private void FixedUpdate() => Move(Time.fixedDeltaTime);

    private void Move(float delta)
    {
        //Grab the cursor's position.
        Vector2 cursorPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);

        //Calculate the target position for the camera.
        targetPosition.x = ((target.position.x * targetWeight) + (cursorPosition.x * cursorWeight)) * weightDivider;
        targetPosition.y = ((target.position.y * targetWeight) + (cursorPosition.y * cursorWeight)) * weightDivider;

        //Add the current screen shake offset.
        targetPosition.x += Mathf.Sin(Mathf.Cos(Time.time)) * currentShake;
        targetPosition.y += Mathf.Cos(Mathf.Sin(Time.time)) * currentShake;

        //Move towards the target position.
        transform.position = Vector3.Lerp(transform.position, targetPosition, cameraSpeed * delta);
    }

    public void ScreenShake() => StartCoroutine(ShockEffects(Time.deltaTime));

    private IEnumerator ShockEffects(float delta)
    {
        aberration.intensity.Override(1);
        currentShake = shockShake;

        while (currentShake > defaultShake + 0.01f || aberration.intensity.value > 0.01f)
        {
            float currentSpeed = shockTransitionSpeed * delta;
            currentShake = Mathf.Lerp(currentShake, defaultShake, currentSpeed);
            aberration.intensity.Interp(aberration.intensity.value, 0, currentSpeed);
            yield return null;
        }

        //Return null.
        yield return null;
    }
}
