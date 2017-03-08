using System;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using ziele3920.SoftBody.Mapper;

namespace ziele3920.SoftBody.SpringMass
{

    public class SpringMassService : ISpringMassService
    {

        public Vector3[] CurrentVerticesPosition { get; private set; }

        private GameObject softBodyObject;
        private MeshFilter meshFilter;
        private Mesh mesh;
        private MeshRenderer meshRenderer;

        private IVerticesMapper verticesMapper;
        private ITrianglesMapper trianglesMapper;
        private Spring[] springs;
        private Vector3[] stdFullVerticesPosition, position, force, velocity;
        private int[] verticesMap, triangles;
        private float defaultSiffness = 1f;
        private float pointMass = 0.01f;
        private int deltaTime = 20;
        private Thread updatePosLoopThread;

        public SpringMassService(GameObject softBodyObject) {
            this.softBodyObject = softBodyObject;
            meshFilter = softBodyObject.GetComponent<MeshFilter>();
            mesh = meshFilter.mesh;
            meshRenderer = softBodyObject.GetComponent<MeshRenderer>();

            verticesMapper = new VerticesMapper();
            stdFullVerticesPosition = mesh.vertices;
            position = verticesMapper.GetUsableVerticesOnly(mesh, out verticesMap);
            force = new Vector3[position.Length];
            velocity = new Vector3[position.Length];

            trianglesMapper = new TrianglesMapper();
            triangles = trianglesMapper.ParseTriangles(mesh, ref verticesMap);

            ISpringsMapper springsMapper = new SpringsMapper();
            springs = springsMapper.GenerateSprings(ref triangles, ref position, defaultSiffness);

            CurrentVerticesPosition = stdFullVerticesPosition;

            updatePosLoopThread = new Thread(new ThreadStart(UpdatePosition));
            updatePosLoopThread.Start();

        }

        public void OnCollisionEnter(Collision collisionInfo) {
            float forceToAdd = 0.1f;
            for(int i = 0; i < collisionInfo.contacts.Length; ++i) 
                AddForceToVertex(GetNearestCollisionVertex(collisionInfo.contacts[i].point), collisionInfo.contacts[i].normal * forceToAdd);
        }

        public void OnCollisionExit(Collision collisionInfo) {

        }

        public void OnCollisionStay(Collision collisionInfo) {
          //  foreach (ContactPoint contact in collisionInfo.contacts) {

             //   Debug.DrawRay(contact.point, contact.normal, Color.white);
           // }
        }

        public void Dispose() {
            updatePosLoopThread.Abort();
        }

        private void UpdateMeshAndCollider() {
            CurrentVerticesPosition = verticesMapper.GetOriginalVertices(ref position, ref verticesMap);
        }

        private void CountInternalForces() {
            for(int i = 0; i < springs.Length; ++i) {
                //force[springs[i].vertice2Index] -= springs[i].stiffness * ((position[springs[i].vertice1Index] - position[springs[i].vertice2Index]) - springs[i].stdLength * (position[springs[i].vertice1Index] - position[springs[i].vertice2Index]) / (position[springs[i].vertice1Index] - position[springs[i].vertice2Index]).magnitude);
                //force[springs[i].vertice1Index] -= springs[i].stiffness * ((position[springs[i].vertice2Index] - position[springs[i].vertice1Index]) - springs[i].stdLength * (position[springs[i].vertice2Index] - position[springs[i].vertice1Index]) / (position[springs[i].vertice2Index] - position[springs[i].vertice1Index]).magnitude);
                Vector3 forceValue = springs[i].stiffness * ((position[springs[i].vertice1Index] - position[springs[i].vertice2Index]) - springs[i].stdLength * (position[springs[i].vertice1Index] - position[springs[i].vertice2Index]) / (position[springs[i].vertice1Index] - position[springs[i].vertice2Index]).magnitude);
                //if (forceValue.magnitude < 0.00000000001f)
                 //   forceValue = Vector3.zero;
                force[springs[i].vertice2Index] += forceValue;
                force[springs[i].vertice1Index] -= forceValue;
            }
        }

        private void UpdatePosition() {
            while (true) {
                CountInternalForces();
                for (int i = 0; i < position.Length; ++i) {
                    Vector3 acceleration = force[i] / pointMass;
                    velocity[i] += deltaTime/1000f * acceleration;
                    position[i] += deltaTime/1000f * velocity[i];
                }
                for (int i = 0; i < force.Length; ++i) {
                    force[i] = Vector3.zero;
                    velocity[i] = Vector3.zero;
                }
                UpdateMeshAndCollider();
                Thread.Sleep(deltaTime);
            }
        }

        private void AddForceToVertex(int vertexIndex, Vector3 forceToAdd) {
            this.force[vertexIndex] += forceToAdd;
        }

        private int GetNearestCollisionVertex(Vector3 hitGlobalPos) {
            Vector3 localHitPos = softBodyObject.transform.InverseTransformPoint(hitGlobalPos);
            float minDis = float.MaxValue;
            int closestVertexIndex = -1;

            for(int i = 0; i < position.Length; ++i)
                if((localHitPos - position[i]).magnitude < minDis) {
                    minDis = (localHitPos - position[i]).magnitude;
                    closestVertexIndex = i;
                }
            return closestVertexIndex;
        }

    }
}