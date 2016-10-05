using UnityEngine;
using System.Collections;
using System;

public class EnemyAI : MonoBehaviour {
    int[,] posData;
    GameObject map;
    Vector3 pos;
    public bool isPlayerSeen;
    TileMap tm;
    double health;
    TMData td;
    RaycastHit hit;
    GameObject playerCollider;
    int layer = 0;
    int timeToAttack;
    double DMG;
    Vector3 lastKnownPos;
    // Use this for initialization
    void Start () {
        playerCollider = GameObject.FindGameObjectWithTag("Player");
        map = GameObject.Find("Plane");
        tm = map.GetComponent<TileMap>();
        td = tm.returnTMData();
        posData = new int[tm.size_x,tm.size_z];
        isPlayerSeen = false;
        health = 25;
        DMG = 5;
        timeToAttack = 25;
	}

    internal void beenHit(double swordDMG)
    {
        health -= swordDMG;
        if (health <= 0) {
            Debug.Log("Enemy is dead!");
            gameObject.SetActive(false);
            playerCollider.GetComponent<PlayerBasics>().Score++;
            GameObject.Find("GameMangers").GetComponent<EnemiesHandler>().enemyDied();
        }
    }

    bool isPlayerDetected()
    {
        if (Vector3.Distance(transform.position, playerCollider.transform.position) <= 15)
        {
            return true;
        }
        else return false;
    }
    bool canSeePlayer() {
        pos = transform.position;
        pos.y += 1.5f;
        Debug.DrawRay(pos, playerCollider.transform.position - pos, new Color(0, 1, 1, 1));
        Ray ray = new Ray(pos, playerCollider.transform.position - pos);
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.tag == "Player")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else {
            return false;
        }
    }


    // Update is called 50 times per second
    void FixedUpdate () {
        if (isPlayerDetected() && canSeePlayer())
        {
            if (isPlayerDetected())
            {
                if (canSeePlayer())
                {
                    var heading = playerCollider.transform.position - transform.position;
                    if (heading.magnitude > 1.1)
                    {
                        transform.position = Vector3.MoveTowards(transform.position, playerCollider.transform.position, 0.1f);
                        transform.LookAt(playerCollider.transform.position);
                        lastKnownPos = playerCollider.transform.position;
                    }
                    if (heading.magnitude <= 1.3)
                    {
                        if (timeToAttack == 0)
                        {
                            playerCollider.GetComponent<PlayerBasics>().beenHit(DMG);
                        }
                            else if (timeToAttack >= 50)
                            {
                                timeToAttack = -1;
                            }
                            timeToAttack++;
                        }
                    }
                }

            }
        else if(lastKnownPos != Vector3.zero)
        {
            transform.position = Vector3.MoveTowards(transform.position, lastKnownPos, 0.1f);
        }
    }
}
