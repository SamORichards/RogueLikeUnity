using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour {
    Text healthBar;
    PlayerBasics player;
    void Start() {
        healthBar = GetComponent<Text>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerBasics>();
    }

    void FixedUpdate() {
        healthBar.text = "Health: " + player.getPlayerHealth() + "  /  " + player.getMaxHealth();   
    }

}
