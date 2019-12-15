using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TitleFading : utility
{
    //public Animator animator;
    public bool boolChange;
    public int wait=0;
    public Text text;
    
    // Start is called before the first frame update
    void Start()
    {
        //animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        wait++;
        //animator.SetInteger("wait", wait++);
        //if (wait > 180)
         //   text.enabled = false;
    }
    public void DisplayTitle()
    {
        text = GetComponent<Text>();

        text.enabled = true;

        wait = 0;
        
    }
}
