using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusTextScript : MonoBehaviour
{
    public float textDelay = 1f;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("TurnOffGameObject", textDelay);
    }

    void TurnOffGameObject()
    {
        gameObject.SetActive(false);
    }
}
