using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oink : MonoBehaviour
{
    [SerializeField] private float DIRECTIONAL_THRESHOLD = 0f;
    [SerializeField] private float VELOCITY_THRESHOLD = 20f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("[" + collision.gameObject.name + "] entering speed: " + collision.relativeVelocity.magnitude);
        Debug.Log("[" + collision.gameObject.name + "] dot: " + (Vector2.Dot(collision.contacts[0].normal.normalized, 
                                                                             collision.relativeVelocity.normalized) >= DIRECTIONAL_THRESHOLD));
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
            Vector2 normal = hit.normal.normalized;  // normal in reference to oinker
            if (Vector2.Dot(normal, collision.relativeVelocity.normalized) >= DIRECTIONAL_THRESHOLD &&
                collision.relativeVelocity.magnitude >= VELOCITY_THRESHOLD)
            {
                //Debug.Log(collision.relativeVelocity.magnitude);
                Debug.DrawRay(hit.point, normal, Color.red, 3f);
                return true;
            }
        }
        return false;
    }

    private void OnDestroy()
    {
        Debug.Log("rip oinker");
    }
}
