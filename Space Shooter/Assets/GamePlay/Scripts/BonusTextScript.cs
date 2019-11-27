using UnityEngine;

public class BonusTextScript : MonoBehaviour
{
    //starts the level with Bonus Level text and turns off the text after a specified amount of time
    public float textDelay = 1f;
    void Start()
    {
        Invoke("TurnOffGameObject", textDelay);
    }

    void TurnOffGameObject()
    {
        gameObject.SetActive(false);
    }
}
