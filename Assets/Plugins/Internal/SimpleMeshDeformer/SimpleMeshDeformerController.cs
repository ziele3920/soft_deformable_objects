using System;
using UnityEngine;
//[RequireComponent(typeof(MeshFilter), typeof(SkinnedMeshRenderer))]
public class SimpleMeshDeformerController : MonoBehaviour {

    private SkinnedMeshRenderer meshRenderer;
    private Vector3[] verticles;
    Mesh m;


    void Start () {
        meshRenderer = GetComponent<SkinnedMeshRenderer>();
        verticles = GetVec();
        //meshRenderer.sharedMesh.SetVertices(new System.Collections.Generic.List<Vector3>(verticles));

    }

	void Update () {

        if (VericlechChanged())
            Debug.Log("mesh deformed");
        else
            Debug.Log(meshRenderer.sharedMesh.vertices.Length);

        }

    private bool VericlechChanged() {
        for(int i = 0; i < verticles.Length; ++i) {
            if (meshRenderer.sharedMesh.vertices[i].x != verticles[i].x ||
                meshRenderer.sharedMesh.vertices[i].y != verticles[i].y ||
                meshRenderer.sharedMesh.vertices[i].z != verticles[i].z)
                return true;
        }
        return false;
    }

    private Vector3[] GetVec() {

        Vector3[] newVec = new Vector3[meshRenderer.sharedMesh.vertices.Length];

        for (int i = 0; i < meshRenderer.sharedMesh.vertices.Length; ++i)
            newVec[i] = new Vector3(meshRenderer.sharedMesh.vertices[i].x, meshRenderer.sharedMesh.vertices[i].y, meshRenderer.sharedMesh.vertices[i].z);
        return newVec;

    }
}
