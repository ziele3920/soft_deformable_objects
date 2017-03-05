using UnityEngine;

namespace ziele3920.SoftBody.Mapper
{
    public class TrianglesMapper : ITrianglesMapper
    {
        public int[] ParseTriangles(Mesh mesh, ref int[] verticesMap) {

            int[] triangles = new int[mesh.triangles.Length];
            for(int i = 0; i < mesh.triangles.Length; ++i) 
                triangles[i] = verticesMap[mesh.triangles[i]];

            return triangles;

        }

    }
}
