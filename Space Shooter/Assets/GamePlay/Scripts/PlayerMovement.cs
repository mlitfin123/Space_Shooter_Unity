using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed; //indicates the speed of the player movement
    public float min_Y, max_Y; //indicates the min and max Y value of the map

    [SerializeField]
    private GameObject Player_Bullet; //determines the bullet the player will use

    [SerializeField]
    private Transform attack_Point; //sets the attack point of the player

    public float attack_Timer = 0.35f; //determines the amount of time until the player can shoot again
    private float current_Attack_Timer;
    private bool canAttack;

    private Animator anim;
    private AudioSource explosionSound;

    void Awake()
    {
        anim = GetComponent<Animator>();
        explosionSound = GetComponent<AudioSource>();

    }
    void Start()
    {
        current_Attack_Timer = attack_Timer; //sets the attack timer upon starting the level
    }

    void Update()
    {
        MovePlayer();
        Attack();
    }

    void MovePlayer() //allows the player to move up and down along the Y axis and not past the minimum or maximum Y values
    {
        if (Input.GetAxisRaw("Vertical") > 0f) //allow the key input of the up arrow or W key
        {
            Vector3 temp = transform.position;
            temp.y += speed = Time.deltaTime * 4;

            if (temp.y > max_Y)
                temp.y = max_Y;

            transform.position = temp;
        }
        else if (Input.GetAxisRaw("Vertical") < 0f)//allow the key input of the down arrow or S key
        {
            Vector3 temp = transform.position;
            temp.y -= speed = Time.deltaTime * 4;

            if (temp.y < min_Y)
                temp.y = min_Y;

            transform.position = temp;
        }
    }
    void Attack()
    {
        attack_Timer += Time.deltaTime; //allows the player to shoot once the attack timer is up
        if (attack_Timer > current_Attack_Timer)
        {
            canAttack = true;
        }

        if (Input.GetKeyDown(KeyCode.Space)) //allows the player to shoot using the space bar
        {
            if (canAttack)
            {
                canAttack = false;
                attack_Timer = 0f;
                Instantiate(Player_Bullet, attack_Point.position, Quaternion.Euler(0f, 0f, -90));//moves the bullet towards the enemy
            }
        }
    }
    void TurnOffGameObject()
    {
        gameObject.SetActive(false);
    }
    void OnTriggerEnter2D(Collider2D target)//destroys the player and turns off the bullet on collision
    {
        if (target.tag == "Bullet" || target.tag == "Enemy")
        {
            if (canAttack)
            {
                canAttack = false;
            }
            Invoke("TurnOffGameObject", .5f);
            explosionSound.Play();
            anim.Play("Destroy");
            FindObjectOfType<GameManager>().RestartLevel();

        }
    }
}
