using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveHighscore : MonoBehaviour
{
    public Text highscore;

    void Start() 
    {
        //highscore.text = Timercontroller.bestTimes.ToString();
    }

    void Update() 
    {
        for (int i = 0; i < Timercontroller.bestTimes.Count; i++) {
            highscore.text = "Highscore " + Timercontroller.bestTimes[i].ToString();


        }
    
    }
}