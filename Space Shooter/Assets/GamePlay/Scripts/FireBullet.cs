using UnityEngine;

public class FireBullet : MonoBehaviour
{
    public float speed = 5f;
    public float bound_X = -15f;

    [HideInInspector]
    public bool is_EnemyBullet = false;

    private AudioSource laserAudio;

    void Awake()
    {
        laserAudio = GetComponent<AudioSource>();
    }
    void Start()
    {
        if (is_EnemyBullet)
            speed *= -1f;
        laserAudio.Play();
    }
    void Update()
    {
        Move();
        if (GameManager.completeLevel == true)
        {
            Invoke("DeactivateGameObject", 0f);
        }
    }

    void Move() //moves the bullet towards the player and deactivates it once it hits the boundary
    {
        Vector3 temp = transform.position;
        temp.x += speed * Time.deltaTime;
        transform.position = temp;
        if (temp.x < bound_X)
            gameObject.SetActive(false);
    }

    void DeactivateGameObject()
    {
        gameObject.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D target) //deactivates the bullet on contact with the player or enemy
    {
        if (target.tag == "Bullet" || target.tag == "Enemy" || target.tag == "Player")
        {
            Invoke("DeactivateGameObject", 0f);
        }
    }
}
