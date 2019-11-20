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
    public Text highScore3;
    public Text highScore4;
    public Text highScore5;
    public Text highScore6;
    public Text highScore7;
    public Text highScore8;
    public Text highScore9;
    public Text highScore10;

    public GameObject restartLevelText;
    public GameObject completeLevelText;
    public GameObject endGameText;

    private void Start()
    {
        highScore.text = PlayerPrefs.GetInt("High Score", 0).ToString();
        highScore2.text = PlayerPrefs.GetInt("High Score2", 0).ToString();
        highScore3.text = PlayerPrefs.GetInt("High Score3", 0).ToString();
        highScore4.text = PlayerPrefs.GetInt("High Score4", 0).ToString();
        highScore5.text = PlayerPrefs.GetInt("High Score5", 0).ToString();
        highScore6.text = PlayerPrefs.GetInt("High Score6", 0).ToString();
        highScore7.text = PlayerPrefs.GetInt("High Score7", 0).ToString();
        highScore8.text = PlayerPrefs.GetInt("High Score8", 0).ToString();
        highScore9.text = PlayerPrefs.GetInt("High Score9", 0).ToString();
        highScore10
            .text = PlayerPrefs.GetInt("High Score10", 0).ToString();
    }
    public void CompleteLevel()
    {
        completeLevel = true;
        completeLevelText.SetActive(true); //displays the complete level text
        Invoke("Complete", completeLevelDelay); //starts the Complete function
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
            highScore.text = "New High Score!"; ScoreScript.Score.ToString();
        }
        else if (ScoreScript.Score > PlayerPrefs.GetInt("High Score2", 0)) 
        {
            PlayerPrefs.SetInt("High Score2", ScoreScript.Score);
            highScore.text = ScoreScript.Score.ToString();
        }
        else if (ScoreScript.Score > PlayerPrefs.GetInt("High Score3", 0))
        {
            PlayerPrefs.SetInt("High Score3", ScoreScript.Score);
            highScore.text = ScoreScript.Score.ToString();
        }
        else if (ScoreScript.Score > PlayerPrefs.GetInt("High Score4", 0))
        {
            PlayerPrefs.SetInt("High Score4", ScoreScript.Score);
            highScore.text = ScoreScript.Score.ToString();
        }
        else if (ScoreScript.Score > PlayerPrefs.GetInt("High Score5", 0))
        {
            PlayerPrefs.SetInt("High Score5", ScoreScript.Score);
            highScore.text = ScoreScript.Score.ToString();
        }
        else if (ScoreScript.Score > PlayerPrefs.GetInt("High Score6", 0))
        {
            PlayerPrefs.SetInt("High Score6", ScoreScript.Score);
            highScore.text = ScoreScript.Score.ToString();
        }
        else if (ScoreScript.Score > PlayerPrefs.GetInt("High Score7", 0))
        {
            PlayerPrefs.SetInt("High Score7", ScoreScript.Score);
            highScore.text = ScoreScript.Score.ToString();
        }
        else if (ScoreScript.Score > PlayerPrefs.GetInt("High Score8", 0))
        {
            PlayerPrefs.SetInt("High Score8", ScoreScript.Score);
            highScore.text = ScoreScript.Score.ToString();
        }
        else if (ScoreScript.Score > PlayerPrefs.GetInt("High Score9", 0))
        {
            PlayerPrefs.SetInt("High Score9", ScoreScript.Score);
            highScore.text = ScoreScript.Score.ToString();
        }
        else if (ScoreScript.Score > PlayerPrefs.GetInt("High Score10", 0))
        {
            PlayerPrefs.SetInt("High Score10", ScoreScript.Score);
            highScore.text = ScoreScript.Score.ToString();
        }
    }

    public void Reset()
    {
        PlayerPrefs.DeleteAll();
        highScore.text = "0";
    }
    void End()
    {
        SceneManager.LoadScene("Main Menu");  //redirects to the main menu when the game ends
    }
}
