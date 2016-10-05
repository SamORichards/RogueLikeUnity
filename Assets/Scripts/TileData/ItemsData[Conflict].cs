using UnityEngine;
using System.Collections;

public class ItemsData : MonoBehaviour {
    Item i;
     public class Item{    
    }
    void Start() {
        i = new Item();
    }
    void OnTriggerEnter(Collider collider) {

        if (collider.tag == "Player") {
            gameObject.SetActive(false);
            collider.GetComponent<PlayerBasics>().hitItem(i);
        }
    }
}
