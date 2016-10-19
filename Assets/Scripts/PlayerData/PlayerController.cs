using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
    public Vector3 velocity;
    float moveY, moveX;
    public int moveSpeed = 6;
    public playerControl pC;


    public void setSpawn(TMData map, int sizeX, int sizeY) {
        bool canSpawn = false;

        while (canSpawn == false) {
            int x = Random.Range(1, sizeX);
            int y = Random.Range(1, sizeY);
            if ((int)map.getTileAt(x,y) == (int)TMData.TileType.FLOOR_TILE) {
                Vector3 vd = new Vector3(x, 0, -sizeY + y);
                transform.position = vd;
                 
                canSpawn = true;
            }
        }

    }

    // Update is called once per frame
    void Update() {

        moveY = Input.GetAxis("Vertical");
        moveX = Input.GetAxis("Horizontal");
    }

    void FixedUpdate() {
        velocity = new Vector3(moveX * moveSpeed, 0, moveY * moveSpeed);
        CharacterController cc = GetComponent<CharacterController>();
        if (velocity.y > 0 || velocity.x > 0 || velocity.z > 0)
        {
            pC.BattleWalkForward();
        }
        cc.SimpleMove(velocity);

    }
}
