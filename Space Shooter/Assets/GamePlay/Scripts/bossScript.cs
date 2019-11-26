using UnityEngine;
using UnityEngine.UI;

public class bossScript : MonoBehaviour
{
    //speed of forward movement
    public float speed = 5f;
    private float waitTime;
    public float startWaitTime;
    public Transform moveSpot;
    public float maxX, minX, minY, maxY;

    public float startHealth = 30;
    private float health;
    //indicates the attack point for the boss
    public Transform attack_Point;
    public Transform attack_Point2;
    public Transform attack_Point3;
    public Transform attack_Point4;
    public Transform attack_Point5;
    //indicates the bullets to use in boss attacks
    public GameObject boss_Bullet;
    public GameObject boss_Bullet2;
    public GameObject boss_Bullet3;

    public bool canShootLaser1;
    public bool canShootLaser2;
    public bool canShootBomb;
    private bool canMove = true;

    public Image healthBar;

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

        if (canShootLaser1)
            Invoke("StartShootingLaser1", Random.Range(1f, 3f));
        if (canShootLaser2)
            Invoke("StartShootingLaser2", Random.Range(1f, 3f));
        if (canShootBomb)
            Invoke("StartShootingBomb", Random.Range(1f, 3f));

        health = startHealth;
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

    void StartShootingLaser1()
    {
        GameObject bullet = Instantiate(boss_Bullet, attack_Point.position, Quaternion.identity);
        bullet.GetComponent<FireBullet>().is_EnemyBullet = true;

        if (canShootLaser1)
            Invoke("StartShootingLaser1", Random.Range(1f, 3f));
    }

    void StartShootingLaser2()
    {
        GameObject bullet3 = Instantiate(boss_Bullet3, attack_Point3.position, Quaternion.Euler(0f, 0f, 90f));
        GameObject bullet5 = Instantiate(boss_Bullet3, attack_Point5.position, Quaternion.Euler(0f, 0f, 90f));
        bullet3.GetComponent<FireBullet>().is_EnemyBullet = true;
        bullet5.GetComponent<FireBullet>().is_EnemyBullet = true;

        if (canShootLaser2)
            Invoke("StartShootingLaser2", Random.Range(1f, 3f));
    }
    void StartShootingBomb()
    {
        GameObject bullet2 = Instantiate(boss_Bullet2, attack_Point2.position, Quaternion.identity);
        GameObject bullet4 = Instantiate(boss_Bullet2, attack_Point4.position, Quaternion.identity);
        bullet2.GetComponent<FireBullet>().is_EnemyBullet = true;
        bullet4.GetComponent<FireBullet>().is_EnemyBullet = true;

        if (canShootBomb)
            Invoke("StartShootingBomb", Random.Range(1f, 3f));
    }

    void TurnOffGameObject()
    {
        gameObject.SetActive(false);
    }
    void OnTriggerEnter2D(Collider2D target)
    {
        if (target.tag == "Bullet" || target.tag == "Enemy")
            health -= 1;
        healthBar.fillAmount = health / startHealth;
        if (health <= 0)
        {
            //add score
            ScoreScript.Score += 50;
            canMove = false;
            canShootLaser1 = false;
            canShootLaser2 = false;
            canShootBomb = false;
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
