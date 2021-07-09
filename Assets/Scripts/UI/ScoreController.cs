using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreController : MonoBehaviour
{
    PlayerStats playerStats;
    private Text _scoreText;
    // Start is called before the first frame update
    void Awake()
    {
        _scoreText = gameObject.GetComponent<Text>();
        playerStats = GameObject.Find("Player").GetComponent<PlayerStats>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        _scoreText.text = "SCORE: " + playerStats.Score;
    }
}
