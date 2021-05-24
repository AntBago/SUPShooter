using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class StopGame : MonoBehaviour
{
    Timercontroller timercontroller;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        timercontroller.EndTimer();
    }

}