using UnityEngine;

public class CameraMouseFollow : MonoBehaviour
{
    private Transform player; // Reference to the player object
    [SerializeField] private float mouse_follow_strength = 0.5f; // Strength of the mouse effect
    [SerializeField] private float max_distance = 15f; 


    private Camera main_camera;

    void Start()
    {
        player = PlayerMovement.instance.gameObject.transform;
        main_camera = Camera.main; // Get the main camera
        transform.position = player.position;
    }

    void Update()
    {
        if (player == null) return;

        // Get the mouse position in world space
        Vector3 mouse_world_position = main_camera.ScreenToWorldPoint(Input.mousePosition);
        mouse_world_position.z = 0; // Keep Z-axis at 0 for 2D

        // Interpolate between the player position and mouse position
        Vector3 target_position = Vector3.Lerp(player.position, mouse_world_position, mouse_follow_strength);

        // Calculate the direction and distance from the player to the target position
        Vector3 direction = target_position - player.position;
        float distance = direction.magnitude;

        // If the distance is greater than the max allowed distance, clamp the position
        if (distance > max_distance)
        {
            direction = direction.normalized * max_distance; // Scale the direction vector to the max distance
            target_position = player.position + direction; // Set target position within the max distance
        }

        // Update this object's position (the LookAt Target)
        transform.position = target_position;
    }
}
