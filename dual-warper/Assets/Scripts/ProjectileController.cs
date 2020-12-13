using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour {
    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.tag != "PortableSurface")
            this.GetComponent<Rigidbody>().position = new Vector3(Random.Range(1000, 10000), Random.Range(1000, 10000), Random.Range(1000, 10000));

        GameObject.FindWithTag("Firepoint").GetComponent<ProjectPortal>().PlacePortal(collision);
    }
}
