using UnityEngine;
public class ScoreScript : MonoBehaviour
{
    public static int Score = 0;

    void OnGUI() //creates a GUI for the player to view their score throughout the game
    {
        GUI.Box(new Rect(0, 10, 120, 20), "Score: " + Score.ToString());
    }
}