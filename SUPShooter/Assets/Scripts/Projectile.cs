using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour

{

    public float moveSpeed = 4f;
    Rigidbody2D rb;
    Vector2 moveDirection;
    bool facingRight = false;
   // public GameObject explosion;
    private Transform player;
    public int Damage = 10;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }
    // Use this for initialization
    void Start()
    {
        moveDirection = (player.transform.position - transform.position).normalized * moveSpeed;
        rb.velocity = new Vector2(moveDirection.x, moveDirection.y);
        Destroy(gameObject, 2f);

        if (player.transform.position.x > transform.position.x)
        {
            Flip();
        }
    }

    private void Update()
    {
        if (player == null)
        {
            return;
        }



    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.GetComponent<Player>().DamagePlayer(Damage);
            Debug.Log("Hit");
            Destroy(gameObject);
        }
        if (collision.gameObject.tag == "ground")
        {
           // Instantiate(explosion, transform.position, transform.rotation);
            Debug.Log("Hit the ground");
            Destroy(gameObject);
        }
     
 

    }
    void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}







//    void Start()
//    {
//        player = GameObject.FindGameObjectWithTag("Player").transform;
//        target = new Vector2(player.position.x, player.position.y);
//    }


//    void Update()
//    {
//        transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);
//        if (transform.position.x == target.x && transform.position.y == target.y)
//        {
//            DestroyProjectile();
//        }
//    }

//    void OnTriggerEnter2D(Collider2D other)
//    {
//        if (other.CompareTag("Player"))
//        {
//            DestroyProjectile();
//        }
//    }
//    void DestroyProjectile()
//    {
//        Destroy(gameObject);
//    }
//}
