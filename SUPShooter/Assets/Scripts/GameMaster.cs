using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        Instantiate(playerPrefab, spawnPoint.position, spawnPoint.rotation);
        
    }

  public static void KillPlayer(Player player)
    {
        Destroy(player.gameObject);
        gm.StartCoroutine(gm._RespawnPlayer());
    }

     

    public static void KillSkeleton (Skeleton skeleton)
    {
     
        Destroy(skeleton.gameObject);
    }
    
    public static void KillBoss(Enemy enemy)
    {

        Destroy(enemy.gameObject);
    }

    public static void KillAlien(AlienSpaceship alien)
    {

        gm._KillAlien(alien);
    }
    public void _KillAlien(AlienSpaceship _alien)
    {

        Transform _clone = Instantiate(_alien.deathParticles, _alien.transform.position, Quaternion.identity) as Transform;
        Destroy(_clone, 5f);
        cameraShake.Shake(_alien.shakeAmt,_alien.shakeLength);

        Destroy(_alien.gameObject);
    }
}
