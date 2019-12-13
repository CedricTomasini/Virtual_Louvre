using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
public class Wall :  PlaceIt
{
    public int iteration=5;
    //public List<Painting> test;
    public float padding;
    public float wallLength;
    public char cardi;
    public GameObject thatWall;
    //public PlaceIt placing;
    public GameObject paint;
    GameObject new_paint;
    public List<GameObject> paintings_obj;
    
   // public List<Painting> paintings;



    /*
    public Wall(char c)
    {
        thatWall = GameObject.CreatePrimitive(PrimitiveType.Cube);
        Debug.Log("wall created");
        cardi = c;
        paintings = null;
    }
    */


    public void add_painting(Painting_info p_info)
    {
        bool b = true;
        //for (int i = 0; i < paintings.Count; i++)
        //{
        //    if (p.Equals(paintings[i]))
        //    {
        //        b = false;
        //        i = paintings.Count;
        //    }
        //}
        if (b)
        {
            new_paint = Instantiate(paint);
            new_paint.GetComponent<Painting>().info = p_info;
            new_paint.GetComponent<Painting>().wallLength = wallLength;
            new_paint.GetComponent<Painting>().wallCenter = thatWall.transform.position;
            paintings_obj.Add(new_paint);
            //paintings.Add(p);
        }
    }

    public void set(Vector3 pos, Vector3 size, char name)
    {
        // Debug.Log("set called in walls");
        pos.y += 2;
        thatWall.transform.position = pos;
        thatWall.transform.localScale = size;
        thatWall.transform.rotation = Quaternion.Euler(0, 0, 0);
        cardi = name;
       
    }
    public void HangPaintings()
    {
        //.Log("hang painting ^s called");
        if (thatWall)
        {
            //thatWall.transform.se
            for (int i = 0; i < paintings_obj.Count; i++)
            {
                paintings_obj[i].GetComponent<Painting>().spawn(cardi);
            }
            Display();
            
        }
    }
    public void Display()
    {
        string displayStyle="inline";
        padding = 0.1f;
        float total = 0;
        // Debug.Log("displaying paintings");
        //float wall_depth = 2;
        //display in line if possible with padding 
        for (int i = 0; i < paintings_obj.Count; i++)
            total += paintings_obj[i].GetComponent<Painting>().info.width;
        if (total + 0.2 * paintings_obj.Count < wallLength)
            displayStyle = "inline";
        else
            displayStyle = "energie";

        float wall_depth = 2;
        Vector3 offset = new Vector3(0, 0, 0);
        offset += thatWall.transform.position;
        if (direction(cardi).z != 0)
        {
            offset -= new Vector3(wall_depth / 2, 0, 0)*Math.Sign(direction(cardi).z);
            //offset.z -= wallLength / 2;
           // offset -= new Vector3(wall_depth / 2, 0, 0);
        }
        else
        { 
            offset += new Vector3(0, 0, wall_depth / 2)*Math.Sign(direction(cardi).x);
            //offset.x -= wallLength / 2;
        }

        Program p =new Program();
        

        FreezePaint(displayStyle,offset);
        if (paintings_obj.Count != 0 && displayStyle=="energie")
            p.PlacePaint(paintings_obj, offset, direction(cardi), iteration);

        //thatWall.GetComponent<Rect>().
        //placing.GetComponent<PlaceIt>().
        // placing.




        // //display small paintings in columns 

        // //display bigger paintings in the center

        // displayStyle = "inline";
        // Vector3 p = thatWall.transform.position;
        // Vector3 offset=new Vector3(0,0,0);
        // offset += direction(cardi)*(wall_depth/2);

        // if (displayStyle == "inline")
        // {
        //     if (direction(cardi).x == 0)
        //     {
        //         offset.x -= wallLength / 2;
        //     }
        //     else
        //     {
        //         offset.z -= wallLength / 2;
        //     }
        // }
        // p += offset;
        // for (int i = 0; i < paintings_obj.Count; i++)
        // {
        //     if (displayStyle == "inline")
        //         InlineDisplay(p, i);

        //     //if (direction(cardi).x == 0)
        //     //{
        //     //    paintings_obj[i].GetComponent<Painting>().rb.constraints =
        //     //         RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionZ;
        //     //    paintings_obj[i].GetComponent<Painting>().rb.velocity.Set(1,0,0);
        //     //}
        //     //else
        //     //{
        //     //    paintings_obj[i].GetComponent<Painting>().rb.constraints =
        //     //         RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionX;
        //     //    paintings_obj[i].GetComponent<Painting>().rb.velocity.Set(0, 0, 1);
        //     //}

        //     paintings_obj[i].GetComponent<Painting>().place(p);
        // }

    }
    // Start is called before the first frame update

    public void FreezePaint(string displayStyle,Vector3 offset)
    {
        //Vector3 p = new Vector3(0, 0, 0);
        for (int i = 0; i < paintings_obj.Count; i++)
        {
            //sif (displayStyle == "inline")
            offset=InlineDisplay(offset, i);
            paintings_obj[i].GetComponent<Painting>().rb.constraints =
                    RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePosition;
            if (direction(cardi).x == 0)
            {
                paintings_obj[i].GetComponent<Painting>().rb.constraints =
                     RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePosition;

            }
            else
            {
                paintings_obj[i].GetComponent<Painting>().rb.constraints =
                     RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePosition;

            }
            paintings_obj[i].GetComponent<Painting>().place(offset);
        }
    }
    public void Destroy()
    {
        for (int i = 0; i < paintings_obj.Count; i++)
        {
            Destroy(paintings_obj[i]);
        }
        paintings_obj.Clear();
        Destroy(thatWall);
    }
    void Start()
    {
    }
    
    public Vector3 InlineDisplay(Vector3 p, int i)
    {
        if (direction(cardi).z == 0)
        {
            p.x = paintings_obj[i].GetComponent<Painting>().info.width / 2 + padding
                + wallLength * (i + 1) / (paintings_obj.Count+1);
            paintings_obj[i].GetComponent<Painting>().rb.constraints =
                     RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionZ;
            if (p.x > wallLength / 2)
            {
                Debug.Log("erreur");
               // p.x = -wallLength / 2;
            }
        }
        else
        {
            p.z = paintings_obj[i].GetComponent<Painting>().info.width / 2 + padding
                + wallLength * (i + 1) / (paintings_obj.Count+1);
            paintings_obj[i].GetComponent<Painting>().rb.constraints =
                     RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionX;
            if (p.z > wallLength / 2) ;
                //p.z = -wallLength / 2;
        }
        return p;
    }
  

    // Update is called once per frame
    void Update()
    {
        


    }
}
