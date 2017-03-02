using System;
using System.Collections;
using System.Timers;
using UnityEngine;

namespace ziele3920.SimpleMeshDeformer
{
    public class VerticesDeformationDetector : MonoBehaviour, IDeformationDetector {

        public event Action OnDeformation;
        public int DetectionFrequency
        {
            get {
                return detectionFrequency;
            }
            set {
                detectionFrequency = value;
                refreshPeriodTime = 1f / detectionFrequency;
            }
        }
        private int detectionFrequency;
        //[s]
        private float refreshPeriodTime;
        private Cloth cloth;
        private Vector3[] stdVertices;
        private float deformationDistanceTrigger;

        private void Start() {
            InitializeCloth();
            InitializeTimer();
        }

        private void InitializeTimer() {
            DetectionFrequency = 30;
            StartCoroutine(CheckDeformation());
        }

        private void InitializeCloth() {
            this.cloth = GetComponent<Cloth>();
            deformationDistanceTrigger = cloth.sleepThreshold/100;
            stdVertices = new Vector3[cloth.vertices.Length];
            for (int i = 0; i < cloth.vertices.Length; ++i)
                stdVertices[i] = new Vector3(cloth.vertices[i].x, cloth.vertices[i].y, cloth.vertices[i].z);
        }

        private IEnumerator CheckDeformation() {
            while (true) {
                for (int i = 0; i < stdVertices.Length; ++i) {
                    if (Vector3.Distance(stdVertices[i], cloth.vertices[i]) > deformationDistanceTrigger) {
                        if (OnDeformation != null)
                            OnDeformation();
                        break;
                    }
                }
                yield return new WaitForSeconds(refreshPeriodTime);
            }
        }

        public void Dispose() {
            StopAllCoroutines();
        }
    }
}
