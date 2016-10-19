using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

public class PlayerBasics : MonoBehaviour {

    public bool isAttacking;
    public double SwordDMG;
    RaycastHit hit;
    Vector3 temp;
    Quaternion rot;
    int timeAttacking;
    double health;
    public double maxHealth;
    public GameObject gameOverScreen;
    public int Score;
    public int Level;

    // Use this for initialization
    void Start()
    {
        Level = 1;
        isAttacking = false;
        timeAttacking = 0;
        SwordDMG = 5;
        maxHealth = 100;
        health = maxHealth;
        Score = 0;
    }

    internal double getMaxHealth()
    {
        return maxHealth;
    }

    internal void hitItem(ItemsData.Item i)
    {
        /* 1 = Health Pack
         * 2 = Armour
         * 3 = Sword Upgrade
         */
        switch (i.itemType) {
            case 1:
                health += 15;
                if (health > maxHealth) {
                    health = maxHealth;
                }
                break;
            case 2:
                maxHealth += 10;
                break;
            case 3:
                SwordDMG += 1;
                break;
            default:
                health += 15;
                if (health > maxHealth)
                {
                    health = maxHealth;
                }
                break;
        }
    }


    public double getPlayerHealth() {
        return health;
    }
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("Fire1")) {
            isAttacking = true;
        }
    }
    void FixedUpdate() {
        if (isAttacking == true) {
            //enter attack code here
            if (timeAttacking == 0)
            {
                timeAttacking = 1;
            }
            else if (timeAttacking < 10)
            {
                timeAttacking++;
            }
            else if (timeAttacking >= 10)
            {
                timeAttacking = 0;
                isAttacking = false;
            }
        }

    }
    public void LookAtPoint(Vector3 mouse)
    {
        temp = mouse;
        temp.y += 1;
        transform.LookAt(mouse);
        rot = transform.rotation;
        rot.x = 0;
        rot.z = 0;
        transform.rotation = rot;

    }

    internal void beenHit(double dMG)
    {
        if (health <= 0)
        {
            gameOver();
            return;
        }
        health -= dMG * Level;

    }

    private void gameOver()
    {
        gameOverScreen.SetActive(true);
    }
}
