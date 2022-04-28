using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitingBody : MonoBehaviour {
    public float distance, rotationalPeriod, orbitalVelocity, orbitalAngle = 0.0f;
    public bool setup = false;
    public Transform parent;

    public virtual void Update() {
        if (!setup)
            return;
        transform.Rotate(Vector3.up, rotationalPeriod * Time.deltaTime, Space.World);
        float tempx, tempy, tempz;
        orbitalAngle += Time.deltaTime * orbitalVelocity;
        tempx = parent.position.x + distance * Mathf.Cos(orbitalAngle);
        tempz = parent.position.z + distance * Mathf.Sin(orbitalAngle);
        tempy = parent.position.y;
        transform.position = new Vector3(tempx, tempy, tempz);
    }

    public virtual void SetupOrbitingBody(float diam, float dis, float rot, float orb, Transform s) {
        PlanetGenerator pG = FindObjectOfType<PlanetGenerator>();
        parent = s;
        transform.localScale = Vector3.one * diam * pG.baseDiameter;
        distance = dis * pG.baseDistanceToSun;
        rotationalPeriod = rot * pG.baseOrbitalRotationalSpeed;
        orbitalVelocity = orb * pG.baseOrbitalSpeed;
        transform.position = new Vector3(distance, 0, distance);
        setup = true;
    }
}