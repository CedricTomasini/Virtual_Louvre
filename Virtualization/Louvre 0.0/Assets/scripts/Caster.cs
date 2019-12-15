using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Caster : utility
{
    public Text text;
    public GameObject imageOriginal;
    public GameObject image;
    public Canvas canvas;
    public SpriteRenderer spriteRenderer;
    public GameObject player;
    
    bool q=false;
    
    void Update()
    {
        if (!q)
        {
            image.GetComponent<Picture>().DisableSprite();
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            image.GetComponent<Picture>().ToggleSprite();
            q = !q;
        }
        RaycastHit hit;
        string pastCollider="";
        float theDistance;
        transform.position = player.transform.position;
        Vector3 forward = player.transform.TransformDirection(Vector3.forward) * 10;
        Debug.DrawRay(transform.position, forward, Color.green);

        if (Physics.Raycast(player.transform.position,forward, out hit))
        {
            theDistance = hit.distance;
            string newColider = hit.collider.gameObject.name;
            if (pastCollider.Equals(newColider))
            {
                image.GetComponent<Picture>().DisableSprite();
            }
            pastCollider = hit.collider.gameObject.name;
            if (hit.collider.gameObject.name == "Painting(Clone)")
            {
                text.text = hit.collider.gameObject.GetComponent<Painting>().info.author + " \n" +
                   hit.collider.gameObject.GetComponent<Painting>().info.title;
                if (q)
                {
                    image.GetComponent<Picture>()
                        .SetImage(hit.collider.gameObject.GetComponent<Painting>().info.image_path);
                    image.GetComponent<Resize>()
                        .Rescale(hit.collider.gameObject.GetComponent<Painting>().info.width,
                        hit.collider.gameObject.GetComponent<Painting>().info.height);

                    image.GetComponent<Picture>().EnableSprite();
                }
            }
            else
            {
                q = false;
                image.GetComponent<Picture>().DisableSprite();
                text.text = "Room : " + Name;
            }

        }
        else
        {
            image.GetComponent<Picture>().DisableSprite();
            q = false;
        }
    }

}
