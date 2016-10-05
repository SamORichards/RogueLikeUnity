using UnityEngine;
using System.Collections;
using System;

public class SwordScript : MonoBehaviour {
    Quaternion q;
    Vector3 temp;
    Vector3 old;
    void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Enemy" && GetComponentInParent<PlayerBasics>().isAttacking)
        {
            Debug.Log("Hit the Enemy!!");
            collider.GetComponent<EnemyAI>().beenHit(GetComponentInParent<PlayerBasics>().SwordDMG);
        }
    }

    public void swingSword() {
        old = transform.rotation.eulerAngles;
        temp.x = transform.rotation.eulerAngles.x;
        temp.y= transform.rotation.eulerAngles.y;
        temp.z = 90;
        transform.rotation = Quaternion.Lerp(Quaternion.Euler(old), Quaternion.Euler(temp), Time.deltaTime *100);

    }

    internal void stopSwingSword()
    {
        old = transform.rotation.eulerAngles;
        temp.x = transform.rotation.eulerAngles.x;
        temp.y = transform.rotation.eulerAngles.y;
        temp.z = 0;
        transform.rotation = Quaternion.Lerp(Quaternion.Euler(old), Quaternion.Euler(temp), Time.deltaTime * 100);
    }
}
