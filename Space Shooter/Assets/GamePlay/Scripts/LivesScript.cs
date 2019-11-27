using UnityEngine;

public class LivesScript : MonoBehaviour
{
    public static int lives = 3; //indicates the number of lives the player begins the game with

    void OnGUI()
    {
        GUI.Box(new Rect(130, 10, 120, 20), "Lives: " + lives.ToString()); //create a GUI box to display the number of lives left
    }
    void Update()
    {
        if (lives <= 0)
            FindObjectOfType<GameManager>().EndGame();  //ends the game if the lives display 0
    }
}
