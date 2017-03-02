using UnityEngine;

namespace ziele3920.Utilities
{

    public class ColiderColisionDebugger : MonoBehaviour
    {

        private void OnCollisionEnter() {

            Debug.Log("collision");
        }

        private void OnTriggerEnter() {
            Debug.Log("collision");

        }
    }
}
