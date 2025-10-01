using UnityEngine;

public class ExplosionEffect : MonoBehaviour
{
    public void PerformExplosion(Vector3 position, float radius, float force)
    {
        Collider[] colliders = Physics.OverlapSphere(position, radius);

        foreach (Collider hit in colliders)
        {
            Rigidbody rigidbody = hit.GetComponent<Rigidbody>();

            if (rigidbody != null)
                rigidbody.AddExplosionForce(force, position, radius, 1f, ForceMode.Impulse);
        }
    }
}