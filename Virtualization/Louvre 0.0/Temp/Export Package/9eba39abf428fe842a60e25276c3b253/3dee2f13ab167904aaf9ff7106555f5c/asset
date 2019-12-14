using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Vector3 offset;
    public Transform player;
    public float rotationSpeed = 1.0f;
    // Start is called before the first frame update
    void Start()
    {
        transform.position = player.position + offset;
    }

 
    // Update is called once per frame
    void Update()
    {

        //transform.position = player.position+offset;

        

        float rotationMouse = Input.GetAxis("Mouse Y") * 10 * rotationSpeed;
        rotationMouse *= Time.deltaTime;
       

       

        // Rotate around our x-axis
        //transform.Rotate(rotationH, 0, 0);
        transform.Rotate(-rotationMouse, 0, 0);

    }
}