using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMaster : MonoBehaviour
{

    public static GameMaster gm;
    void Start()
        
    {
        if(gm== null)
        {
            gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>() ;
        }
    }

    public Transform playerPrefab;
    public Transform spawnPoint;
    public int spawnDelay = 2;


    public CameraShake cameraShake;

    public IEnumerator _RespawnPlayer()
    {
        
        yield return new WaitForSeconds(spawnDelay);

       // if (gameObject.find("Player") == null)
        //{
            Instantiate(playerPrefab, spawnPoint.position, spawnPoint.rotation);
        //}
        
        
    }

  public static void KillPlayer(Player player)
    {



       

    Destroy(player.gameObject);

        
            

        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        if (currentSceneIndex != 4)
        {
            gm.StartCoroutine(gm._RespawnPlayer());

        }
        else
        {
            SceneManager.LoadScene("GameOver");
        }


    }

     

    public static void KillSkeleton (Skeleton skeleton)
    {
     
        Destroy(skeleton.gameObject);
    }
    
    public static void KillBoss(Enemy enemy)
    {

        Destroy(enemy.gameObject);
    }

    public static void KillBoss2(BossHealth boss)
    {

        Destroy(boss.gameObject);
    }


    public static void KillAlien(AlienSpaceship alien)
    {

        gm._KillAlien(alien);
    }
    public void _KillAlien(AlienSpaceship _alien)
    {

        GameObject _clone = Instantiate(_alien.deathParticles.gameObject, _alien.transform.position, Quaternion.identity) as GameObject;
        Destroy(_clone, 2f);
        cameraShake.Shake(_alien.shakeAmt, _alien.shakeLength);

        Destroy(_alien.gameObject);

    }
    public static void KillEnemyTest(EnemyTest enemyTest)
    {

        Destroy(enemyTest.gameObject);
    }

}
