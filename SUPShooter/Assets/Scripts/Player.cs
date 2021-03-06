using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [System.Serializable]
    public class PlayerStats
    {
        
        public int maxHealth = 100;

        private int _curHealth;
        public int curHealth
        {
            get { return _curHealth; }
            set { _curHealth = Mathf.Clamp(value, 0, maxHealth); }
        }

        public void Init()
        {
            curHealth = maxHealth;
        }
    }
    public PlayerStats stats = new PlayerStats();
    
    public int fallBoundary = -20;
    [SerializeField]
    private StatusIndicator statusIndicator;
    void Start()
    {
        stats.Init();

        if (statusIndicator == null)
        {
            Debug.LogError("No status indicator referenced on player");
        }
        else
        {
            statusIndicator.SetHealth(stats.curHealth, stats.maxHealth);
        }

    }

    public bool isAlive = true;

    private int damage;

    void Update()
    {
        if (transform.position.y <= fallBoundary)
        {
            DamagePlayer(999999);
        }

    }

    public void DamagePlayer(int damage)
    {

       


        if (!isAlive)
        {
            return;
        }
        stats.curHealth -= damage;
        if (stats.curHealth <= 0)
        {
            isAlive = false;
            GameMaster.KillPlayer(this);

    }

    statusIndicator.SetHealth(stats.curHealth, stats.maxHealth);

    } 

        


    
    public void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.gameObject.CompareTag("opossum"))
        {
            stats.curHealth -= damage;
            Debug.Log("HitOpossum");
        }
    }

   
}


