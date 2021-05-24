using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTest : MonoBehaviour
{
    [System.Serializable]
    public class EnemyStatsTest
    {
        public int maxHealth = 100;


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
    public EnemyStatsTest stats = new EnemyStatsTest();

    

    public float shakeAmt = 0.1f;
    public float shakeLength = 0.1f;

    [Header("Optional: ")]
    [SerializeField]
    private StatusIndicator statusIndicator;

    void Start()
    {
        stats.Init();

        if (statusIndicator != null)
        {
            statusIndicator.SetHealth(stats.curHealth, stats.maxHealth);
        }

        //if (deathParticles == null)
        //{
        //    Debug.LogError("no death particels referenced");
        //}
    }

    public void DamageEnemyTest(int damage)
    {
        stats.curHealth -= damage;
        if (stats.curHealth <= 0)
        {
            GameMaster.KillEnemyTest(this);

        }
        if (statusIndicator != null)
        {
            statusIndicator.SetHealth(stats.curHealth, stats.maxHealth);
        }

    }
    void OnCollisionEnter2D(Collision2D _colInfo)
    {
        Player _player = _colInfo.collider.GetComponent<Player>();
        if (_player != null)
        {
            _player.DamagePlayer(stats.damage);
            DamageEnemyTest(999999);
        }


    }
}
