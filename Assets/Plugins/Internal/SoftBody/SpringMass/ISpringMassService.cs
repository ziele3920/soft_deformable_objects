using System;
using UnityEngine;

namespace ziele3920.SoftBody.SpringMass
{

    public interface ISpringMassService : IDisposable
    {
        void OnCollisionEnter(Collision collisionInfo);
        void OnCollisionStay(Collision collisionInfo);
        void OnCollisionExit(Collision collisionInfo);
    }
}
