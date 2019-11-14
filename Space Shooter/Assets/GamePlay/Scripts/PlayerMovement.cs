using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    public float min_Y, max_Y;

    [SerializeField]
    private GameObject Player_Bullet;

    [SerializeField]
    private Transform attack_Point;

    public float attack_Timer = 0.35f;
    private float current_Attack_Timer;
    private bool canAttack;

    private AudioSource laserAudio;

    private Animator anim;
    private AudioSource explosionSound;

    void Awake()
    {
        anim = GetComponent<Animator>();
        explosionSound = GetComponent<AudioSource>();
        laserAudio = GetComponent<AudioSource>();
    }
    // Start is called before the first frame update
    void Start()
    {
        current_Attack_Timer = attack_Timer;
    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();
        Attack();
    }

    void MovePlayer()
    {
        if (Input.GetAxisRaw("Vertical") > 0f)
        {
            Vector3 temp = transform.position;
            temp.y += speed = Time.deltaTime * 4;

            if (temp.y > max_Y)
                temp.y = max_Y;

            transform.position = temp;
        }
        else if (Input.GetAxisRaw("Vertical") < 0f)
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
        attack_Timer += Time.deltaTime;
        if (attack_Timer > current_Attack_Timer)
        {
            canAttack = true;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (canAttack)
            {
                canAttack = false;
                attack_Timer = 0f;
                Instantiate(Player_Bullet, attack_Point.position, Quaternion.Euler(0f, 0f, -90));

                laserAudio.Play();
            }
        }
    }
    void TurnOffGameObject()
    {
        gameObject.SetActive(false);
    }
    void OnTriggerEnter2D(Collider2D target)
    {
        if (target.tag == "Bullet" || target.tag == "Enemy")
        {
            if (canAttack)
            {
                canAttack = false;
            }
            Invoke("TurnOffGameObject", .3f);
            explosionSound.Play();
            anim.Play("Destroy");
            FindObjectOfType<GameManager>().RestartLevel();

        }
    }
}
