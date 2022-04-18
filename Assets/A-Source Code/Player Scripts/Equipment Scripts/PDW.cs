using UnityEngine;

public class PDW : Firearm
{
    [SerializeField] private GameEvent shockEvent = null;
    [SerializeField] private ParticleSystem muzzleFlash = null;
    [SerializeField] private Animation flashAnimation = null;

    [SerializeField] private float fireRate = 2;
    private float timeTillFire;

    private bool isFiring = false;

    public override void Fire()
    {
        if (!isFiring && Time.time >= timeTillFire)
        {
            shockEvent.Invoke();
            muzzleFlash.Play();
            flashAnimation.Play();
            currentAngle = maxAngle;

            timeTillFire = Time.time + (1 / fireRate);
            isFiring = true;
        }
    }

    public override void StopFire() => isFiring = false;

    public override void Reload()
    {
        return;
    }

    private void OnDrawGizmos()
    {
        float rad = (currentAngle * Mathf.Deg2Rad) / 2;
        Vector2 a = new Vector2(Mathf.Sin(rad), Mathf.Cos(rad));

        a = transform.InverseTransformVector(a);
        a *= 10;

        Vector2 b = Vector2.Reflect(a, transform.up);

        Gizmos.DrawLine(transform.position, (Vector2)transform.position + a);
        Gizmos.DrawLine(transform.position, (Vector2)transform.position + b);
    }
}
