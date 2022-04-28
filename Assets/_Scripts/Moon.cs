using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moon : OrbitingBody {

    //public override void Update() {
    //    base.Update();
    //}

    public void SetupMoon(string orbitingPlanet, float surfaceDistance, float rot, float orb) {
        PlanetGenerator pG = FindObjectOfType<PlanetGenerator>();
        parent = GameObject.Find(orbitingPlanet).transform;
        distance = surfaceDistance * pG.baseDistanceFromSurface + parent.localScale.x/2;
        rotationalPeriod = rot * pG.baseOrbitalRotationalSpeed;
        orbitalVelocity = orb * pG.baseOrbitalSpeed;
        transform.position = new Vector3(parent.position.x + distance, 0, parent.position.z + distance);
        setup = true;
    }
}
