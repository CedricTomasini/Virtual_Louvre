
using UnityEngine;
using System;
using UnityEngine.UI;

[Serializable]
public class Painting : utility
{
    
    public Text author;
    public float depth;
    public Rigidbody rb;
    public GameObject paint;
    public float wallHeight=10;
    public float wallLength;
    public Vector3 wallCenter;
    public Painting_info info;
    public Texture texture;
    public Material current_mat;



    public void spawn(char c)
    {
        depth = 1;

        Vector3 v = VAbs(direction(c)) * depth;
        
        v.y = info.height;
        if (v.z != 0)
        {
            v.z = info.width;
            v.x = depth;
            
        }
        else
        {
            v.x = info.width;
            v.z = depth;
        }
        transform.localScale = v;
        if (info.cardi == 'n' /*|| info.cardi == 's'*/)
        {
            transform.RotateAround(transform.position, Vector3.up, 180);
        }
        SetTexture();

    }
    public void SetTexture()
    {

       //set the texture from the data path stored in the structure 

        Texture texture = Resources.Load<Texture>(info.image_path);
        current_mat.EnableKeyword("_DETAIL_MULX2");
        current_mat.EnableKeyword("_NORMALMAP");
        current_mat.EnableKeyword("_METALLICGLOSSMAP");

        current_mat = paint.GetComponent<Renderer>().material;
        current_mat.SetTexture(info.title, texture);
        GetComponent<Renderer>().material=current_mat;

        current_mat.mainTexture = texture;
        current_mat.SetTexture("_DetailAlbedoMap", texture);
       

    }
    public void place(Vector3 pos)
    {
        transform.position = pos;
    }
    

     
}

