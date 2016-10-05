using UnityEngine;
using System.Collections;
using System;

public class EnemiesHandler : MonoBehaviour {
    public GameObject EnemiesManger;
    public GameObject Enemy;
    int enemiesNum;
    int enemyNumOrginal;

    public void spawnEnemies(TMData map, int sizeX, int sizeY, int[,] itemMap)
    {

        enemiesNum = (sizeX * sizeY) / 750;
        enemyNumOrginal = enemiesNum;

        while (enemiesNum > 0)
        {
            int x = UnityEngine.Random.Range(1, sizeX);
            int y = UnityEngine.Random.Range(1, sizeY);
            if ((int)map.getTileAt(x, y) == (int)TMData.TileType.FLOOR_TILE && itemMap[x,y] == 0)
            {
                float totY = -sizeY + y + 0.5f;
                Vector3 pos = new Vector3(x + 0.5f, 0.001f, totY);
                Quaternion rot = new Quaternion(0, 0, 0, 0);
                Instantiate(Enemy, pos, rot);
                enemiesNum--;
            }
        }
        foreach (GameObject itemObject in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            itemObject.transform.parent = EnemiesManger.transform;
        }
        Debug.Log(enemyNumOrginal + " Enemies Spawned");

    }

    internal void enemyDied()
    {
        enemyNumOrginal--;
        Debug.Log(enemyNumOrginal);
        if (enemyNumOrginal == 0) {
            GetComponent<GameManger>().newLevel();
        }
    }
}
