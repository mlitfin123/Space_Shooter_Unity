using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    float currentTime = 0f;
    float startingTime = 0f;

    [SerializeField] Text TimerText;
    void Start() // Start the timer 
    {
        currentTime = startingTime;
    }
    void Update() //updates and displays the timer according to real time
    {
        currentTime += Time.deltaTime;
        TimerText.text = currentTime.ToString("0.0");

        if (currentTime <= 0)
            currentTime = 0;
    }
}
