using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : OrbitingBody {
    
}

/*
    Color c1 = Color.blue
    int lengthOfLineRenderer = 100;

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
*/
