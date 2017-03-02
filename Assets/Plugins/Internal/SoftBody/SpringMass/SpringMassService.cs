using System;
using UnityEngine;

namespace ziele3920.SoftBody.SpringMass
{

    public class SpringMassService : ISpringMassService
    {

        private GameObject softBodyObject;

        public SpringMassService(GameObject softBodyObject) {
            this.softBodyObject = softBodyObject;
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
    }
}