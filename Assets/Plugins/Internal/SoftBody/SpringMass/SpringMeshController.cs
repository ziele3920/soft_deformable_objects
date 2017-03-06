using Autofac;
using UnityEngine;

namespace ziele3920.SoftBody.SpringMass
{
    [RequireComponent(typeof(Collider), typeof(MeshFilter))]
    public class SpringMeshController : MonoBehaviour
    {
        private ISpringMassService springMassService;
        
        private void Start() {
            springMassService = new SpringMassService(this.gameObject);
        }

        private void OnCollisionEnter(Collision collisionInfo) {
            Debug.Log("colision enter");
            springMassService.OnCollisionEnter(collisionInfo);
        }

        private void OnCollisionStay(Collision collisionInfo) {
            Debug.Log("colision stay");
            springMassService.OnCollisionStay(collisionInfo);
        }

        private void OnCollisionExit(Collision collisionInfo) {
            Debug.Log("colision exit");
            springMassService.OnCollisionExit(collisionInfo);
        }

    }
}
