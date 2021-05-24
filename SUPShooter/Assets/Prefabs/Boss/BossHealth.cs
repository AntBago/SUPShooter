using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealth : MonoBehaviour
{

    //public int health = 500;

    public GameObject deathEffect;



    //public Transform player;

	[System.Serializable]
	public class BossStats
	{

		public int maxHealth = 500;

		private int _curHealth;
		public int curHealth
		{
			get { return _curHealth; }
			set { _curHealth = Mathf.Clamp(value, 0, maxHealth); }
		}

		public int damage = 40;



		public void Init()
		{
			curHealth = maxHealth;
		}
	}
	public BossStats stats = new BossStats();

    [Header("Optional: ")]
    [SerializeField]
    private StatusIndicator statusIndicator;

    public void DamageBoss(int damage)
    {
        if (isInvulnerable)
            return;

            stats.curHealth -= damage;
        if (stats.curHealth <= 400)
        {
            GetComponent<Animator>().SetBool("IsEnraged", true);
        }
        if (stats.curHealth <= 0)
        {
            GameMaster.KillBoss2(this);

        }
        if (statusIndicator != null)
        {
            statusIndicator.SetHealth(stats.curHealth, stats.maxHealth);
        }

    }
    void Start()
    {

        stats.Init();

        if (statusIndicator != null)
        {
            statusIndicator.SetHealth(stats.curHealth, stats.maxHealth);
        }
    }

    public bool isInvulnerable = false;

    //public void TakeDamage(int damage)
    //{
    //    if (isInvulnerable)
    //        return;

    //    stats.curHealth -= damage;

    //    if (stats.curHealth <= 200)
    //    {
    //        GetComponent<Animator>().SetBool("IsEnraged", true);
    //    }

    //    if (stats.curHealth <= 0)
    //    {
    //        Die();
    //    }
    //}

    void Die()
    {
        Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

}
