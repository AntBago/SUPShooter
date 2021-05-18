using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

public class Timercontroller : MonoBehaviour
{
    public static Timercontroller instans;

    public Text timeCounter;

    private TimeSpan timePlaying;
    private bool timerGoing;
    private float eladsedTime;
    //public int lastScene;
    

    private void Awake()
    {
        
        


            DontDestroyOnLoad(this.gameObject);
      



    }

    // Start is called before the first frame update
    void Start()
    {
        

        timeCounter.text = "Time: 00:00.00";
        timerGoing = false;

        BeginTimer();
        

    }

    public void BeginTimer()
    {
        timerGoing = true;
        eladsedTime = 0f;
        StartCoroutine(UpdateTimer());

    }

    public void EndTimer()
    {
        timerGoing = false;

    }

    private IEnumerator UpdateTimer()
    {
        while (timerGoing)
        {
            eladsedTime += Time.deltaTime;
            timePlaying = TimeSpan.FromSeconds(eladsedTime);
            string timePlayingString = "Time: " + timePlaying.ToString("mm':'ss'.'ff");
            timeCounter.text = timePlayingString;

            yield return null;

        }


    }

    // Update is called once per frame
    void Update()
    {
       
        
         int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        if (currentSceneIndex == 5) {
            EndTimer();
        }
     
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // here you can use scene.buildIndex or scene.name to check which scene was loaded
        if (scene.name == "Menu")
        {
            // Destroy the gameobject this script is attached to
            Destroy(gameObject);
        }
    }

}


  
