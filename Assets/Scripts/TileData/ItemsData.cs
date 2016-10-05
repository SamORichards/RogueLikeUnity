using UnityEngine;
using System.Collections;

public class ItemsData : MonoBehaviour {
    Item i;
    Renderer rend;
     public class Item{
        public int itemType = Random.Range(1, 6);
        /* 1 = Health Pack
         * 2 = Armour
         * 3 = Sword Upgrade
         */
    }
    void Start() {
        rend = GetComponent<Renderer>();
        i = new Item();
        switch (i.itemType) {
            case 1:
                rend.material = GameObject.Find("GameMangers").GetComponent<ItemHandler>().HealhtTexture;
                break;
            case 2:
                rend.material = GameObject.Find("GameMangers").GetComponent<ItemHandler>().AmourTexture;
                break;
            case 3:
                rend.material = GameObject.Find("GameMangers").GetComponent<ItemHandler>().WeaponUpgradeTexture;
                break;
            default:
                rend.material = GameObject.Find("GameMangers").GetComponent<ItemHandler>().HealhtTexture;
                break;
        }
    }
    void OnTriggerEnter(Collider collider) {

        if (collider.tag == "Player") {
            gameObject.SetActive(false);
            collider.GetComponent<PlayerBasics>().hitItem(i);
        }
    }
}
