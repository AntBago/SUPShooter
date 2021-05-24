using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyNewGame : MonoBehaviour
{
    public void destroyTimer()
    {
        Destroy(GameObject.Find("Canvas"));
    }
}
