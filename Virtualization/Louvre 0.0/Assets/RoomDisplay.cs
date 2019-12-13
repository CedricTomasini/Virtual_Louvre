using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class RoomDisplay : utility
{
    public Text title;
    bool titleFade=false;
    public GameObject fade;
    float fadeValue;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
     if(roomChange)
        {
            title.text ="room :\n" +roomName;
            titleFade = true;
            fadeValue = 1;
        }
        if (titleFade)
        {

            //fade.GetComponent<Material>().alpha; 
           // fadeValue -= 0.01;

        }
    }
}
