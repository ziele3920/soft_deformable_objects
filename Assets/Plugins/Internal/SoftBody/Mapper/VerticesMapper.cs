using System.Collections.Generic;
using UnityEngine;

namespace ziele3920.SoftBody.Mapper
{
    public class VerticesMapper : IVerticesMapper
    {
        public Vector3[] GetUsableVerticesOnly(Mesh mesh, out int[] verticesMap) {
            List<Vector3> usableVerices = new List<Vector3>();
            verticesMap = new int[mesh.vertices.Length];

            int vertColeration;
            for(int i = 0; i < mesh.vertices.Length; ++i) {
                if ((vertColeration = IsDubledVertex(mesh.vertices, usableVerices, i)) == -1) {
                    usableVerices.Add(mesh.vertices[i]);
                    verticesMap[i] = usableVerices.Count - 1;
                    continue;
                }
                verticesMap[i] = vertColeration;
            }
            return usableVerices.ToArray();
        }

        public Vector3[] GetOriginalVertices(ref Vector3[] independentVertices, ref int[] vericesMap) {
            Vector3[] originalVertices = new Vector3[vericesMap.Length];

            for (int i = 0; i < vericesMap.Length; ++i)
                originalVertices[i] = independentVertices[vericesMap[i]];

            return originalVertices;
        }

        private int IsDubledVertex(Vector3[] meshVertices, List<Vector3> usableVertices, int index) {
            for (int i = 0; i < usableVertices.Count; ++i)
                if (usableVertices[i] == meshVertices[index])
                    return i;
            return -1;
        }
    }
}
