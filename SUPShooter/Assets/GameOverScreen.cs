using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverScreen : MonoBehaviour
{
    public Text pointsText;
    // Start is called before the first frame update
   public void Setup(float time) {
        gameObject.SetActive(true);
        pointsText.text = "Time: " + time.ToString(); 
    }
}
