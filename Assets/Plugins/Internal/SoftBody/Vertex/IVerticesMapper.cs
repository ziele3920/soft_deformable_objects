using UnityEngine;

namespace ziele3920.SoftBody.Vertices
{
    public interface IVerticesMapper
    {
        Vector3[] GetUsableVerticesOnly(Mesh mesh, out int[] verticesMap);
        Vector3[] GetOriginalVertices(Vector3[] independentVertices, int[] vericesMap);
    }
}