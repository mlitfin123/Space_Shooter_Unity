using UnityEngine;

public class FireBullet : MonoBehaviour
{
    public float speed = 5f;
    public float bound_X = -15f;

    [HideInInspector]
    public bool is_EnemyBullet = false;
    // Start is called before the first frame update
    void Start()
    {
        if (is_EnemyBullet)
            speed *= -1f;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    void Move()
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

    void OnTriggerEnter2D(Collider2D target)
    {
        if (target.tag == "Bullet" || target.tag == "Enemy" || target.tag == "Player")
        {
            Invoke("DeactivateGameObject", 0f);
        }
    }
}
