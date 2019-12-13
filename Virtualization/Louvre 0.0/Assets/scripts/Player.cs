using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //Transform door;
    // Start is called before the first frame update
    Collider m_Collider;

    
        

    

    void Start()
    {
        transform.position = new Vector3(0, 5, 0);
        m_Collider = GetComponent<Collider>();
        m_Collider.enabled=(false);
        
    }
    public float speed = 10.0f;
    public float rotationSpeed = 100.0f;

    void Update()
    {
        float rotationV = 0;
        
        // Get the horizontal and vertical axis.
        // By default they are mapped to the arrow keys.
        // The value is in the range -1 to 1
        float translationV = Input.GetAxis("Vertical") * speed;
        float rotation = Input.GetAxis("Horizontal") * rotationSpeed;
        float rotationMouse = Input.GetAxis("Mouse X") *10* rotationSpeed;
        //if(Input.GetKey(KeyCode.D))
        //     rotationV=  rotationSpeed;
        //if (Input.GetKey(KeyCode.A))
        //    rotationV = -rotationSpeed;
        float translationFront = 0;
        if (Input.GetKey(KeyCode.S))
            translationFront = -speed;
        if (Input.GetKey(KeyCode.W))
            translationFront = speed;
        translationFront *= Time.deltaTime;

        float translationSide = 0;
        if (Input.GetKey(KeyCode.A))
            translationSide = -speed;
        if (Input.GetKey(KeyCode.D))
            translationSide = speed;
        translationSide *= Time.deltaTime;





        // Make it move 10 meters per second instead of 10 meters per frame...
        //translationV *= Time.deltaTime;
        rotation *= Time.deltaTime;
        rotationV *= Time.deltaTime;
        rotationMouse *= Time.deltaTime;


        // Move translation along the object's z-axis
        transform.Translate(translationSide, 0, 0);
        transform.Translate(0, 0, translationFront);


        // Rotate around our y-axis
        transform.Rotate(0, rotation, 0);
        transform.Rotate(0, rotationMouse, 0);


        if (Input.GetKeyDown(KeyCode.Space))
        {
            //Toggle the Collider on and off when pressing the space bar
            m_Collider.enabled = !m_Collider.enabled;

            //Output to console whether the Collider is on or not
            //Debug.Log("Collider.enabled = " + m_Collider.enabled);
        }
    }
    // Update is called once per frame
   
}
