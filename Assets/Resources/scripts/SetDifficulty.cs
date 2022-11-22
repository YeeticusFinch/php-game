using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetDifficulty : MonoBehaviour
{

    public int difficulty;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Fire1"))
        {
            //Debug.Log("Click");
            if (GetComponent<BoxCollider2D>().bounds.Contains(new Vector3(Main.mouseWorldPosition.x, Main.mouseWorldPosition.y, 0)))
            {
                Debug.Log("Selected " + name);
                if (difficulty == -4)
                {
                    Main.diff = 4;
                    Main.max_ram = 16;
                    Main.ram = 16;
                }
                else
                    Main.diff = difficulty;
                Main.StartGame();
            }
        }
    }
}
