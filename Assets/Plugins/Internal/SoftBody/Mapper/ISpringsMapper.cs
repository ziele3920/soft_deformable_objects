using UnityEngine;
using ziele3920.SoftBody.SpringMass;

namespace ziele3920.SoftBody.Mapper
{
    public interface ISpringsMapper
    {
        Spring[] GenerateSprings(ref int[] triangles, ref Vector3[] position, float defaultSiffness);
    }
}