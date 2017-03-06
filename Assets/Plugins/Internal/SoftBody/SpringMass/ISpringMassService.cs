using System;
using UnityEngine;

namespace ziele3920.SoftBody.SpringMass
{

    public interface ISpringMassService : IDisposable
    {
        Vector3[] CurrentVerticesPosition { get; }
        void OnCollisionEnter(Collision collisionInfo);
        void OnCollisionStay(Collision collisionInfo);
        void OnCollisionExit(Collision collisionInfo);
    }
}
