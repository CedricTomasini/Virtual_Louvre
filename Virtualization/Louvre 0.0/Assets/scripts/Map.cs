﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Map : MonoBehaviour
{
   public GameObject here;
    public Material current_mat;
    public Image image;
    public SpriteRenderer sprite;

    void Start()
    {
        image.enabled = false;
    }
    public void ToggleSprite()
    {
        image.enabled = !image.enabled;
    }
    public void DisableSprite()
    {
        image.enabled = false;
        Debug.Log("image kill");
    }
    public void EnableSprite()
    {
        image.enabled = true;
        Debug.Log("image spawn");
    }

    public void SetImage(string path)
    {

        Texture texture = Resources.Load<Texture>(path);
        current_mat.EnableKeyword("_NORMALMAP");
        current_mat.EnableKeyword("_METALLICGLOSSMAP");



        current_mat = image.material;
        current_mat.SetTexture("name", texture);
        image.material = current_mat;
        current_mat.mainTexture = texture;
        current_mat.SetTexture("_DetailAlbedoMap", texture);
        current_mat.EnableKeyword("_DETAIL_MULX2");



    }
    // Update is called once per frame
   
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            Debug.Log("take out map");
            ToggleSprite();

        }

        here.GetComponent<Resize>().Rescale(1f, 0.9f);
            
    }
        

}
