using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    TO-DOs:
        - Use separate projectiles for either portals (so that any one portal action is not interrupted by another before it's finished)
*/

public class ProjectPortal : MonoBehaviour {
    public GameObject LeftPortal;

    public GameObject RightPortal;

    public GameObject LeftProjectile;

    public GameObject RightProjectile;

    private float ProjectileSpeed = 10.0f;

    public string CurrentPortal;

    void Update() {
        if (Input.GetMouseButton(0)) {
            ShootProjectile();
            CurrentPortal = "Left";
        }
        else if (Input.GetMouseButton(1)) {
            ShootProjectile();
            CurrentPortal = "Right";
        }
    }

    void FixedUpdate() {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        // get the direction of the projectile through vector math
        Vector3 direction = mousePosition - transform.position;
        // covert the direction to an angle in degrees
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        transform.localRotation = Quaternion.identity;
        transform.localPosition = Vector3.zero;
    }

    void ShootProjectile() {
        // get the cursor's position on the screen such that the projectile should fly towards it
        // ScreenToWorldPoint takes the x, y coordinates on the screen as well as the required distance from the camera
        // along the world z-axis (the more exact the z-axis value the more accurate the result)
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, transform.position.z - Camera.main.gameObject.transform.position.z));
        // get the direction of the projectile through vector math
        Vector3 direction = mousePosition - transform.position;
        // convert the direction to an angle in degrees
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // ---- reuse an existing projectile object in the current context ----
        GameObject Projectile = (CurrentPortal == "Right") ? RightProjectile : LeftProjectile;
        // reset initial velocity to 0 so that AddRelativeForce does not continue to add the velocities of the previously used instances of the projectile
        Projectile.GetComponent<Rigidbody>().velocity = Vector3.zero;
        Projectile.transform.position = transform.position;
        Projectile.transform.rotation = Quaternion.Euler(0.0f, 0.0f, angle);
        // --------

        // Add a constant force to the projectile with a speed of ProjectileSpeed along its local x-axis, disregarding its mass
        Projectile.GetComponent<Rigidbody>().AddRelativeForce(new Vector3(ProjectileSpeed, 0.0f, 0.0f), ForceMode.VelocityChange);
    }

    public void PlacePortal(Collision surface) {
        // check if the hit surface allows projection of portals
        if (surface.gameObject.tag != "PortableSurface")
            return;


        GameObject portal = (CurrentPortal == "Left") ? LeftPortal : RightPortal;
        // get the exact point in 3D space in which the collision took place (as opposed to the origin of the surface as is the case with transform.position)
        ContactPoint surfaceContactPoint = surface.GetContact(0);

        portal.transform.position = surfaceContactPoint.point;
        // rotate the portal to face the same direction as the surface using the normal of the single face that collided with the projectile
        portal.transform.rotation = Quaternion.LookRotation(surfaceContactPoint.normal);
    }
}
