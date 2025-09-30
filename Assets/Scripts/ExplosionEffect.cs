using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionEffect : MonoBehaviour
{
    public void PerformExplosion(Vector3 position, float radius, float force)
    {
        Collider[] colliders = Physics.OverlapSphere(position, radius);

        foreach (Collider hit in colliders)
        {
            Rigidbody rb = hit.GetComponent<Rigidbody>();

            if (rb != null)
                rb.AddExplosionForce(force, position, radius, 1f, ForceMode.Impulse);
        }
    }
}