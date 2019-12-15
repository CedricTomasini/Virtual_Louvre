using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Resize : MonoBehaviour
{
    float width;
    float height;
    public RectTransform rectTransform;
    public RectTransform canvasTransform;
    void Start()
    {
        
    }
    public void Rescale(float w,float h)
    {
        width = w;
        height = h;
        float setHeight = 0;
        float setWidth = 0;
        float ratioH = 0;
        float ratioV = 0;
        ratioH = (width / height);
        ratioV = (height / width);
        if (width > height) {
            setWidth =  canvasTransform.sizeDelta.x*(50f / 100f) ;
            setHeight = ratioV * setWidth;
            
        }
        else
        {
            setHeight = (80f / 100f) * canvasTransform.sizeDelta.y;
            setWidth = ratioH * setHeight;
        }

        rectTransform.sizeDelta = new Vector2(setWidth, setHeight);
    }

    void Update()
    {
        
    }
}
