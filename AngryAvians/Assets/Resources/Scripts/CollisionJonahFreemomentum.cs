using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionJonahFreemomentum : MonoBehaviour
{

    [SerializeField] private float DIRECTIONAL_THRESHOLD = 0f;
    [SerializeField] private float JONAHS_FREE_MOMENTUM_THRESHOLD = 30f;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Rigidbody2D otherRb = collision.gameObject.GetComponent<Rigidbody2D>();
        Vector2 normal = collision.contacts[0].normal.normalized;  // normal in reference to oinker
        float jonahsFreeMomentum = collision.relativeVelocity.magnitude *
                                   (otherRb != null ? otherRb.mass : 1);  // weighted speed

        //Debug.Log("[" + collision.gameObject.name + "] entering speed: " + collision.relativeVelocity.magnitude);
        //Debug.Log("[" + collision.gameObject.name + "] entering Jonah's Free Momentum: " + jonahsFreeMomentum);
        //Debug.Log("[" + collision.gameObject.name + "] dot: " + (Vector2.Dot(collision.contacts[0].normal.normalized, 
        //                                                                     collision.relativeVelocity.normalized) >= DIRECTIONAL_THRESHOLD));
        if (IsFatalVelocity(collision))
        {
            Destroy(this.gameObject);

        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (IsFatalVelocity(collision))
        {
            Destroy(this.gameObject);
        }
    }

    private bool IsFatalVelocity(Collision2D collision)
    {
        ContactPoint2D[] colliders = collision.contacts;
        foreach (ContactPoint2D hit in colliders)
        {
            Rigidbody2D otherRb = collision.gameObject.GetComponent<Rigidbody2D>();
            Vector2 normal = hit.normal.normalized;  // normal in reference to oinker
            float jonahsFreeMomentum = collision.relativeVelocity.magnitude *
                                       (otherRb != null ? otherRb.mass : 1);  // weighted speed

            if (Vector2.Dot(normal, collision.relativeVelocity.normalized) >= DIRECTIONAL_THRESHOLD &&
                jonahsFreeMomentum >= JONAHS_FREE_MOMENTUM_THRESHOLD)
            {
                Debug.DrawRay(hit.point, normal, Color.red, 3f);
                return true;
            }
        }
        return false;
    }
}
