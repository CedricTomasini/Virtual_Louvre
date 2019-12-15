using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
public class Wall :  PlaceIt
{
    public int iteration;
    public float padding;
    public float wallLength;
    public char cardi;
    public GameObject thatWall;
    public GameObject paint;
    GameObject new_paint;
    public List<GameObject> paintings_obj;
    


    public void add_painting(Painting_info p_info)
    {
        bool b = true;
        
        if (b)
        {
            new_paint = Instantiate(paint);
            new_paint.GetComponent<Painting>().info = p_info;
            new_paint.GetComponent<Painting>().wallLength = wallLength;
            new_paint.GetComponent<Painting>().wallCenter = thatWall.transform.position;
            paintings_obj.Add(new_paint);
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
        if (thatWall)
        {
            
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
        for (int i = 0; i < paintings_obj.Count; i++)
            total += paintings_obj[i].GetComponent<Painting>().info.width;
        if (total + 0.2 * paintings_obj.Count < wallLength)
            displayStyle = "inline";
        else
        {
            displayStyle = "energie";
            Debug.Log("energie 1");

        }

        float wall_depth = 2;
        Vector3 offset = new Vector3(0, 0, 0);
        offset += thatWall.transform.position;
        if (direction(cardi).z != 0)
        {
            offset += new Vector3(wall_depth / 2, 0, 0)*Math.Sign(direction(cardi).x);
            
        }
        else
        { 
            offset -= new Vector3(0, 0, wall_depth / 2)*Math.Sign(direction(cardi).z);
        }

        Program p =new Program();
        

        FreezePaint(displayStyle,offset);

        if (paintings_obj.Count != 0 && displayStyle == "energie")
        {
            p.PlacePaint(paintings_obj, transform.position, direction(cardi), 1000);
           
        }
        else if (paintings_obj.Count != 0)
        {
            p.PlacePaint(paintings_obj, transform.position, direction(cardi), 20);
        }
    }

    public void FreezePaint(string displayStyle,Vector3 offset)
    {
        Vector3 p = new Vector3(0, 0, 0);
        for (int i = 0; i < paintings_obj.Count; i++)
        {
            
            p=InlineDisplay(p, i);
            paintings_obj[i].GetComponent<Painting>().rb.constraints =
                    RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePosition;
            paintings_obj[i].GetComponent<Painting>().place(p);
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
        float wallDepth = 2f;
        if (direction(cardi).z == 0)
        {
            p.x =thatWall.transform.position.x-wallLength/2+
                paintings_obj[i].GetComponent<Painting>().info.width / 2 + padding
                + wallLength * (i + 1) / (paintings_obj.Count+1);
            p.z = -(thatWall.transform.position.z+wallDepth / 2)*Math.Sign(direction(cardi).x);
            
        }
        else
        {
            p.z = thatWall.transform.position.z - wallLength / 2 +
                paintings_obj[i].GetComponent<Painting>().info.width / 2 + padding
                + wallLength * (i + 1) / (paintings_obj.Count+1);
            p.x = -(thatWall.transform.position.x - wallDepth / 2) * Math.Sign(direction(cardi).z);
            
           
        }
        p.y = thatWall.transform.position.y;
        return p;
    }
  

}
