using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public bool fly = false;
    public float age = 0;
    public float speed = 1;
    public int id;

    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        //GetComponent<BoxCollider2D>().isTrigger = true;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {

        if (Main.gameOver)
        {
            GameObject.Destroy(this.gameObject);
        }
        if (fly && Main.started)
        {
            if (gameObject.layer != 0)
            {
                //rb = gameObject.AddComponent<Rigidbody2D>();
                //rb.gravityScale = 0;
                gameObject.layer = 0;
                GetComponent<BoxCollider2D>().isTrigger = true;
                //rb.velocity = Vector3.right*speed;
            }
            age += Time.fixedDeltaTime;
            transform.position += Vector3.right * speed * (1.17f - Main.diff*0.15f);
        }
        if (age > 2)
            GameObject.Destroy(this.gameObject);
    }
}
