using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        LivesScript.Lives = 3;
        ScoreScript.Score = 0;
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
