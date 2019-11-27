using UnityEngine;

public class LevelScript : MonoBehaviour
{
    public int level;

    void OnGUI() //display the level number in a GUI box for the player to view
    {
        GUI.Box(new Rect(260, 10, 120, 20), "Level: " + level.ToString());
    }
}
