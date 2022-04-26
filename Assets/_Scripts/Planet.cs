using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour {
    public float afterDistance, afterRotationalPeriod, afterOrbitalVelocity, orbitalAngle = 0.0f;
    int lengthOfLineRenderer = 100;
    public bool setup = false;
    Color c1 = Color.blue;
    public PlanetGenerator sun;
    
    void DrawOrbit() {
        LineRenderer lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.material = new Material(Shader.Find("Legacy Shaders/Particles/Additive"));
        lineRenderer.startColor = c1;
        lineRenderer.endColor = c1;
        lineRenderer.startWidth = 1.0f;
        lineRenderer.endWidth = 1.0f;
        lineRenderer.positionCount = lengthOfLineRenderer + 1;

        int i = 0;
        while (i <= lengthOfLineRenderer) {
            float unitAngle = (float)(2 * 3.14) / lengthOfLineRenderer;
            float currentAngle = (float)unitAngle * i;

            Vector3 pos = new Vector3(afterDistance * Mathf.Cos(currentAngle), 0, afterDistance * Mathf.Sin(currentAngle));
            lineRenderer.SetPosition(i, pos);
            i++;
        }
    }

    void Update() {
        if (!setup)
            return;
        transform.Rotate(Vector3.up, afterRotationalPeriod * Time.deltaTime, Space.World);
        float tempx, tempy, tempz;
        orbitalAngle += Time.deltaTime * afterOrbitalVelocity;
        tempx = sun.transform.position.x + afterDistance * Mathf.Cos(orbitalAngle);
        tempz = sun.transform.position.z + afterDistance * Mathf.Sin(orbitalAngle);
        tempy = sun.transform.position.y;
        transform.position = new Vector3(tempx, tempy, tempz);
    }


    public void SetupPlanet(float diameter, float distance, float rotationalPeriod, float orbitalVelocity, PlanetGenerator s) {
        sun = s;
        transform.localScale = Vector3.one * diameter * sun.baseDiameter;
        afterDistance = distance * sun.baseDistanceToSun;
        afterRotationalPeriod = rotationalPeriod * sun.baseOrbitalRotationalSpeed;
        afterOrbitalVelocity = orbitalVelocity * sun.baseOrbitalSpeed;
        transform.position = new Vector3(afterDistance, 0, afterDistance);
        //DrawOrbit();

        setup = true;
    }
}
