using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elephpant : MonoBehaviour
{

    public GameObject legs;
    public GameObject head;
    public GameObject MainCamera;

    public Movement Up;
    public Movement Down;
    public Movement Left;
    public Movement Right;

    public Sprite[] move_sprites;
    public Sprite[] shoot_sprites;

    public float speed = 10f;

    public bool canShoot = true;
    public static bool shooting = false;
    public static GameObject projectile;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    int sn = 0;
    bool moving = false;
    private void FixedUpdate()
    {
        moving = false;
        if (Main.started)
        {
            if (Up.held)
            {
                transform.position += Vector3.up * speed * Time.fixedDeltaTime;
                moving = true;
            }
            if (Down.held)
            {
                transform.position -= Vector3.up * speed * Time.fixedDeltaTime;
                moving = true;
            }
            if (Right.held)
            {
                transform.position += Vector3.right * speed * Time.fixedDeltaTime;
                moving = true;
            }
            if (Left.held)
            {
                transform.position -= Vector3.right * speed * Time.fixedDeltaTime;
                moving = true;
            }

            if (moving)
            {
                sn++;
                sn %= move_sprites.Length*5;
                legs.GetComponent<SpriteRenderer>().sprite = move_sprites[sn/5];
            } else
            {
                legs.GetComponent<SpriteRenderer>().sprite = move_sprites[0];
            }

            if (shooting)
            {
                if (canShoot)
                {
                    canShoot = false;
                    StartCoroutine(Shoot(GameObject.Instantiate<GameObject>(projectile)));
                }
                shooting = false;
            }
        }
    }

    public IEnumerator Shoot(GameObject projectile)
    {
        projectile.transform.localScale *= 0.6f;
        projectile.transform.position = new Vector3(0.94f, 0, 0);
        canShoot = false;
        foreach (Sprite s in shoot_sprites)
        {
            head.GetComponent<SpriteRenderer>().sprite = s;
            yield return new WaitForSeconds(0.05f);
        }
        projectile.transform.localScale *= 0.3f;
        head.GetComponent<SpriteRenderer>().sprite = shoot_sprites[0];
        projectile.transform.position = transform.position + Vector3.right*0.2f;
        projectile.tag = "Projectile";
        projectile.GetComponent<Projectile>().fly = true;
        yield return new WaitForSeconds(0.1f);
        canShoot = true;
    }
}
