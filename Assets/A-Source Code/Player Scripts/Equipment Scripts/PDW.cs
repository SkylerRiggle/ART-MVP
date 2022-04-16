using UnityEngine;

public class PDW : Firearm
{
    [SerializeField] private GameEvent shockEvent = null;

    public override void Fire()
    {
        shockEvent.Invoke();
    }

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
