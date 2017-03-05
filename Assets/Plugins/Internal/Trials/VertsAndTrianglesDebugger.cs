using UnityEngine;
using ziele3920.SoftBody.Vertices;

namespace ziele3920.MeshTrials
{
    public class VertsAndTrianglesDebugger : MonoBehaviour
    {

        void Start() {
            Mesh m = GetComponent<MeshFilter>().mesh;
            Debug.Log("veritices " + m.vertices.Length + " traingles " + m.triangles.Length);
            string vec = "", tr = "";
            for (int i = 0; i < m.vertices.Length; ++i)
                vec += i.ToString() + m.vertices[i] + " ";
            for (int i = 0; i < m.triangles.Length; ++i) {
                tr += m.triangles[i] + "-";
                if (i % 3 == 2)
                    tr += " | ";

            }
            Debug.Log(vec);
            Debug.Log(tr);
            Debug.Log("triangles count " + m.triangles.Length / 3);
            Debug.Log("vertices.length " + m.vertices.Length);
            Debug.Log("vertexBufferCount " + m.vertexBufferCount);
            Debug.Log("vertexCount " + m.vertexCount);
            VerticesMapper vs = new VerticesMapper();
            int[] dupaczaba;
            vs.GetUsableVerticesOnly(m, out dupaczaba);

        }
    }
}
