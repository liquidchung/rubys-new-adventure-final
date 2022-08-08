using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class nonplayerchara : MonoBehaviour
{
    public float displayTime = 4.0f;
    public GameObject dialogBox;
    public GameObject secondaryBox;
    float timerDisplay;
    public bool dialogue;
    public static bool sceneChange;


    void Start()
    {
        dialogBox.SetActive(false);
        secondaryBox.SetActive(false);
        dialogue = false;
        sceneChange = false;
        timerDisplay = 4.0f;

    }

    void Update()
    {
        if (dialogue)
        {
            timerDisplay -= Time.deltaTime;

            if (sceneChange && timerDisplay < 0)
            {
                SceneManager.LoadScene("Scene 2");
                sceneChange = false;
            }

            if (timerDisplay < 0)
            {
                dialogBox.SetActive(false);
                secondaryBox.SetActive(false);
                dialogue = false;
                Debug.Log("Bye dialogue");
                timerDisplay = 4.0f;
            }
        }

    }

    public void DisplayDialog()
    {
        timerDisplay = displayTime;
        Debug.Log(EVIL.roboCount);
        dialogBox.SetActive(true);
        dialogue = true;

        if (EVIL.roboCount == 4)
        {
            secondaryBox.SetActive(true);
            dialogue = true;
            sceneChange = true;
        }
    }


}
