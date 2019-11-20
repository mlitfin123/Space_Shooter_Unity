using UnityEngine;

public class bossScript : MonoBehaviour
{
    //speed of forward movement
    public float speed = 5f;
    private float waitTime;
    public float startWaitTime;
    public Transform moveSpot;
    public float maxX, minX, minY, maxY;

    public float life = 1;
    //indicates the attack point for the boss
    public Transform attack_Point;
    //indicates the bullets to use in boss attacks
    public GameObject boss_Bullet;
    public bool canShoot;
    private bool canMove = true;

    private Animator anim;
    private AudioSource explosionSound;
    void Awake()
    {
        anim = GetComponent<Animator>();
        explosionSound = GetComponent<AudioSource>();
    }

    // Start is called before the first frame update
    void Start()
    {
        moveSpot.position = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));

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
            transform.position = Vector2.MoveTowards(transform.position, moveSpot.position, speed * Time.deltaTime);

            if(Vector2.Distance(transform.position, moveSpot.position)< 0.2f)
            {
                if(waitTime <= 0)
                {
                    moveSpot.position = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
                    waitTime = startWaitTime;
                }
                else
                {
                    waitTime -= Time.deltaTime;
                }
            }
        }
    }

    void StartShooting()
    {
        GameObject bullet = Instantiate(boss_Bullet, attack_Point.position, Quaternion.identity);
        bullet.GetComponent<FireBullet>().is_EnemyBullet = true;

        if (canShoot)
            Invoke("StartShooting", Random.Range(1f, 3f));
    }

    void TurnOffGameObject()
    {
        gameObject.SetActive(false);
    }
    void OnTriggerEnter2D(Collider2D target)
    {
        if (target.tag == "Bullet" || target.tag == "Enemy")
            life -= 1;
        if (life <= 0)
        {
            //add score
            ScoreScript.Score += 50;
            canMove = false;
            canShoot = false;
            CancelInvoke("StartShooting");
            //destroy boss
            Invoke("TurnOffGameObject", .3f);
            explosionSound.Play();
            anim.Play("Death1");
            anim.Play("Death");
            FindObjectOfType<EnemySpawner>().bossnumber -= 1;
            FindObjectOfType<GameManager>().CompleteLevel();
        }
    }
}
