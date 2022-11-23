using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    public static int enemies = 0;

    public TextMesh ram_text;
    public TextMesh credibility_text;
    public TextMesh score_text;

    public GameObject[] common_enemies;
    public GameObject[] uncommon_enemies;
    public GameObject[] rare_enemies;

    public GameObject elephpant;
    public static Vector3 mouseWorldPosition;
    public static bool started = false;
    public static int diff = 1;

    public static float ram = 64;
    public static float max_ram = 64;
    public static int score = 0;

    public static bool gameOver = false;

    public static float credibility = 10;

    public GameObject gameOverText;
    private Vector3 gotl;

    public static void StartGame()
    {
        started = true;
        GameObject[] yeet = GameObject.FindGameObjectsWithTag("Start Menu");
        foreach (GameObject y in yeet)
            GameObject.Destroy(y);
    }

    // Start is called before the first frame update
    void Start()
    {
        gotl = gameOverText.transform.position;
        gameOverText.transform.position += Vector3.up * 10;

    }

    int c = 0;

    // Update is called once per frame
    void Update()
    {
        mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    private void FixedUpdate()
    {
        c++;

        if (gameOver)
        {
            gameOverText.transform.position -= Vector3.up * 10;
            gameOver = false;
        }
        if (c > 10 && started)
        {
            if (credibility < 0)
                credibility = 0;
            if (ram <= 0)
            {
                started = false;
                gameOver = true;
            }
            ram_text.text = "RAM Available: " + Mathf.Round(ram) + " / " + max_ram;
            credibility_text.text = "Credibility: " + Mathf.Round(credibility * 10) / 10;
            score_text.text = "Score: " + score;
            c = 0;
            if (Random.Range(0, 100) < Mathf.Min(8 + score*0.15f + diff*1.5f, 50)/(0.5f*enemies+0.9f))
            {
                GameObject spawned_enemy = null;
                //Debug.Log("Spawning Enemy");
                do
                {
                    switch ((int)Random.Range(0, 5.99f))
                    {
                        case 0:
                        case 1:
                        case 2:
                            spawned_enemy = (common_enemies[Mathf.Min((int)Random.Range(0, common_enemies.Length), common_enemies.Length)]);
                            break;
                        case 3:
                        case 4:
                            spawned_enemy = (uncommon_enemies[Mathf.Min((int)Random.Range(0, uncommon_enemies.Length), uncommon_enemies.Length)]);
                            break;
                        case 5:
                        case 6:
                            spawned_enemy = (rare_enemies[Mathf.Min((int)Random.Range(0, rare_enemies.Length), rare_enemies.Length)]);
                            break;
                    }
                } while (spawned_enemy == null || (spawned_enemy.GetComponent<Enemy>().only_low_cred && credibility > 5));
                spawned_enemy = Instantiate<GameObject>(spawned_enemy);
                spawned_enemy.GetComponent<Enemy>().Spawn();
            }
            if (credibility < 4)
            {
                GameObject spawned_enemy = null;
                do
                {
                    switch ((int)Random.Range(0, 5.99f))
                    {
                        case 0:
                            spawned_enemy = (common_enemies[Mathf.Min((int)Random.Range(0, common_enemies.Length), common_enemies.Length)]);
                            break;
                        case 1:
                            spawned_enemy = (uncommon_enemies[Mathf.Min((int)Random.Range(0, uncommon_enemies.Length), uncommon_enemies.Length)]);
                            break;
                        case 2:
                            spawned_enemy = (rare_enemies[Mathf.Min((int)Random.Range(0, rare_enemies.Length), rare_enemies.Length)]);
                            break;
                    }
                } while (!spawned_enemy.GetComponent<Enemy>().only_low_cred);
                spawned_enemy = Instantiate<GameObject>(spawned_enemy);
                spawned_enemy.GetComponent<Enemy>().Spawn();
            }
        }
    }
}
