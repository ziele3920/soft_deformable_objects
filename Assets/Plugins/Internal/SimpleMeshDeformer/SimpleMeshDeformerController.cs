using UnityEngine;

namespace ziele3920.SimpleMeshDeformer
{

    [RequireComponent(typeof(Cloth))]
    public class SimpleMeshDeformerController : MonoBehaviour
    {
        private Cloth cloth;
        private IDeformationDetector deformationDetector;
        private int deformationDetectionFrequency = 30;

        void Start() {
            cloth = GetComponent<Cloth>();
            deformationDetector = gameObject.AddComponent<VerticesDeformationDetector>();
            deformationDetector.DetectionFrequency = deformationDetectionFrequency;
            deformationDetector.OnDeformation += OnDeformation;
        }

        private void OnDeformation() {
            Debug.Log("Deformation!");
        }

        private void OnDestroy() {
            if (deformationDetector != null)
                deformationDetector.Dispose();
        }
    }
}