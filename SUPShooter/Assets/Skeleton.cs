using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton : MonoBehaviour
{
    public Transform player;

    public GameObject deathEffect;

    public bool isFlipped = false;

    [System.Serializable]
    public class SkeletonStats
    {
        public int Health = 100;
    }
    public SkeletonStats stats = new SkeletonStats();

    public void DamageSkeleton(int damage)
    {
        stats.Health -= damage;
        if (stats.Health <= 0)
        {
            DeathEffect();
            GameMaster.KillSkeleton(this);

        }
    }

    private void DeathEffect()
    {
        if(deathEffect != null)
        {
            GameObject effect = Instantiate(deathEffect, transform.position, Quaternion.identity);
                Destroy(effect, 1f);
        }
    }


    public void LookAtPlayer()
    {
        Vector3 flipped = transform.localScale;
        flipped.z *= -1f;

        if(transform.position.x > player.position.x && isFlipped)
        {
            transform.localScale = flipped;
            transform.Rotate(0f, 180f, 0f);
            isFlipped = false;
        }
        else if (transform.position.x < player.position.x && !isFlipped)
        {
            transform.localScale = flipped;
            transform.Rotate(0f, 180f, 0f);
            isFlipped = true;
        }
    }
}
