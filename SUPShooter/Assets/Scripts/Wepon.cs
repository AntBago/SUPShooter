using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wepon : MonoBehaviour
{
    // Start is called before the first frame update

    
    public float fireRate = 0;
    public int Damage = 10;
    public LayerMask whatToHit;
    public Transform BulletTrailPrefab;
    public Transform MuzzleFlashPrefab;



    float timeToSpawnEffect = 0;
    public float effectSpawnRate = 10;
    float timeToFire = 0;
    Transform firePoint;

    void Awake()
    {
        firePoint = transform.Find("FirePoint");
        if(firePoint == null) 
        {
            Debug.LogError("no fire point");
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if(fireRate == 0)
        {
            if(Input.GetButtonDown("Fire1"))
            {
                Shoot();
            }
        else
        {
            if (Input.GetButton("Fire1") && Time.time > timeToFire)
            {
                    timeToFire = Time.time + 1/fireRate;
                    Shoot();
            }
        }
    }
    }
    void Shoot()
    {
        Vector2 mousePosition = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
        Vector2 firePointPosition = new Vector2(firePoint.position.x, firePoint.position.y);
        RaycastHit2D hit = Physics2D.Raycast(firePointPosition, mousePosition - firePointPosition, 100, whatToHit);
        if (Time.time >= timeToSpawnEffect)
        {
            Effect();
            timeToSpawnEffect = Time.time + 1 / effectSpawnRate;
        }
        if (hit.collider != null)
        {
            Debug.DrawLine(firePointPosition, hit.point, Color.red);
            Skeleton skeleton = hit.collider.GetComponent<Skeleton>();
            AlienSpaceship alien = hit.collider.GetComponent<AlienSpaceship>();
            Enemy enemy = hit.collider.GetComponent<Enemy>();
            BossHealth boss = hit.collider.GetComponent<BossHealth>();
            if (skeleton != null)
            {
                skeleton.DamageSkeleton(Damage);
                Debug.Log("We hit" + hit.collider.name + "and did" + Damage + "damage");
            }
            if (alien != null)
            {
                alien.DamageAlien(Damage);
                Debug.Log("We hit" + hit.collider.name + "and did" + Damage + "damage");
            }
            if (enemy != null)
            {
                enemy.DamageBoss(Damage);
                Debug.Log("We hit " + hit.collider.name + "and did " + Damage + "damage");
            }
            if (boss != null)
            {
                boss.DamageBoss(Damage);
                Debug.Log("We hit " + hit.collider.name + "and did " + Damage + "damage");
            }
        }




    }
    void Effect()
    {
        Instantiate(BulletTrailPrefab, firePoint.position, firePoint.rotation);
        Transform clone = Instantiate(MuzzleFlashPrefab, firePoint.position, firePoint.rotation) as Transform;
        clone.parent = firePoint;
        float size = Random.Range(0.6f, 0.9f);
        clone.localScale = new Vector3(size, size, size);
        Destroy(clone.gameObject, 0.02f);
    }
}
