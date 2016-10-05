using UnityEngine;
using System.Collections;

public class GameManger : MonoBehaviour {

    public GameObject loadingScreen;

	// Use this for initialization
	void Start () {
	
	}

    public void newLevel() {
        loadingScreen.SetActive(true);
        GetComponent<ItemHandler>().reset();
        GameObject[] walls = GameObject.FindGameObjectsWithTag("Wall");
        foreach (GameObject wall in walls) {
            Destroy(wall);
        }
        GameObject.Find("Plane").GetComponent<TileMap>().CreateMesh();
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerBasics>().Level++;

    }
}
