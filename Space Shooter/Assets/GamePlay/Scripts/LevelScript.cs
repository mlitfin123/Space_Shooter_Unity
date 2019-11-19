using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelScript : MonoBehaviour
{
    public int level;

    void OnGUI()
    {
        GUI.Box(new Rect(260, 10, 120, 20), "Level: " + level.ToString());  //display the level number in a GUI box
    }
    void Update()
    {
        if (level > 20)
            FindObjectOfType<GameManager>().EndGame();  //ends the game if the level is greater than 20
    }
}
