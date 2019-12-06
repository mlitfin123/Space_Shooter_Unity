using UnityEngine;

public class enemy_script : MonoBehaviour
{
    //speed of forward movement
    public float speed = 5f;
    //speed of rotation
    public float rotate_Speed = 50f;

    //indicates enemy can shoot
    public bool canShoot;
    //indicates enemy can rotate
    public bool canRotate;
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
    void Awake()
    {
        anim = GetComponent<Animator>();
        explosionSound = GetComponent<AudioSource>();
    }
    void Start()
    {
        //determines if the enemy can or can't rotate
        if (canRotate)
        {
            //random rotation speed and direction
            if (Random.Range(0, 2) > 0)
            {
                rotate_Speed = Random.Range(rotate_Speed, rotate_Speed * 20f);
                rotate_Speed *= -1f;
            }
            else
            {
                rotate_Speed = Random.Range(rotate_Speed, rotate_Speed + 20f);
            }
        }
        //determines if the enemy can shoot or not
        if (canShoot)
                Invoke("StartShooting", Random.Range(1f, 3f));
    }
    void Update()
    {
        Move();
        RotateEnemy();
        if (GameManager.completeLevel == true)
        {
            Invoke("TurnOffGameObject", 0f);
            CancelInvoke("StartShooting");
        }
    }

    void Move()
    {
        if (canMove)
        {
            //enemies move forward after spawning
            Vector3 temp = transform.position;
            temp.x -= speed * Time.deltaTime;
            transform.position = temp;
            //remove game object after hitting the boundary
            if (temp.x < bound_X)
                gameObject.SetActive(false);
        }
    }

    //rotate the asteroid
    void RotateEnemy()
    {
        if(canRotate)
        {
            transform.Rotate(new Vector3(0f, 0f, rotate_Speed * Time.deltaTime), Space.World);
        }
    }

    //enemy fires bullet
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
        if(target.tag == "Bullet" || target.tag == "Player")
        {
            //add score
            ScoreScript.Score += 10;
            canMove = false;
            //stop shooting
            if(canShoot)
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
