using UnityEngine;

namespace ziele3920.MeshTrials
{

    public class SimpleMeshGeneratorExample : MonoBehaviour
    {

        private void Start() {
            GenerateObject(GenerateMesh());
        }

        private void GenerateObject(Mesh newMesh) {
            GameObject newObject = new GameObject("newMesh");
            MeshFilter newMeshFilter = newObject.AddComponent<MeshFilter>();
            newMeshFilter.mesh = newMesh;
            MeshRenderer newRenderer = newObject.AddComponent<MeshRenderer>();
            newObject.transform.position = Vector3.one;
        }

        private Mesh GenerateMesh() {

            Mesh newMesh = new Mesh();

            newMesh.vertices = new Vector3[]
            {
            new Vector3(0,0,0),
            new Vector3(1,0,0),
            new Vector3(1,1,0),
            new Vector3(0,1,0)
            };

            newMesh.triangles = new int[]
            {
            0,1,3,1,2,3
            };

            return newMesh;
        }

    }
}
