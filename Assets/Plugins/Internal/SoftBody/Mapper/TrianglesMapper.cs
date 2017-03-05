using UnityEngine;

namespace ziele3920.SoftBody.Mapper
{
    public class TrianglesMapper : ITrianglesMapper
    {
        public int[] ParseTriangles(Mesh mesh, ref int[] verticesMap) {

            int[] triangles = new int[mesh.triangles.Length];
            for(int i = 0; i < mesh.tangents.Length; ++i) 
                triangles[i] = verticesMap[triangles[i]];

            return triangles;

        }

    }
}
