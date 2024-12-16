using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    public Portal other_portal;
    public Transform teleport_point;

    AudioSource audio;

    void Start()
    {
        audio = gameObject.GetComponent<AudioSource>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        
        if(other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("CompanionCube") || other.gameObject.CompareTag("Enemy")) {

            if (other_portal != null)
            {
                audio.Play();

                GameObject other_obj = other.gameObject;
                Rigidbody2D rb = other_obj.GetComponent<Rigidbody2D>();

                // Teleport player
                other_obj.transform.position = other_portal.teleport_point.position;

                // Get incoming velocity
                Vector2 incoming_velocity = rb.velocity;

                // Transform velocity based on portal orientations
                Vector2 exit_normal = GetPortalNormal();
                Vector2 entry_normal = other_portal.GetPortalNormal();

                // Reflect and align velocity to the exit portal
                Vector2 exit_velocity = TransformVelocity(incoming_velocity, entry_normal, exit_normal);

                rb.velocity = exit_velocity;

            }

        }
    }




    // Get the normal vector based on portal side
    public Vector2 GetPortalNormal()
    {
        Vector2 normal = (teleport_point.position - transform.position).normalized;
        return normal;
    }

    // Transform the velocity based on portal normals
    Vector2 TransformVelocity(Vector2 velocity, Vector2 entryNormal, Vector2 exitNormal)
    {
        // Reflect velocity around the entry normal
        Vector2 reflectedVelocity = Vector2.Reflect(velocity, entryNormal);

        // Align reflected velocity with the exit portal's orientation
        float angle = Vector2.SignedAngle(entryNormal, exitNormal);
        return Quaternion.Euler(0, 0, angle) * reflectedVelocity;
    }
}
