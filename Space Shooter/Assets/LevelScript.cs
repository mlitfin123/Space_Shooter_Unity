using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelScript : MonoBehaviour
{
    public static int sceneCount;

    void Start()
    {
        sceneCount += 1;
    }

    void OnGUI()
    {
        GUI.Box(new Rect(260, 10, 120, 20), "Level: " + sceneCount.ToString());
    }
}
