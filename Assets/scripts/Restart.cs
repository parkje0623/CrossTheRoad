using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Restart : MonoBehaviour
{
    private int highestScore;
    private string highScoreOwner;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (MapGenerator.isHighScore)
            {
                highestScore = MapGenerator.keepHighScore;
                PlayerPrefs.SetInt("HighestScore", highestScore);
                highScoreOwner = DataStore.username;
                PlayerPrefs.SetString("HighScoreOwner", highScoreOwner);
                PlayerPrefs.Save();
            }
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    public void RestartGame()
    {
        if (MapGenerator.isHighScore)
        {
            highestScore = MapGenerator.keepHighScore;
            PlayerPrefs.SetInt("HighestScore", highestScore);
            highScoreOwner = DataStore.username;
            PlayerPrefs.SetString("HighScoreOwner", highScoreOwner);
            PlayerPrefs.Save();
        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
