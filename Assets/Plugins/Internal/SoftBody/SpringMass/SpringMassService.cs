using System;
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
        private Vector3[] stdFullVerticesPosition, currentCutedVerticesPosition, force, velocity;
        private int[] verticesMap, triangles;
        private float defaultSiffness = 0.9f;

        public SpringMassService(GameObject softBodyObject) {
            this.softBodyObject = softBodyObject;
            meshFilter = softBodyObject.GetComponent<MeshFilter>();
            meshRenderer = softBodyObject.GetComponent<MeshRenderer>();

            verticesMapper = new VerticesMapper();
            stdFullVerticesPosition = meshFilter.mesh.vertices;
            currentCutedVerticesPosition = verticesMapper.GetUsableVerticesOnly(meshFilter.mesh, out verticesMap);
            force = new Vector3[currentCutedVerticesPosition.Length];
            velocity = new Vector3[currentCutedVerticesPosition.Length];

            trianglesMapper = new TrianglesMapper();
            triangles = trianglesMapper.ParseTriangles(meshFilter.mesh, ref verticesMap);
            springs = GenerateSprings();

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
                    stdLength = (currentCutedVerticesPosition[triangles[i - 1]] = currentCutedVerticesPosition[triangles[i - 2]]).magnitude,
                    stiffness = defaultSiffness
                };

                newSprings[i - 1] = new Spring
                {
                    vertice1Index = triangles[i - 1],
                    vertice2Index = triangles[i],
                    stdLength = (currentCutedVerticesPosition[triangles[i]] = currentCutedVerticesPosition[triangles[i - 1]]).magnitude,
                    stiffness = defaultSiffness
                };

                newSprings[i] = new Spring
                {
                    vertice1Index = triangles[i],
                    vertice2Index = triangles[i - 2],
                    stdLength = (currentCutedVerticesPosition[triangles[i - 2]] = currentCutedVerticesPosition[triangles[i]]).magnitude,
                    stiffness = defaultSiffness
                };
            }
            return newSprings;
        }
    }
}