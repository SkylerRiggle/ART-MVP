using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    #region External Components
    private PlayerGameInput playerInput;
    private Rigidbody2D playerRigidbody;
    #endregion

    #region Speed Control Variables
    [Header("Speed Control:")]
    [SerializeField] private float moveSpeed = 5;
    [SerializeField] private float lookSpeed = 3;
    [SerializeField] private float sprintMultiplier = 1.5f;
    [SerializeField] private float crouchMultiplier = 0.75f;
    #endregion

    #region Sprint Regeneration Control Variables
    [Header("Sprint Regeneration Control:")]
    [SerializeField] private float sprintTime = 6;
    [SerializeField] private float regenDelay = 3;
    [SerializeField] private float regenRate = 1;
    private float currentSprintTime;
    private bool isSprintRegenerating;
    #endregion

    private void Awake()
    {
        //Read in components needed for movement.
        playerInput = GetComponent<PlayerGameInput>();
        playerRigidbody = GetComponent<Rigidbody2D>();

        //Default the player's sprint time to full.
        currentSprintTime = sprintTime;
    }

    private void Update()
    {
        //Turn towards the mouse cursor.
        LookTowards(playerInput.mousePosition, Time.deltaTime);

        //Handle the player's sprint regeneration.
        if (!playerInput.sprintInput && currentSprintTime < sprintTime && !isSprintRegenerating)
        { StartCoroutine(HandleSprintRegeneration()); }
    }

    private void FixedUpdate() => Move(Time.fixedDeltaTime);

    private void LookTowards(Vector2 lookPoint, float delta) =>
        //Turn towards a point at a rate of some look speed.
        transform.up = Vector2.Lerp(transform.up, (lookPoint - (Vector2)transform.position).normalized, lookSpeed * delta);

    private void Move(float delta)
    {
        //Determine the player's current movement speed.
        float speed = moveSpeed;
        if (playerInput.sprintInput && currentSprintTime > 0) //Sprint check.
        { 
            speed *= sprintMultiplier;
            currentSprintTime = Mathf.Clamp(currentSprintTime - delta, 0, sprintTime);
        } 
        else if (playerInput.crouchInput) { speed *= crouchMultiplier; } //Crouch check.

        //Move the player in the direction of their input at a set speed.
        playerRigidbody.MovePosition((Vector2)transform.position + (playerInput.moveInput * speed * delta));
    }

    private IEnumerator HandleSprintRegeneration()
    {
        //Set the sprint regeneration boolean.
        isSprintRegenerating = true;

        //Wait to regenerate the player's sprint time.
        yield return new WaitForSeconds(regenDelay);

        //Regenerate the player's sprint time.
        while (!playerInput.sprintInput && currentSprintTime < sprintTime)
        {
            currentSprintTime = Mathf.Clamp(currentSprintTime + (regenRate * Time.deltaTime), 0, sprintTime);
            yield return null;
        }

        //Reset the sprint regeneration boolean.
        isSprintRegenerating = false;

        //Return null.
        yield return null;
    }
}
