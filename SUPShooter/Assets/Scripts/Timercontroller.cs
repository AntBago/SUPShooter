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
    int totalScores = 5; // the total times you want to save


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
    /// <summary>
    ///  Call this in Awake to load in your data.
    ///  It checks to see if you have any saved times and adds them to the list if you do.
    /// </summary>
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

    /// <summary>
    /// Call this from your EndTimer method. 
    /// This will check and see if the time just created is a "best time".
    /// </summary>
    public void CheckTime(float time)
    {
        // if there are not enough scores in the list, go ahead and add it
        if (bestTimes.Count < totalScores)
        {
            bestTimes.Add(time);

            // make sure the times are in order from highest to lowest
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

    /// <summary>
    /// This is called from CheckTime().
    /// It saves the times to PlayerPrefs.
    /// </summary>
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
        // here you can use scene.buildIndex or scene.name to check which scene was loaded
        if (scene.name == "Menu")
        {
            // Destroy the gameobject this script is attached to
            Destroy(gameObject);
        }
    }

}


  
