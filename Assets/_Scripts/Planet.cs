using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour {
    float rotationalSpeed = 10f, orbitalSpeed = .20f,orbitalAngle = 0.0f,angle = 0.0f,orbitalRotationalSpeed = 20f, distanceToSun = 150;
    Color c1 = Color.blue;
    int lengthOfLineRenderer = 100;

    GameObject sun;
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

            Vector3 pos = new Vector3(distanceToSun * Mathf.Cos(currentAngle), 0, distanceToSun * Mathf.Sin(currentAngle));
            lineRenderer.SetPosition(i, pos);
            i++;
        }
    }

    void Start() {
        sun = GameObject.Find("Sun");
        transform.position = new Vector3(distanceToSun, 0, distanceToSun);
        DrawOrbit();

    }

    void Update() {
        transform.Rotate(Vector3.up, rotationalSpeed * Time.deltaTime, Space.World);
        float tempx, tempy, tempz;
        orbitalAngle += Time.deltaTime * orbitalSpeed;
        tempx = sun.transform.position.x + distanceToSun * Mathf.Cos(orbitalAngle);
        tempz = sun.transform.position.z + distanceToSun * Mathf.Sin(orbitalAngle);
        tempy = sun.transform.position.y;
        transform.position = new Vector3(tempx, tempy, tempz);
    }


    public void SetupPlanet(float diameter, float distance, float rotationalPeriod, float orbitalVelocity) {

    }
}
