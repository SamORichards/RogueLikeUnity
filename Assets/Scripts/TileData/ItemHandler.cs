using UnityEngine;
using System.Collections;

public class ItemHandler : MonoBehaviour {
    public GameObject ItemsManger;
    public GameObject item;
    public Material HealhtTexture, WeaponUpgradeTexture, AmourTexture;
    int[,] itemMap;
    GameObject[] itemsList;
    


    public void spawnObjects(TMData map, int sizeX, int sizeY, float tileSize) {
        int itemNum = (sizeX * sizeY) / 750;
        int itemNumOrigianl = itemNum;
        float tempX = sizeX * tileSize;
        float tempY = sizeY * tileSize;
        itemMap = new int[(int)tempX, (int) tempY];
        for (int x = 0; x < sizeX * tileSize; x++) {
            for (int y = 0; y < sizeY * tileSize; y++)
            {
                itemMap[x, y] = 0;
            }
        }
        while (itemNum > 0) {
            int x = (int)Random.Range(1, sizeX * tileSize);
            int y = (int)Random.Range(1, sizeY * tileSize);
            if ((int)map.getTileAt(x, y) == (int)TMData.TileType.FLOOR_TILE) {
                float totY = -sizeY + y + 0.5f;
                Vector3 pos = new Vector3(x + 0.5f, 0.6f, totY);
                Quaternion rot = new Quaternion(0, 180, 0, 0);
                Instantiate(item, pos, rot);
                itemMap[x, y] = 1;
                itemNum--;
            }
        }
        itemsList = GameObject.FindGameObjectsWithTag("Item");
        foreach (GameObject itemObject in itemsList)
        {
            itemObject.transform.parent = ItemsManger.transform;
        }
        Debug.Log(itemNumOrigianl + " Items Spawned");
        GetComponent<EnemiesHandler>().spawnEnemies(map, sizeX, sizeY, tileSize, itemMap);

    }
    public void reset() {
        foreach (GameObject itemObject in itemsList) {
            Destroy(itemObject);
        }
    }
}
