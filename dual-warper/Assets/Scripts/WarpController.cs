using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarpController : MonoBehaviour {
    public GameObject OtherPortal;

    private void OnTriggerEnter(Collider collider) {
        if (collider.tag == "Player") {
            collider.gameObject.GetComponent<Rigidbody>().position = OtherPortal.transform.position + OtherPortal.transform.forward;
        }
    }
}
