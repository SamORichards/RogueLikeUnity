using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreBar : MonoBehaviour {
    Text scoreBar;
    PlayerBasics player;
    void Start()
    {
        scoreBar = GetComponent<Text>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerBasics>();
    }

    void FixedUpdate()
    {
        scoreBar.text = "Score: " + player.Score;
    }
}
