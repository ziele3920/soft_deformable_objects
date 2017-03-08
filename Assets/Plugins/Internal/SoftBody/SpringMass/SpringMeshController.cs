using Autofac;
using UnityEngine;

namespace ziele3920.SoftBody.SpringMass
{
    [RequireComponent(typeof(MeshCollider), typeof(MeshFilter))]
    public class SpringMeshController : MonoBehaviour
    {
        private ISpringMassService springMassService;
        private MeshFilter meshFiter;
        private MeshCollider collider;
        
        private void Start() {
            meshFiter = GetComponent<MeshFilter>();
            springMassService = new SpringMassService(this.gameObject);
            collider = GetComponent<MeshCollider>();
        }

        private void Update() {
            meshFiter.mesh.vertices = springMassService.CurrentVerticesPosition;
            meshFiter.mesh.RecalculateNormals();
            collider.sharedMesh = meshFiter.mesh;
        }

        private void OnCollisionEnter(Collision collisionInfo) {
            //Debug.Log("colision enter");
            springMassService.OnCollisionEnter(collisionInfo);
        }

        private void OnCollisionStay(Collision collisionInfo) {
            ///Debug.Log("colision stay");
            springMassService.OnCollisionStay(collisionInfo);
        }

        private void OnCollisionExit(Collision collisionInfo) {
            //Debug.Log("colision exit");
            springMassService.OnCollisionExit(collisionInfo);
        }

        private void OnDestroy() {
            if(springMassService != null)
            springMassService.Dispose();
        }

    }
}
