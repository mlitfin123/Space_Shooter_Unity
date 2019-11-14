using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    bool endGame = false;
    bool restartLevel = false;
    bool completeLevel = false;

    public float restartDelay = 1f;
    public float endGameDelay = 1f;
    public float completeLevelDelay = 1f;

    public GameObject restartLevelText;
    public GameObject completeLevelText;
    public GameObject endGameText;

    public void CompleteLevel()
    {
        completeLevel = true;
        completeLevelText.SetActive(true);
        Invoke("Complete", completeLevelDelay);
    }

    void Complete()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void RestartLevel()
    {
        if (restartLevel == false)
        {
            restartLevel = true;
            Invoke("Restart", restartDelay);
            restartLevelText.SetActive(true);
            LivesScript.Lives -= 1;
        }
    }

    void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void EndGame()
    {
        endGame = true;
        Invoke("End", endGameDelay);
        endGameText.SetActive(true);
        restartLevelText.SetActive(false);
        restartLevel = false;
        LivesScript.Lives = 0;
    }
    void End()
    {
        SceneManager.LoadScene("Main Menu");
    }
}
