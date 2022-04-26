using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using NavMeshBuilder = UnityEngine.AI.NavMeshBuilder;

public class NavMeshBaker : MonoBehaviour
{
    [SerializeField] private float bounds;
    private Transform m_Tracked;
    private List<NavMeshBuildSource> m_Sources = new List<NavMeshBuildSource>();
    private NavMeshData m_NavMesh;
    private NavMeshDataInstance m_Instance;
    private Vector3 m_Size;

    public void BakeMap() {
        m_Size = new Vector3(bounds, 1.0f, bounds);
        m_NavMesh = new NavMeshData();
        m_Instance = NavMesh.AddNavMeshData(m_NavMesh);
        m_Sources.Clear();
        #region Adding meshes for baking
        foreach (Transform child in transform) {
            if (child.tag.Equals("NavExempt"))
                continue;
            if (!child.gameObject.activeInHierarchy)
                continue;
            Mesh sMesh = null;
            if (child.TryGetComponent<MeshFilter>(out MeshFilter mesh)) {
                if (mesh != null)
                    sMesh = mesh.sharedMesh;
                else
                    continue;
            }
            MeshFilter meshF = child.GetComponent<MeshFilter>();
            if (meshF == null || sMesh == null) continue;
            var s = new NavMeshBuildSource {
                shape = NavMeshBuildSourceShape.Mesh,
                sourceObject = sMesh,
                transform = meshF.transform.localToWorldMatrix,
                area = 0
            };
            m_Sources.Add(s);
        }

        #endregion
        UpdateNavMesh();
    }

    private void OnDestroy() {
        NavMesh.RemoveAllNavMeshData();
    }

    void UpdateNavMesh() {
        var defaultBuildSettings = NavMesh.GetSettingsByID(0);
        defaultBuildSettings.agentRadius = 0.5f;
        var center = m_Tracked ? m_Tracked.position : transform.position;
        NavMeshBuilder.UpdateNavMeshData(m_NavMesh, defaultBuildSettings, m_Sources, new Bounds(Quantize(center, 0.1f * m_Size), m_Size));

        FindObjectOfType<NPC_Manager>().StartAI();
    }

    static Vector3 Quantize(Vector3 v, Vector3 quant) {
        float x = quant.x * Mathf.Floor(v.x / quant.x);
        float y = quant.y * Mathf.Floor(v.y / quant.y);
        float z = quant.z * Mathf.Floor(v.z / quant.z);
        return new Vector3(x, y, z);
    }
}
