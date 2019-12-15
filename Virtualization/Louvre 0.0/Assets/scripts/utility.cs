using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;


public class utility : MonoBehaviour
{

    public static int timer;
    public static bool roomChange;
    public static string roomName;
    public static string Name;
    [Serializable]
    public struct Painting_info
    {

        public string author;
        public string title;
        public float width;
        public float height;
        public string wall;
        public char cardi;
        public string image_path;

        public void transform()
        {
            char[] orientation = { 'o', 'e', 's', 'n' };
            if (wall.Length != 0)
            {
                wall = wall.ToLower();
                cardi = wall[0];
            }
            else cardi = orientation[UnityEngine.Random.Range(0, 3)];
            //doublee the size of the painting and converts them into meter 
            //if no width and height found default values are assigned
            if (width == 0)
                width = 2f;
            else
                width /= 50;

            if (height == 0)
                height = 1.4f;
            else
                height /= 50;


            if (image_path.Length != 0)
            {
                image_path = image_path.Remove(image_path.Length - 4);

            }


        }

    }
    [Serializable]
    public class Painting_tab
    {
        public Painting_info[] tab;

    }
    //gives the direction in the reference frame given the cardinal point
    static public Vector3 direction(char c)
    {

        switch (c)
        {
            case 'n':
            case 'N': return new Vector3(1, 0, 0);
            case 's':
            case 'S': return new Vector3(-1, 0, 0);
            case 'O':
            case 'o': return new Vector3(0, 0, 1);
            case 'e':
            case 'E': return new Vector3(0, 0, -1);
            default: Debug.Log("error detected, wrong char"); break;
        }
        return new Vector3(0, 0, 0);

        
        
    }
    // Start is called before the first frame update

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        timer++;
    }
    public Vector3 VAbs(Vector3 v)
    {
        if (v.x < 0)
            v.x *= -1;
        if (v.z < 0)
            v.z *= -1;
        return v;
    }
    
}
