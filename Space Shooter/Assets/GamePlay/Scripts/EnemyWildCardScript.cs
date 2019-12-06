using UnityEngine;

public class EnemyWildCardScript : MonoBehaviour
{
    //script for the enemy that does not move in a straight line towards the player
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
    void Update()
    {
        Move();
    }
    void Move() //indicates the enemy to move to random move spots manually placed across the map
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
            //remove game object at the boundary
            if (transform.position.x < bound_X)
                gameObject.SetActive(false);
        }
    }
    void StartShooting() //indicates the enemy can begin shooting within a random time period
    {
        GameObject bullet = Instantiate(bullet_Prefab, attack_Point.position, Quaternion.identity);
        bullet.GetComponent<FireBullet>().is_EnemyBullet = true;

        if (canShoot)
            Invoke("StartShooting", Random.Range(1f, 2f));
    }
    void TurnOffGameObject()
    {
        gameObject.SetActive(false);
    }
    void OnTriggerEnter2D(Collider2D target)//determines the actions on collision
    {
        if (target.tag == "Bullet" || target.tag == "Player")
        {
            ScoreScript.Score += 15;//adds score if hit
            canMove = false;
            //stop shooting after being destroyed
            if (canShoot)
            {
                canShoot = false;
                CancelInvoke("StartShooting");
            }
            //destroy enemy after being hit
            Invoke("TurnOffGameObject", .2f);

            explosionSound.Play();
            anim.Play("Destroy");
        }
    }
}


