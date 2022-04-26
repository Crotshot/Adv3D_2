using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerlinMap : MonoBehaviour {
    [SerializeField] [Range(10, 100)] private int mapDimes;
    [SerializeField] [Range(0, 100)] private float size, frequency, scale;
    public Vector3[] points;

    void Start() {
        CalulateNoise();
    }

    public bool remakeMap;
    private void Update() {
        if (remakeMap) {
            remakeMap = false;
            RemakeMap();
        }
    }

    void CalulateNoise() {
        points = new Vector3[mapDimes * mapDimes];
        int count = 0;
        for (int z = 0; z < mapDimes; z++) {
            for (int x = 0; x < mapDimes; x++) {
                points[count] = new Vector3((x- mapDimes / 2) * size,
                    Mathf.Round(Mathf.PerlinNoise((transform.position.x + x) * 1.0f / frequency + 0.1f, (transform.position.z + z) * 1.0f / frequency + 0.1f) * scale),
                    (z - mapDimes / 2) * size);
                count++;
            }
        }
        Mesh mesh = CreateMapMesh();
        GetComponent<MeshFilter>().mesh = mesh;
        GetComponent<MeshCollider>().sharedMesh = mesh;
    }

    private void RemakeMap() {
        CalulateNoise();
    }

    private Mesh CreateMapMesh() {//Using a mesh a to visualize the area of vision the AI sensor has
        int[] tris = new int[(mapDimes * mapDimes - mapDimes * 2 + 1) * 6];
        Mesh mesh = new Mesh();

        int c = 0, s = 0;
        for (int i = 0; i < points.Length - mapDimes-1; i++) {
            if (s == mapDimes-1) {
                Debug.Log("Skipped Edge:" + i);
                s = 0;
                continue; //Skip the right edge of the grid
            }
            s += 1;
            //Tri 1
            tris[c] = i;
            c++;
            tris[c] = i + mapDimes;
            c++;
            tris[c] = i + 1;
            c++;
            //Tri 2
            tris[c] = i + 1;
            c++;
            tris[c] = i + mapDimes;
            c++;
            tris[c] = i + mapDimes+1;
            c++;

        }


        mesh.vertices = points;
        mesh.triangles = tris;
        mesh.RecalculateNormals();
        return mesh;
    }
}
