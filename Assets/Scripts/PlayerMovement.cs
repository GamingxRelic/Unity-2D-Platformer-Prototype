using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Player Components")]
    [SerializeField] Rigidbody2D rb;
    [SerializeField] PlayerCheckpointHandler checkpoint_handler;
    [SerializeField] AudioSource player_audio;
    [SerializeField] ParticleSystem particles;

    public bool alive = true;

    [Header("Movement")]
    private Vector2 movement;
    [SerializeField] float movement_speed, acceleration, deceleration;
    Vector2 target_velocity;

    [Header("Max Velocities")]
    [SerializeField] float max_fall_speed;

    [Header("Jumping")]
    [SerializeField] float jump_force = 50f;
    public bool is_grounded;
    float jump_timer_remaining;
    public float coyote_time_remaining;


    public int air_jumps;
    int jumps_left;
    [SerializeField] float jump_timer = 1f;

    // Delegates
    public delegate void OnJump();
    public OnJump on_jump;

    public delegate void OnDeath();
    public static OnDeath on_death;
    public delegate void OnRespawn();
    public OnDeath on_respawn;

    void Update()
    {
        GetMovementInput();
        ApplyMovement();
        HandleJump();
        ClampMovement();

        // Reset to last checkpoint
        if(Input.GetKeyDown(KeyCode.R)) {
            Teleport(GetRecentCheckpointPos(), true);
            Die();
            Respawn();
        }
    }

    void GetMovementInput() {
        if(!alive) return;

        float horizontal = Input.GetAxisRaw("Horizontal");
        target_velocity.x = horizontal * movement_speed;
    }

    void HandleJump() {
        if(!alive) return;

        if(Input.GetButtonDown("Jump")) {
            if(coyote_time_remaining > 0f) { // Coyote Jump
                Jump();
            }
            else if(jumps_left > 0 && !is_grounded) { // Air Jump
                Jump();
                particles.Play();
                jumps_left -= 1;
            } else { // Start Jump queue timer
                jump_timer_remaining = jump_timer;
            }
        }
            
        if(jump_timer_remaining > 0f) { // When the jump queue timer is running, check if the player touches the ground, if they do, make the player jump.
            if(is_grounded) {
                Jump();
                jump_timer_remaining = 0f;
                jumps_left = air_jumps;
            }

            jump_timer_remaining -= Time.deltaTime;
        }
    }

    void Jump() {
        if(!alive) return;
        
        rb.velocity = new Vector2(rb.velocity.x, jump_force);
        coyote_time_remaining = 0f;
        on_jump();
        player_audio.Play(); 
    }

    void ApplyMovement() {
        target_velocity.y = rb.velocity.y;

        if(target_velocity.x != 0f && alive) {
            rb.velocity = Vector2.Lerp(rb.velocity, target_velocity, acceleration * Time.deltaTime);
        } else {
            rb.velocity = Vector2.Lerp(rb.velocity, new Vector2(0f, rb.velocity.y), deceleration * Time.deltaTime);
        }
    }

    void ClampMovement() {
        float clamped_x = Mathf.Clamp(rb.velocity.x, -movement_speed, movement_speed);
        float clamped_y = Mathf.Clamp(rb.velocity.y, -max_fall_speed, max_fall_speed);
        rb.velocity = new Vector2(clamped_x, clamped_y);
    }

    void Teleport(Vector2 pos, bool reset_velocity) {
        if(reset_velocity) {
            rb.velocity = Vector2.zero;
        }

        transform.position = pos;
    }

    Vector2 GetRecentCheckpointPos() {
        return checkpoint_handler.recent_checkpoint_pos;
    }

    public void Die() {
        on_death();
    }

    public void Respawn() {
        on_respawn();
    }

    public void RespawnAtCheckpoint() {
        Teleport(GetRecentCheckpointPos(), true);
        Respawn();
        on_respawn();
    }
}
