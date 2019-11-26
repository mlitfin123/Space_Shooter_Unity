using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    bool endGame = false;
    bool restartLevel = false;
    bool completeLevel = false;

    public float restartDelay = 1f;
    public float endGameDelay = 1f;
    public float completeLevelDelay = 1f;

    public Text highScore;
    public Text highScore2;

    public GameObject restartLevelText;
    public GameObject completeLevelText;
    public GameObject endGameText;

    private void Start()
    {
        highScore.text = PlayerPrefs.GetInt("High Score", 0).ToString();
        highScore2.text = PlayerPrefs.GetInt("High Score2", 0).ToString();

    }
    public void CompleteLevel()
    {
        completeLevel = true;
        completeLevelText.SetActive(true); //displays the complete level text
        Invoke("Complete", completeLevelDelay); //starts the Complete function
        CancelInvoke("SpawnEnemies");
    }
    void Complete()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); //loads the next scene after finishing the level
    }

    public void RestartLevel()
    {
        if (restartLevel == false)
        {
            restartLevel = true;
            Invoke("Restart", restartDelay); //starts the Restart function
            restartLevelText.SetActive(true); //displays the restart text
            LivesScript.Lives -= 1; //decreases the lives on a restart
        }
    }

    void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); //reloads the active scene
    }
    public void EndGame()
    {
        endGame = true; //sets the endGame boolian to true
        Invoke("End", endGameDelay); //starts the End function
        endGameText.SetActive(true); //displays the endGame text
        restartLevelText.SetActive(false); //prevents the restartLevel text
        restartLevel = false; //prevents the level from restarting
        LivesScript.Lives = 0;
        if (ScoreScript.Score > PlayerPrefs.GetInt("High Score", 0))  //create a high score for the player
        {
            PlayerPrefs.SetInt("High Score", ScoreScript.Score);
            highScore.text = ScoreScript.Score.ToString();
        }
        if (ScoreScript.Score > PlayerPrefs.GetInt("High Score2", 0) && ScoreScript.Score < PlayerPrefs.GetInt("High Score", 0)) //create a runner up high score for the player
        {
            PlayerPrefs.SetInt("High Score2", ScoreScript.Score);
            highScore2.text = ScoreScript.Score.ToString();
        }
    }

    public void Reset()
    {
        PlayerPrefs.DeleteAll();
        highScore.text = "0";
        highScore2.text = "0";
    }
    void End()
    {
        SceneManager.LoadScene("Main Menu");  //redirects to the main menu when the game ends
    }
}
