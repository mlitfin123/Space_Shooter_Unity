using UnityEngine;
using UnityEngine.UI;

public class bossScript : MonoBehaviour
{
    public float speed = 5f;//speed of forward movement
    private float waitTime;
    public float startWaitTime;
    public Transform moveSpot; //indicates the location the boss is to move towards before following the random movement script
    public float maxX, minX, minY, maxY;

    public float startHealth = 30;
    private float health;
    //indicates the attack points for the boss
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

    public Image healthBar;//displays the healthbar in the game for the boss

    private Animator anim;
    private AudioSource explosionSound;
    void Awake()
    {
        anim = GetComponent<Animator>(); //gets the Animator source upon Awake
        explosionSound = GetComponent<AudioSource>(); //gets the Audio source upon Awake
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
    void Update()
    {
        Move();
    }
    void Move() //allows the boss to move to random locations within a specific X and Y range
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

    void StartShootingLaser1()//allows the boss to shoot the laser1
    {
        GameObject bullet = Instantiate(boss_Bullet, attack_Point.position, Quaternion.Euler(0f, 0f, 90f));
        bullet.GetComponent<FireBullet>().is_EnemyBullet = true;

        if (canShootLaser1)
            Invoke("StartShootingLaser1", Random.Range(1f, 3f));
    }

    void StartShootingLaser2()//allows the boss to shoot the laser2
    {
        GameObject bullet3 = Instantiate(boss_Bullet3, attack_Point3.position, Quaternion.Euler(0f, 0f, 90f));
        GameObject bullet5 = Instantiate(boss_Bullet3, attack_Point5.position, Quaternion.Euler(0f, 0f, 90f));
        bullet3.GetComponent<FireBullet>().is_EnemyBullet = true;
        bullet5.GetComponent<FireBullet>().is_EnemyBullet = true;

        if (canShootLaser2)
            Invoke("StartShootingLaser2", Random.Range(1f, 3f));
    }
    void StartShootingBomb() //allows the boss to shoot the bomb
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
        if (target.tag == "Bullet" || target.tag == "Boss") //decreases the boss health after being shot
            health -= 1;
        healthBar.fillAmount = health / startHealth;
        if (health <= 0)
        {
            ScoreScript.Score += 100;//add score
            canMove = false;//prevents boss from moving after death
            canShootLaser1 = false;//prevents boss from shooting laser1 after death
            canShootLaser2 = false;//prevents boss from shooting laser2 after death
            canShootBomb = false; //prevents boss from shooting bomb after death
            CancelInvoke("StartShootingLaser1");
            CancelInvoke("StartShootingLaser2");
            CancelInvoke("StartShootingBomb");
            Invoke("TurnOffGameObject", .3f); //destroy boss
            explosionSound.Play(); //boss death sound
            anim.Play("Death1"); //boss death animation
            anim.Play("Death"); //boss death animation
            FindObjectOfType<EnemySpawner>().bossnumber -= 1; //subtracts the number of bosses in the game
            FindObjectOfType<GameManager>().CompleteLevel(); //completes the level once the boss is destroyed
        }
    }
}
