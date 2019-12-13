using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;



public class Json : MonoBehaviour
{
    [Serializable]
    public class PlayerInfo
    {
        public string _id;
        public int index;
        public bool isActive;
        //public int useless;
        public void afunction()
        {
            //nothing
        }

    }
    [Serializable]
    public class RootObject
    {
        public PlayerInfo[] users;
    }
    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log("jsno");
        string apath = Application.dataPath + "\\generated.json";
        string jstring = File.ReadAllText(apath);
        RootObject myObject = JsonUtility.FromJson<RootObject>("{\"users\":" + jstring + "}");
        //Debug.Log("json test :"+myObject.users?[1]?.index);
        //Debug.Log("i'm in json");

    }

// Update is called once per frame
void Update()
    {
        
    }
}
