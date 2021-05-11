using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Text scoreDisplay;

    public static float highScore;
    public static float currentScore;

    private void Start()
    {
        if (scoreDisplay)
        {
            scoreDisplay.text = currentScore.ToString() + "m";
        }
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }
    public void StartScene()
    {
        SceneManager.LoadScene(0);
    }


    public static void EndGame()
    {
        SceneManager.LoadScene(2);
    }


}
