using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialTiling : MonoBehaviour
{
    public Material material;
    public GameObject wall;
    public GameObject room;
    // Start is called before the first frame update
    void Start()
    {
        material.mainTextureScale =new Vector2(wall.GetComponent<Wall>().wallLength/10,
            room.GetComponent<room>().wall_height/10);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
