using UnityEngine;

namespace ziele3920.Utilities
{

    public class SimpleMove : MonoBehaviour
    {

        void Update() {

            if (Input.GetKey(KeyCode.UpArrow)) 
                transform.position = transform.position + (transform.up * Time.deltaTime * 0.3f);

            if (Input.GetKey(KeyCode.DownArrow)) 
                transform.position = transform.position - (transform.up * Time.deltaTime * 0.3f);

        }
    }
}
