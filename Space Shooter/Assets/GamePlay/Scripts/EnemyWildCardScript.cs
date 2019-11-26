using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWildCardScript : MonoBehaviour
{
    public float speed = 5;
    private float waitTime;
    public float startWaitTime;

    //indicates enemy can shoot
    public bool canShoot;
    //indicates enemy can move
    private bool canMove = true;
    //indicates the boundary of the game map
    public float bound_X = -15f;
    //indicates the attack point for the enemy
    public Transform attack_Point;
    //indicates the bullets to use in enemy attacks
    public GameObject bullet_Prefab;

    private Animator anim;
    private AudioSource explosionSound;

    public Transform[] moveSpots;
    private int randomSpot;
    // Start is called before the first frame update
    void Awake()
    {
        anim = GetComponent<Animator>();
        explosionSound = GetComponent<AudioSource>();
    }
    void Start()
    {
        randomSpot = Random.Range(0, moveSpots.Length);
        if (canShoot)
            Invoke("StartShooting", Random.Range(1f, 3f));
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }
    void Move()
    {
        if (canMove)
        {
            transform.position = Vector2.MoveTowards(transform.position, moveSpots[randomSpot].position, speed * Time.deltaTime);

            if (Vector2.Distance(transform.position, moveSpots[randomSpot].position) < 0.2f)
            {
                if (waitTime <= 0)
                {
                    randomSpot = Random.Range(0, moveSpots.Length);
                    waitTime = startWaitTime;
                }
                else
                {
                    waitTime -= Time.deltaTime;
                }
            }
            //remove game object
            if (transform.position.x < bound_X)
                gameObject.SetActive(false);
        }
    }
    void StartShooting()
    {
        GameObject bullet = Instantiate(bullet_Prefab, attack_Point.position, Quaternion.identity);
        bullet.GetComponent<FireBullet>().is_EnemyBullet = true;

        if (canShoot)
            Invoke("StartShooting", Random.Range(1f, 3f));
    }
    void TurnOffGameObject()
    {
        gameObject.SetActive(false);
    }
    //determines the actions on collision
    void OnTriggerEnter2D(Collider2D target)
    {
        if (target.tag == "Bullet" || target.tag == "Player")
        {
            //add score
            ScoreScript.Score += 10;
            canMove = false;
            //stop shooting
            if (canShoot)
            {
                canShoot = false;
                CancelInvoke("StartShooting");
            }
            //destroy enemy
            Invoke("TurnOffGameObject", .2f);

            explosionSound.Play();
            anim.Play("Destroy");
        }
    }
}


