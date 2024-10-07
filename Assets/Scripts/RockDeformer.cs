using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockDeformer : MonoBehaviour {
    private Mesh _mesh;

    // Start is called before the first frame update
    void Start() {
        _mesh = GetComponent<MeshFilter>().mesh;
        Vector3[] verts = _mesh.vertices;
        Vector3[] norms = _mesh.normals;

        for (int i = 0; i < verts.Length; i++) {
            float noise = 0;
            float freq = 1.0f;
            float amp = 0.5f;
            for (int j = 0; j < 3; j++) {
                noise += amp * (Mathf.PerlinNoise(freq * (verts[i].x + transform.position.x), freq * (verts[i].z + transform.position.z)) - 0.5f);
                freq *= 2.01f;
                amp *= 0.5f;
            }
            verts[i] = verts[i] + noise * norms[i];
        }

        _mesh.vertices = verts;
        _mesh.RecalculateNormals();
    }

        // Update is called once per frame
        void Update() {

    }
}
