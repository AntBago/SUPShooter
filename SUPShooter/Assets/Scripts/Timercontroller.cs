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
    public static float elapsedTime;
    public static List<float> bestTimes = new List<float>();
    int totalScores = 5; 


    private void Awake()
    {

        DontDestroyOnLoad(this.gameObject);
        LoadTimes();
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
        elapsedTime = 0f;
        StartCoroutine(UpdateTimer());

    }

    public void EndTimer()
    {
        timerGoing = false;
        CheckTime(elapsedTime);
        Debug.Log(elapsedTime);
    }

    public IEnumerator UpdateTimer()
    {
        while (timerGoing)
        {
            elapsedTime += Time.deltaTime;
            timePlaying = TimeSpan.FromSeconds(elapsedTime);
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

    //  Call this in Awake to load data.
    //  checks to see if theres any saved times and adds them to the list if there is.
    public void LoadTimes()
    {
        for (int i = 0; i < totalScores; i++)
        {
            string key = "time" + i;
            if (PlayerPrefs.HasKey(key))
            {
                bestTimes.Add(PlayerPrefs.GetFloat(key));
                
            }
        }
    }

    // Call from EndTimer method. 
    // check and see if the time just created is a "best time".
    public void CheckTime(float time)
    {
        // if there are not enough scores in the list, add it
        if (bestTimes.Count < totalScores)
        {
            bestTimes.Add(time);

            // Sort times from highest to lowest
            bestTimes.Sort((a, b) => b.CompareTo(a));
            SaveTimes();
            
        }
        else
        {

            for (int i = 0; i < bestTimes.Count; i++)
            {
                // if the time is smaller, insert it
                if (time < bestTimes[i])
                {
                    bestTimes.Insert(i, time);

                    // remove the last item in the list
                    bestTimes.RemoveAt(bestTimes.Count - 1);
                    SaveTimes();
                    break;
                }
            }
        }
    }
    // This is called from CheckTime().
    // saves the times to PlayerPrefs.

    public void SaveTimes()
    {
        for (int i = 0; i < bestTimes.Count; i++)
        {
            string key = "time" + i;
            PlayerPrefs.SetFloat(key, bestTimes[i]);
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
        // if scene is menu
        if (scene.name == "Menu")
        {
            // Destroy the gameobject (Timer)
            Destroy(gameObject);
        }
    }

}


  
