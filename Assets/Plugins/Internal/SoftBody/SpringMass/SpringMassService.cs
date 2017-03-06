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
        private float deltaTime, pointMass;

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

            ISpringsMapper springsMapper = new SpringsMapper();
            springs = springsMapper.GenerateSprings(ref triangles, ref position, defaultSiffness);
            CountInternalForces();
        }

        public void OnCollisionEnter(Collision collisionInfo) {
            //collisionInfo.contacts[0].
        }

        public void OnCollisionExit(Collision collisionInfo) {
            throw new NotImplementedException();
        }

        public void OnCollisionStay(Collision collisionInfo) {
            foreach (ContactPoint contact in collisionInfo.contacts) {
               // print(contact.thisCollider.name + " hit " + contact.otherCollider.name);
                Debug.DrawRay(contact.point, contact.normal, Color.white);
            }
        }

        public void Dispose() {
            throw new NotImplementedException();
        }

        private void UpdateMeshAndCollider() {
            meshFilter.mesh.vertices = verticesMapper.GetOriginalVertices(ref position, ref verticesMap);
            meshFilter.mesh.RecalculateNormals();
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

        private void UpdatePosition() {
            for (int i = 0; i < position.Length; ++i) {
                Vector3 acceleration = force[i] / pointMass;
                velocity[i] += deltaTime * acceleration;
                position[i] += deltaTime * velocity[i];
            }
        }

    }
}