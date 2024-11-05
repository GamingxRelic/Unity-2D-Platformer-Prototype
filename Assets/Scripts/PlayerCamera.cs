using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField] private Vector3 offset = Vector3.zero;
    [SerializeField] private float smooth_time;
    private Vector3 velocity = Vector3.zero;
    [SerializeField] Transform target;

    void Update()
    {
        Vector3 target_position = target.position + offset;
        transform.position = Vector3.SmoothDamp(transform.position, target_position, ref velocity, smooth_time);
    }
}
