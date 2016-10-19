using UnityEngine;
using System.Collections;

public class GameManger : MonoBehaviour {

    public GameObject loadingScreen;
    public GameObject pauseScreen;
    GameObject ui;
    bool pause;


	// Use this for initialization
	void Start () {
        ui = GameObject.Find("UI");
    }

    void Update() {
        if (Input.GetButtonDown("Cancel")) {
            if (pause == false)
            {
                pauseGame(true);
                pause = true;
            }
            else {
                pauseGame(false);
                pause = false;
            }
        }
    }

    public void newLevel() {
        loadingScreen.SetActive(true);
        GetComponent<ItemHandler>().reset();
        GameObject[] walls = GameObject.FindGameObjectsWithTag("Wall");
        foreach (GameObject wall in walls) {
            Destroy(wall);
        }
        TileMap tileMap = GameObject.Find("Plane").GetComponent<TileMap>();
        tileMap.CreateMesh(tileMap.size_x, tileMap.size_z);
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerBasics>().Level++;
    }

    void pauseGame(bool yes) {
        ui.SetActive(!yes);
        if (yes)
        {
            Time.timeScale = 0;
        }
        else { 
            Time.timeScale = 1;
        }
        pauseScreen.SetActive(yes);
    }

}
