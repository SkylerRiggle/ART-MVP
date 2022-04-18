using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public abstract class Firearm : MonoBehaviour
{
    [SerializeField] private MouseCursor _cursor = null;
    public MouseCursor cursor { get { return _cursor; } }

    private bool isAiming;
    private float angleOffset, moveDilation;
    protected float currentAngle, maxAngle;
    [SerializeField] private float minAngle = 0;
    [SerializeField] private float aimSpeed = 2;
    [SerializeField] private Light2D[] aimLights = new Light2D[0];

    protected bool isReloading;

    private void Awake()
    {
        foreach (Light2D light in aimLights)
        {
            float outer = light.pointLightOuterAngle;
            float diff = outer - light.pointLightInnerAngle;

            if (outer > maxAngle)
            {
                maxAngle = outer;
            }

            if (diff > angleOffset)
            {
                angleOffset = diff;
            }
        }
    }

    public abstract void Fire();
    public abstract void StopFire();
    public abstract void Reload();

    public void SetAim(bool isAiming, float dilation)
    {
        this.isAiming = isAiming;
        moveDilation = dilation;
    }

    private void Update() => HandleAim(Time.deltaTime);

    private void HandleAim(float delta)
    {
        currentAngle = Mathf.Lerp(currentAngle, (isAiming ? minAngle : maxAngle) * moveDilation, aimSpeed * delta);
        foreach (Light2D light in aimLights)
        {
            light.pointLightOuterAngle = currentAngle;
            light.pointLightInnerAngle = currentAngle - angleOffset;
        }
    }
}
