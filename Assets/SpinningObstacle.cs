using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinningObstacle : MonoBehaviour
{
    Rigidbody2D rb;
    [SerializeField] private float rotation_speed = 100f;

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }
    void FixedUpdate()
    {
        rb.MoveRotation(rb.rotation + rotation_speed * Time.fixedDeltaTime);
    }
}
