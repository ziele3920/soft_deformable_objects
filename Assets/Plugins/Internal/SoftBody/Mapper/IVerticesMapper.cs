using UnityEngine;

namespace ziele3920.SoftBody.Mapper
{
    public interface IVerticesMapper
    {
        Vector3[] GetUsableVerticesOnly(Mesh mesh, out int[] verticesMap);
        Vector3[] GetOriginalVertices(ref Vector3[] independentVertices, ref int[] vericesMap);
    }
}