using UnityEngine;

namespace ziele3920.SimpleMeshDeformer
{
    [RequireComponent(typeof(SkinnedMeshRenderer))]
    public class SkinnedMeshDuplicator : MonoBehaviour
    {

        private SkinnedMeshRenderer skinnedMeshrenderer;

        void Start() {
            skinnedMeshrenderer = GetComponent<SkinnedMeshRenderer>();
        }

        void Update() {
            if (Input.GetKeyDown(KeyCode.S))
                Snapshot();
        }

        private void Snapshot() {
            Mesh newMesh = new Mesh();
            newMesh.vertices = GetComponent<Cloth>().vertices;
            newMesh.triangles = buildTriangles(newMesh.vertices.Length * 3);

            GenerateObject(newMesh);
        }

        private void GenerateObject(Mesh newMesh) {
            GameObject newObject = new GameObject("newSphere");
            MeshFilter newMeshFilter = newObject.AddComponent<MeshFilter>();
            newMeshFilter.mesh = newMesh;
            MeshRenderer newRenderer = newObject.AddComponent<MeshRenderer>();
            newObject.transform.position = Vector3.one;
        }
        int[] buildTriangles(int size) {
            int[] triangles = new int[size];
            int nbLong = 24;
            int nbLat = 16;
            int i = 0;
            for (int lon = 0; lon < nbLong; lon++) {
                triangles[i++] = lon + 2;
                triangles[i++] = lon + 1;
                triangles[i++] = 0;
            }


            for (int lat = 0; lat < nbLat - 1; lat++) {
                for (int lon = 0; lon < nbLong; lon++) {
                    int current = lon + lat * (nbLong + 1) + 1;
                    int next = current + nbLong + 1;

                    triangles[i++] = current;
                    triangles[i++] = current + 1;
                    triangles[i++] = next + 1;

                    triangles[i++] = current;
                    triangles[i++] = next + 1;
                    triangles[i++] = next;
                }
            }


            for (int lon = 0; lon < nbLong; lon++) {
                triangles[i++] = size / 3 - 1;
                triangles[i++] = size / 3 - (lon + 2) - 1;
                triangles[i++] = size / 3 - (lon + 1) - 1;
            }

            return triangles;
        }
    }
}