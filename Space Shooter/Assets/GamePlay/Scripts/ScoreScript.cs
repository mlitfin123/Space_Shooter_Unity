using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ScoreScript : MonoBehaviour
{
    public static int Score = 0;

    void OnGUI()
    {
        GUI.Box(new Rect(0, 10, 120, 20), "Score: " + Score.ToString());
    }
}