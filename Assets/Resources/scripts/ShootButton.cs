using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootButton : MonoBehaviour
{
    public bool held = false;
    public bool crash = false;
    public bool show_on_low_credibility = false;
    public GameObject projectile;
    public int id;

    string temp_text;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!show_on_low_credibility || Main.credibility <= 5)
        {
            if (projectile.GetComponent<TextMesh>().text.Length < 2)
                projectile.GetComponent<TextMesh>().text = temp_text;
            bool yeet = false;
            if (Input.GetButton("Fire1"))
            {
                //Debug.Log("Click");
                if (GetComponent<BoxCollider2D>().bounds.Contains(new Vector3(Main.mouseWorldPosition.x, Main.mouseWorldPosition.y, 0)))
                {
                    //Debug.Log(name + " pressed");
                    held = true;
                    yeet = true;
                    //Debug.Log("Clicked " + name);
                    if (crash)
                    {
                        Main.started = false;
                        Application.Quit();
                    } else
                    {
                        Elephpant.projectile = projectile;
                        Elephpant.shooting = true;
                    }
                }
            }
            if (!yeet)
                held = false;
        } else if (projectile.GetComponent<TextMesh>().text.Length > 1)
        {
            temp_text = projectile.GetComponent<TextMesh>().text;
            projectile.GetComponent<TextMesh>().text = "";
        }
    }
}
