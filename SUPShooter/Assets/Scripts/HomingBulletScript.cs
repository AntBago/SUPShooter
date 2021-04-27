using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingBulletScript : MonoBehaviour
{
    public float speed = 5;
    Transform player;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
    }
}
