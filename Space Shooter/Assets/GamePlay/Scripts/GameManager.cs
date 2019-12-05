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

    public static int score1;
    public static int score2;
    public static int score3;
    public static int score4;
    public static int score5;
    public static int score6;
    public static int score7;
    public static int score8;
    public static int score9;
    public static int score10;
    public static int tempscore;

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
        getScores();
        if (tempscore > score1)
        {
            score10 = score9;
            score9 = score8;
            score8 = score7;
            score7 = score6;
            score6 = score5;
            score5 = score4;
            score4 = score3;
            score3 = score2;
            score2 = score1;
            score1 = tempscore;
        }
        else if (tempscore > score2)
        {
            score10 = score9;
            score9 = score8;
            score8 = score7;
            score7 = score6;
            score6 = score5;
            score5 = score4;
            score4 = score3;
            score3 = score2;
            score2 = tempscore;
        }
        else if (tempscore > score3)
        {
            score10 = score9;
            score9 = score8;
            score8 = score7;
            score7 = score6;
            score6 = score5;
            score5 = score4;
            score4 = score3;
            score3 = tempscore;
        }
        else if (tempscore > score4)
        {
            score10 = score9;
            score9 = score8;
            score8 = score7;
            score7 = score6;
            score6 = score5;
            score5 = score4;
            score4 = tempscore;
        }
        else if (tempscore > score5)
        {
            score10 = score9;
            score9 = score8;
            score8 = score7;
            score7 = score6;
            score6 = score5;
            score5 = tempscore;
        }
        else if (tempscore > score6)
        {
            score10 = score9;
            score9 = score8;
            score8 = score7;
            score7 = score6;
            score6 = tempscore;
        }
        else if (tempscore > score7)
        {
            score10 = score9;
            score9 = score8;
            score8 = score7;
            score7 = tempscore;
        }
        else if (tempscore > score8)
        {
            score10 = score9;
            score9 = score8;
            score8 = tempscore;
        }
        else if (tempscore > score9)
        {
            score10 = score9;
            score9 = tempscore;
        }
        else if (tempscore > score10)
        {
            score10 = tempscore;
        }
        saveScores();
        highScore.text = PlayerPrefs.GetInt("High Score", 0).ToString();
        highScore2.text = PlayerPrefs.GetInt("High Score2", 0).ToString();
        highScore3.text = PlayerPrefs.GetInt("High Score3", 0).ToString();
        highScore4.text = PlayerPrefs.GetInt("High Score4", 0).ToString();
        highScore5.text = PlayerPrefs.GetInt("High Score5", 0).ToString();
        highScore6.text = PlayerPrefs.GetInt("High Score6", 0).ToString();
        highScore7.text = PlayerPrefs.GetInt("High Score7", 0).ToString();
        highScore8.text = PlayerPrefs.GetInt("High Score8", 0).ToString();
        highScore9.text = PlayerPrefs.GetInt("High Score9", 0).ToString();
        highScore10.text = PlayerPrefs.GetInt("High Score10", 0).ToString();
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
            LivesScript.lives -= 1; //decreases the lives on a restart
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
        LivesScript.lives = 0;
        tempscore = ScoreScript.Score;
    }

    public void Reset() //resets the high scores
    {
        PlayerPrefs.DeleteAll();
        highScore.text = "0";
        highScore2.text = "0";
        highScore3.text = "0";
        highScore4.text = "0";
        highScore5.text = "0";
        highScore6.text = "0";
        highScore7.text = "0";
        highScore8.text = "0";
        highScore9.text = "0";
        highScore10.text = "0";
    }
    void End()
    {
        SceneManager.LoadScene("Main Menu");  //redirects to the main menu when the game ends
    }
    void saveScores()
    {
        PlayerPrefs.SetInt("High Score", score1);
        PlayerPrefs.SetInt("High Score2", score2);
        PlayerPrefs.SetInt("High Score3", score3);
        PlayerPrefs.SetInt("High Score4", score4);
        PlayerPrefs.SetInt("High Score5", score5);
        PlayerPrefs.SetInt("High Score6", score6);
        PlayerPrefs.SetInt("High Score7", score7);
        PlayerPrefs.SetInt("High Score8", score8);
        PlayerPrefs.SetInt("High Score9", score9);
        PlayerPrefs.SetInt("High Score10", score10);
    }
    void getScores()
    {

        if (PlayerPrefs.HasKey("High Score") == true)
        {
            score1 = PlayerPrefs.GetInt("High Score");
            score2 = PlayerPrefs.GetInt("High Score2");
            score3 = PlayerPrefs.GetInt("High Score3");
            score4 = PlayerPrefs.GetInt("High Score4");
            score5 = PlayerPrefs.GetInt("High Score5");
            score6 = PlayerPrefs.GetInt("High Score6");
            score7 = PlayerPrefs.GetInt("High Score7");
            score8 = PlayerPrefs.GetInt("High Score8");
            score9 = PlayerPrefs.GetInt("High Score9");
            score10 = PlayerPrefs.GetInt("High Score10");
        }
    }
}
