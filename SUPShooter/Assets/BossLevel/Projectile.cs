using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour

{
    public float speed;

    private Transform player;
<<<<<<< Updated upstream
    private Vector2 target;
    
=======
    public int Damage = 10;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }
    // Use this for initialization
>>>>>>> Stashed changes
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        target = new Vector2(player.position.x, player.position.y);
    }

    
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);
        if (transform.position.x == target.x && transform.position.y == target.y)
        {
            DestroyProjectile();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            DestroyProjectile();
        }
<<<<<<< Updated upstream
=======

        if (collision.gameObject.tag == "Player")
        {
            collision.GetComponent<EnemyHealth>().curHealth -= Damage;

        }
        if (collision.gameObject.tag == "ground")
        {
            Instantiate(explosion, transform.position, transform.rotation);
            Debug.Log("Hit the ground");
            Destroy(gameObject);
        }

>>>>>>> Stashed changes
    }
    void DestroyProjectile()
    {
        Destroy(gameObject);
    }
}
