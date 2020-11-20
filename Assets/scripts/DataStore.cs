using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class DataStore : MonoBehaviour
{
    private Text highestScore;
    public static string username;

    public void Leaderboard(string name)
    {
        highestScore = GameObject.Find("HighScore").GetComponent<Text>();
        highestScore.text = name + ": " + Player.trackScore.ToString();
        username = name + ": ";
    }
}
