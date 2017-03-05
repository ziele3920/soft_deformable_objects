using System;
using System.Collections.Generic;
using UnityEngine;
using ziele3920.SoftBody.Mapper;

namespace ziele3920.SoftBody.SpringMass
{

    public class SpringMassService : ISpringMassService
    {

        private GameObject softBodyObject;
        private MeshFilter meshFilter;
        private MeshRenderer meshRenderer;

        private IVerticesMapper verticesMapper;
        private ITrianglesMapper trianglesMapper;
        private Spring[] springs;
        private Vector3[] stdFullVerticesPosition, position, force, velocity;
        private int[] verticesMap, triangles;
        private float defaultSiffness = 1f;

        public SpringMassService(GameObject softBodyObject) {
            this.softBodyObject = softBodyObject;
            meshFilter = softBodyObject.GetComponent<MeshFilter>();
            meshRenderer = softBodyObject.GetComponent<MeshRenderer>();

            verticesMapper = new VerticesMapper();
            stdFullVerticesPosition = meshFilter.mesh.vertices;
            position = verticesMapper.GetUsableVerticesOnly(meshFilter.mesh, out verticesMap);
            force = new Vector3[position.Length];
            velocity = new Vector3[position.Length];

            trianglesMapper = new TrianglesMapper();
            triangles = trianglesMapper.ParseTriangles(meshFilter.mesh, ref verticesMap);
            springs = GenerateSprings();
            CountInternalForces();
        }

        public void OnCollisionEnter(Collision collisionInfo) {
            throw new NotImplementedException();
        }

        public void OnCollisionExit(Collision collisionInfo) {
            throw new NotImplementedException();
        }

        public void OnCollisionStay(Collision collisionInfo) {
            throw new NotImplementedException();
        }

        public void Dispose() {
            throw new NotImplementedException();
        }
        private Spring[] GenerateSprings() {
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
            for(int i = 0; i < newSprings.Length; ++i) {
                if (ListContainSpring(newSpringsWithoutDubled, newSprings[i]))
                    continue;
                newSpringsWithoutDubled.Add(newSprings[i]);
            }
            return newSpringsWithoutDubled.ToArray();
        }

        private bool ListContainSpring(List<Spring> springsList, Spring spring) {
            for(int i = 0; i < springsList.Count; ++i) 
                if ((springsList[i].vertice1Index == spring.vertice2Index && springsList[i].vertice2Index == spring.vertice1Index) ||
                    (springsList[i].vertice1Index == spring.vertice1Index && springsList[i].vertice2Index == spring.vertice2Index))
                    return true;
            return false;
        }

        private void CountInternalForces() {
            for(int i = 0; i < springs.Length; ++i) {
                //force[springs[i].vertice2Index] -= springs[i].stiffness * ((position[springs[i].vertice1Index] - position[springs[i].vertice2Index]) - springs[i].stdLength * (position[springs[i].vertice1Index] - position[springs[i].vertice2Index]) / (position[springs[i].vertice1Index] - position[springs[i].vertice2Index]).magnitude);
                //force[springs[i].vertice1Index] -= springs[i].stiffness * ((position[springs[i].vertice2Index] - position[springs[i].vertice1Index]) - springs[i].stdLength * (position[springs[i].vertice2Index] - position[springs[i].vertice1Index]) / (position[springs[i].vertice2Index] - position[springs[i].vertice1Index]).magnitude);
                Vector3 forceValue = springs[i].stiffness * ((position[springs[i].vertice1Index] - position[springs[i].vertice2Index]) - springs[i].stdLength * (position[springs[i].vertice1Index] - position[springs[i].vertice2Index]) / (position[springs[i].vertice1Index] - position[springs[i].vertice2Index]).magnitude);
                force[springs[i].vertice2Index] -= forceValue;
                force[springs[i].vertice1Index] += forceValue;
            }
        }
    }
}