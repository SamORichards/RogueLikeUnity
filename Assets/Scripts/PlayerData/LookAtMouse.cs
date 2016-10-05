using UnityEngine;
using System.Collections;

public class LookAtMouse : MonoBehaviour {
    GameObject Player;
    PlayerBasics PB;

    void Start() {
        Player = GameObject.FindGameObjectWithTag ("Player");
        PB = Player.GetComponent<PlayerBasics>();
    }
    void Update()
    {

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;

        if (GetComponent<Collider>().Raycast(ray, out hitInfo, Mathf.Infinity))
        {
            PB.LookAtPoint(hitInfo.point);
        }
    }

}
