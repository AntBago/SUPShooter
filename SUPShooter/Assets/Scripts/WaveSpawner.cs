using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{   //skapar ett enum för spawning waiting och counting
    public enum SpawnState { SPAWNING, WAITING, COUNTING};
    [System.Serializable]
    public class Wave
    {
        public string name;
        public Transform enemy;
        public int count;
        public float rate;

    }
    //skapar en array av waves
    public Wave[] waves;

    private int nextWave = 0;
    //skapar en array av spawnpoints som innehåller transform object i koden som man kan dra ut vart man vill att fienderna ska spawna
    public Transform[] spawnPoints;
    
    public float timeBetweenWaves = 5f;
    private float waveCountdown;

    private float searchCountdown = 1f;

    private SpawnState state = SpawnState.COUNTING;
    
    // Start is called before the first frame update
    void Start()
    {
        if (spawnPoints.Length == 0)
        {
            Debug.LogError("No spawn point ");
        }
            
        
        waveCountdown = timeBetweenWaves;
    }

    // Update is called once per frame
    void Update()
    {
        if(nextWave != waves.Length) //stoppar hela update funktionen
        {

        
            if(state == SpawnState.WAITING)
            { 
                //kollar ifall alla fiender är dödade
                if (!EnemyIsAlive())
                {
                    WaveCompleted();
                }
                else
                {
                
                    return;
                }
            }
            //räknar ner tills nästa våg våg med fiender kommer
            if(waveCountdown <= 0)
            {
                if(state != SpawnState.SPAWNING)
                {
                    StartCoroutine(SpawnWave(waves[nextWave]));

                }
            
            }
            else
            {
                waveCountdown -= Time.deltaTime;
            }
        }
       
    }
    //när en wave är klarad så kommer nästa tills det inte är några fler
    void WaveCompleted()
    {
        Debug.Log("Wave Completed");

        state = SpawnState.COUNTING;
        waveCountdown = timeBetweenWaves;

            if (nextWave + 1 > waves.Length - 1)
            {

                Debug.Log("all waves Comlete");

                nextWave++;

            }
            else
            {
                nextWave++;
            }


    }
    //en boolian funktion som kollar ifall fienderna lever eller ej
    bool EnemyIsAlive()
    {
        searchCountdown -= Time.deltaTime;
        if(searchCountdown <= 0f)
        {
            searchCountdown = 1f;
            //kollar ifall det finns nått gameobject med taggen Enemy
            if (GameObject.FindGameObjectWithTag("Enemy") == null) 
            {
                return false;
            }

        }
        return true;
    }
    //en IEnumerator som ser till att det är "den waven" som spawnar fiender
    IEnumerator SpawnWave(Wave _wave)
    {
        Debug.Log("Spawning Wave: " + _wave.name);
        state = SpawnState.SPAWNING;

        for(int i = 0; i < _wave.count; i++)
        {
            SpawnEnemy(_wave.enemy);
            yield return new WaitForSeconds(1f / _wave.rate);
        }

        state = SpawnState.WAITING;

        yield break;
    }

    // funktion för att spawna fiender random mellan de olika spawnpointsen
    void SpawnEnemy(Transform _enemy)
    {
     
      
        Debug.Log("Spawning Enemy " + _enemy.name);
        Transform _sp = spawnPoints[Random.Range(0, spawnPoints.Length)];
        Instantiate(_enemy, _sp.position, _sp.rotation);
    }
}
