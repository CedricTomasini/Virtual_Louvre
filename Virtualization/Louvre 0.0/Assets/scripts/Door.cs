using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : utility
{
    public DoorInfo info;
    //change level 
    // restart lvl with new data 
    public struct DoorInfo
    {
        public float p00;
        public float p01;
        public float p10;
        public float p11;
        public string name;

    }
    private void OnCollisionEnter(Collision collision)
    {
        if (timer > 30)
        {
            roomName = info.name;
            roomChange = true;
        }
    }
  
}
