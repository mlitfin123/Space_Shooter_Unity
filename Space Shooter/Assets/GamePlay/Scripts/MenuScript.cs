using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    public void StartGame() //loads level 1 after the start button is clicked
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        LivesScript.lives = 3;
        ScoreScript.Score = 0;
    }
    public void QuitGame() //quits the game once the quit button is clicked
    {
        Application.Quit();
    }
}
