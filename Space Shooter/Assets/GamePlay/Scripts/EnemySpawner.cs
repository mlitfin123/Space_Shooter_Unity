using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    //indicates min and max spawn location
    public float min_Y = -4.3f, max_Y = 4.3f;
    //indicates asteroids to spawn
    public GameObject[] asteroid_Prefabs;
    //indicates enemy ships to spawn
    public GameObject[] enemy_Prefabs;
    //indicates enemy boss to spawn
    public GameObject boss;
    //indicates time until next spawn
    public float timer = 2f;
    // Start is called before the first frame update
    public float bosstimer = 30f;
    public int bossnumber = 0;
    void Start()
    {
        Invoke("SpawnEnemies", timer);
        Invoke("SpawnBoss", bosstimer);
    }

    void SpawnEnemies()
    {
        //indicates a random position for the spawn between the min and max
        float pos_Y = Random.Range(min_Y, max_Y);
        //temporary position of spawner
        Vector3 temp = transform.position;
        temp.y = pos_Y;
        //indicates a 50/50 chance to spawn an asteroid or enemy
        if(Random.Range(0, 2) > 0)
        {
            //spawn asteroid
            Instantiate(asteroid_Prefabs[Random.Range(0, asteroid_Prefabs.Length)], temp, Quaternion.identity);

        }
        else if (Random.Range(0, 2) > 0)
        {
            //spawn enemy
            Instantiate(enemy_Prefabs[Random.Range(0, enemy_Prefabs.Length)], temp, Quaternion.Euler(0f, 0f, 90f));
        }
        Invoke("SpawnEnemies", timer);
    }
    void SpawnBoss()
    {
        //indicates a random position for the spawn between the min and max
        float pos_Y = Random.Range(min_Y, max_Y);
        //temporary position of spawner
        Vector3 temp = transform.position;
        temp.y = pos_Y;
        if (bossnumber == 0) //spawns the boss if there is not already a boss present
        {
            Instantiate(boss, temp, Quaternion.Euler(0f, 0f, 90f));
            Invoke("SpawnBoss", bosstimer);
            bossnumber += 1;
        }
    }
}
