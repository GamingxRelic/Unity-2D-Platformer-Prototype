using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalGun : MonoBehaviour
{
    [SerializeField] private SpriteRenderer sprite;
    [SerializeField] private float ray_distance = 5f;
    [SerializeField] private Vector2 offset = new Vector2(0.0f, 0.5f);
    [SerializeField] private LayerMask hit_layers;
    [SerializeField] private GameObject blue_portal_prefab;
    [SerializeField] private GameObject orange_portal_prefab;
    [SerializeField] private Transform gun_tip;
    [SerializeField] private LineRenderer line;
    AudioSource audio;

    private Portal blue_portal = null, orange_portal = null;


    void Start()
    {
        line.positionCount = 2;    
        audio = gameObject.GetComponent<AudioSource>();
    }

    void Update()
    {
        if(Input.GetMouseButtonDown(0)) {
            ShootPortal(true);
        } 
        else if(Input.GetMouseButtonDown(1)) {
            ShootPortal(false);
        }

        PortalGunFaceMouse();
        PortalGunDrawLine();
    }

    void PortalGunFaceMouse() {
        Vector3 mouse_world_position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 dir_to_mouse = mouse_world_position - transform.position;


        float angle = Mathf.Atan2(dir_to_mouse.y, dir_to_mouse.x) * Mathf.Rad2Deg;

        if(angle > 90f || angle < -90f) {
            sprite.flipY = true;
        } else {
            sprite.flipY = false;
        }

        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }

    void PortalGunDrawLine() {
        Vector2 mouse_world_position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 dir_to_mouse = (mouse_world_position - ( (Vector2)transform.position + offset ) ).normalized;
        RaycastHit2D hit = Physics2D.Raycast(transform.position + new Vector3(0f, 0.5f, 0f), dir_to_mouse, 500f, hit_layers);

        if (hit.collider != null)
        {
            Vector3 end_position = hit.point;
            end_position.z = -1f;
            line.SetPosition(1, end_position);
        }

        Vector3 start_position = gun_tip.transform.position;
        start_position.z = -1f;

        line.SetPosition(0, start_position);

        if(hit.distance <= ray_distance && hit.collider.gameObject.layer == 8) {
            line.startColor = Color.green;
            line.endColor = Color.green;
        } else {
            line.startColor = Color.red;
            line.endColor = Color.red;
        }
    }

    void ShootPortal(bool shoot_blue) {
        Vector2 mouse_world_position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 dir_to_mouse = (mouse_world_position - ( (Vector2)transform.position + offset ) ).normalized;

        RaycastHit2D hit = Physics2D.Raycast(transform.position + new Vector3(0f, 0.5f, 0f), dir_to_mouse, ray_distance, hit_layers);

        if (hit.collider != null)
        {
            if(hit.collider.gameObject.layer != 8) {
                return;
            }
            GameObject new_portal;
            // new_portal = Instantiate(blue_portal_prefab);

            if(shoot_blue) {
                new_portal = Instantiate(blue_portal_prefab);
                
                if(blue_portal != null) {
                    Destroy(blue_portal.gameObject);
                    blue_portal = null;
                }

                blue_portal = new_portal.GetComponent<Portal>();

                if(orange_portal != null) {
                    orange_portal.other_portal = blue_portal;
                    blue_portal.other_portal = orange_portal;
                }

            } 
            else {
                new_portal = Instantiate(orange_portal_prefab);
                
                if(orange_portal != null) {
                    Destroy(orange_portal.gameObject);
                    orange_portal = null;
                }

                orange_portal = new_portal.GetComponent<Portal>();

                if(blue_portal != null) {
                    orange_portal.other_portal = blue_portal;
                    blue_portal.other_portal = orange_portal;
                }

            }

            // Play audio
            audio.Play();

            // Rotate the portal to align with the surface normal
            Vector2 surface_normal = hit.normal;
            float angle = Mathf.Atan2(surface_normal.y, surface_normal.x) * Mathf.Rad2Deg;
            new_portal.transform.rotation = Quaternion.Euler(0, 0, angle - 90f); // Subtract 90 to align properly

            new_portal.transform.position = hit.point;

            new_portal.transform.SetParent(hit.collider.transform);
        }
    }

    
    public void ClearPortals() {
        Debug.Log("clera portals");
        if(blue_portal != null) {
            Destroy(blue_portal.gameObject);
            blue_portal = null;
        }

        if(orange_portal != null) {
            Destroy(orange_portal.gameObject);
            orange_portal = null;
        }   
    }
}