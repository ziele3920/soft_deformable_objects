using System.Collections.Generic;
using UnityEngine;
using ziele3920.SoftBody.SpringMass;

namespace ziele3920.SoftBody.Mapper
{
    public class SpringsMapper : ISpringsMapper
    {
        public Spring[] GenerateSprings(ref int[] triangles, ref Vector3[] position, float defaultSiffness) {
            Spring[] newSprings = new Spring[triangles.Length];
            for (int i = 2; i < triangles.Length; i += 3) {
                newSprings[i - 2] = new Spring
                {
                    vertice1Index = triangles[i - 2],
                    vertice2Index = triangles[i - 1],
                    stdLength = (position[triangles[i - 1]] - position[triangles[i - 2]]).magnitude,
                    stiffness = defaultSiffness
                };

                newSprings[i - 1] = new Spring
                {
                    vertice1Index = triangles[i - 1],
                    vertice2Index = triangles[i],
                    stdLength = (position[triangles[i]] - position[triangles[i - 1]]).magnitude,
                    stiffness = defaultSiffness
                };

                newSprings[i] = new Spring
                {
                    vertice1Index = triangles[i],
                    vertice2Index = triangles[i - 2],
                    stdLength = (position[triangles[i - 2]] - position[triangles[i]]).magnitude,
                    stiffness = defaultSiffness
                };
            }
            List<Spring> newSpringsWithoutDubled = new List<Spring>();
            for (int i = 0; i < newSprings.Length; ++i) {
                if (ListContainSpring(newSpringsWithoutDubled, newSprings[i]))
                    continue;
                newSpringsWithoutDubled.Add(newSprings[i]);
            }
            return newSpringsWithoutDubled.ToArray();
        }

        private bool ListContainSpring(List<Spring> springsList, Spring spring) {
            for (int i = 0; i < springsList.Count; ++i)
                if ((springsList[i].vertice1Index == spring.vertice2Index && springsList[i].vertice2Index == spring.vertice1Index) ||
                    (springsList[i].vertice1Index == spring.vertice1Index && springsList[i].vertice2Index == spring.vertice2Index))
                    return true;
            return false;
        }
    }
}
