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
        if (wait > 180)
            text.enabled = false;
    }
    public void DisplayTitle()
    {
        text = GetComponent<Text>();

        text.enabled = true;
        Debug.Log("i animate "+ text);
        wait = 0;
        //animator.enabled = true;
        //Animator.Play(state, layer, normalizedTime);
       // animator = GetComponent<Animator>();
        //animator.enabled = false;
        
        //Debug.Log(animator.)
        //animator.SetBool("roomChange", true);
        
        
        //animator.SetBool
        //animator.Play("TitleAnimation", -1, 0f);
        //animator.GetCurrentAnimatorStateInfo(0);
        //
    }
}
