using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Timercontroller : MonoBehaviour
{
    public static Timercontroller instans;

    public Text timeCounter;

    private TimeSpan timePlaying;
    private bool timerGoing;
    private float eladsedTime;

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

    public void EndTimer() {
        timerGoing = false;
    
    }

    private IEnumerator UpdateTimer() {
        while (timerGoing)
        {
            eladsedTime += Time.deltaTime;
            timePlaying = TimeSpan.FromSeconds(eladsedTime);
            string timePlayingString = "Time: " + timePlaying.ToString("mm':'ss'.'ff");
            timeCounter.text = timePlayingString;

            yield return null;
        
        }


    }


    //void OnCollisionEnter2D(Collision2D _colInfo)
    //{
    //    Player _player = _colInfo.collider.GetComponent<Player>();
    //    if (_player != null)
    //    {
    //        EndTimer();
    //    }


    //}



}
  

