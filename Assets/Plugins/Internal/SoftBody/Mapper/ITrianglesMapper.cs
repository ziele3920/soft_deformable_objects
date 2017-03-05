using UnityEngine;

namespace ziele3920.SoftBody.Mapper
{
    public interface ITrianglesMapper
    {
        int[] ParseTriangles(Mesh mesh, ref int[] verticesMap);
    }
}