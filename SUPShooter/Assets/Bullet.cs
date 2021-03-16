using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 20f;
    public int damage = 25;
    public Rigidbody2D rb;
    

    void Start()
    {
        rb.velocity = transform.right * speed;
        
    }

    void OnTriggerEnter2D (Collider2D hitInfo) 
    {
        Monster monster = hitInfo.GetComponent<Monster>();
        if (monster !=null)
        {
            monster.TakeDamage(damage);
        }
        Destroy(gameObject);
    }
}
