using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {
    GameObject Player;
    public float x, y, z, newx, newz;
    public float moveSpeed = 1.0f;
    Vector3 pos;
    // Use this for initialization
    void Start () {
        Player = GameObject.FindGameObjectWithTag("Player");
        x = Player.transform.position.x;
        z = Player.transform.position.z;
        y = 22.5f;
    }
	
	// Update is called once per frame
	void Update () {
        x = newx;
        z = newz;
        newx = Player.transform.position.x;
        newz = Player.transform.position.z;
        pos = new Vector3(Mathf.Lerp(x, newx, moveSpeed), y, Mathf.Lerp(z, newz, moveSpeed));
        //pos = new Vector3(newx, y, newz);
        transform.position = pos;
    }
}
