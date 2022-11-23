using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Main main;
    public int hp = 10;
    public bool insta_lose = false;
    public bool only_low_cred = false;
    public float speed;

    public bool increase_cred = false;
    public bool lower_cred = false;

    Rigidbody2D rb;

    bool go = false;
    float used_ram = 0;

    public int[] vuln =
    {
        0, // Execute Operation
        0, // Crash Program
        0, // SQL Query
        0, // Log Service Incident
        0, // Log Secutiry Incident
        0, // Pay Ransom
        0, // Report to Department of Homeland Security
    };

    public string[] texts;

    public void Spawn()
    {
        used_ram = Random.Range(0, Mathf.Max(0.1f, 2 + 0.5f * Main.score + Main.diff));
        Main.ram -= used_ram;
        transform.position = new Vector3(12, Random.Range(-3f, 4f), 0);
        transform.localScale *= 1.5f;
        go = true;
        GetComponent<TextMesh>().text = texts[Mathf.Min(Random.Range(0, texts.Length), texts.Length - 1)];
        //GetComponent<BoxCollider2D>().isTrigger = true;
        rb = gameObject.AddComponent<Rigidbody2D>();
        rb.gravityScale = 0;
        rb.isKinematic = true;
        gameObject.layer = 0;
        Main.enemies++;
    }

    // Start is called before the first frame update
    void Start()
    {
        //GetComponent<BoxCollider2D>().isTrigger = true;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {

        if (Main.gameOver)
        {
            GameObject.Destroy(this.gameObject);
        }
        if (go && Main.started)
        {
            transform.position += Vector3.left * Time.fixedDeltaTime * speed * (1 + Main.diff*0.23f) * (1 + Main.score*0.03f) * 0.5f;
            if (hp <= 0)
            {
                Main.score++;
                Main.ram += used_ram;
                Main.enemies--;
                GameObject.Destroy(this.gameObject);
            }
            if (transform.position.x < -12)
            {
                Main.enemies--;
                if (lower_cred)
                    Main.credibility--;
                if (insta_lose)
                {
                    Main.started = false;
                    Main.gameOver = true;
                    //Application.Quit();
                }
                GameObject.Destroy(this.gameObject);
            }
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (!go || !Main.started)
            return;
        //Debug.Log("OnCollisionEnter2D");
        if (col.gameObject.tag.Equals("Projectile"))
        {
            handle_collision(col.gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (!go)
            return;
        //Debug.Log("OnCollisionEnter2D");
        if (col.gameObject.tag.Equals("Projectile"))
        {
            handle_collision(col.gameObject);
        } else if (col.gameObject.tag.Equals("Player"))
        {
            transform.position += Vector3.left * 100;
        }
    }

    void OnCollisionEnter(Collision col)
    {
        if (!go)
            return;
        //Debug.Log("OnCollisionEnter");
        if (col.gameObject.tag.Equals("Projectile"))
        {
            handle_collision(col.gameObject);
        }
    }

    void handle_collision(GameObject other)
    {
        if (other.GetComponent<Projectile>() == null)
            return;
        int id = other.GetComponent<Projectile>().id;
        GameObject.Destroy(other);
        if (vuln[id] <= -100)
        {
            Main.ram += 99 + vuln[id];
            Main.score--;
            hp += 100 + vuln[id];
        }
        else if (vuln[id] < 0)
        {
            hp += vuln[id];
            Main.credibility--;
        }
        else
        {
            hp -= vuln[id];
            if (vuln[id] == 0)
            {
                if (Main.diff >= 3)
                    Main.credibility -= 0.5f;
                if (Main.diff >= 5)
                    Main.credibility -= 1;
            }
        }
    }
}
