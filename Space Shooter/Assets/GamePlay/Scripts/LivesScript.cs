using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivesScript : MonoBehaviour
{
    public static int Lives = 3;

    void OnGUI()
    {
        GUI.Box(new Rect(130, 10, 120, 20), "Lives: " + Lives.ToString());
    }

    void Update()
    {
        if (Lives <= 0)
            FindObjectOfType<GameManager>().EndGame();
    }
}
