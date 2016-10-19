using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MapButton : MonoBehaviour {
    Vector3 map;
    public GameObject loadingScreen;

    public void onClick() {
        switch(this.GetComponentInChildren<Text>().text){
            case "Small":
                loadTheMap(75, 75);
                break;
            case "Medium":
                loadTheMap(100, 100);
                break;
            case "Large":
                loadTheMap(150, 150);
                break;

        }
    }

    void loadTheMap(int sizeX, int sizeY) {
        loadingScreen.SetActive(true);
        GameObject.Find("MainMenuCanvas").SetActive(false);
        GameObject.Find("Plane").GetComponent<TileMap>().CreateMesh(sizeX, sizeY);
    }
}
