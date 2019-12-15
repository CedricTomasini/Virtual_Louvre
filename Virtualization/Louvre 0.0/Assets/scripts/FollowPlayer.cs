using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Vector3 offset;
    public Transform player;
    public float rotationSpeed = 1.0f;
    void Start()
    {
        transform.position = player.position + offset;
    }

    void Update()
    {
        float rotationMouse = Input.GetAxis("Mouse Y") * 5 * rotationSpeed;
        rotationMouse *= Time.deltaTime;
        transform.Rotate(-rotationMouse, 0, 0);
    }
}