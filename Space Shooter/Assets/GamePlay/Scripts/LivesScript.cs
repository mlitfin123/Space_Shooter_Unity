using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivesScript : MonoBehaviour
{
    public static int Lives = 3;
    //create a GUI box to display the number of lives left
    void OnGUI()
    {
        GUI.Box(new Rect(130, 10, 120, 20), "Lives: " + Lives.ToString());
    }
    //ends the game if the lives display 0
    void Update()
    {
        if (Lives <= 0)
            FindObjectOfType<GameManager>().EndGame();
    }
}
