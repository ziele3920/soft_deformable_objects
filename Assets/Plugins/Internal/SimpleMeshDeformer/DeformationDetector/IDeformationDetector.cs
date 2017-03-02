using System;

namespace ziele3920.SimpleMeshDeformer
{
    public interface IDeformationDetector : IDisposable
    {
        int DetectionFrequency { get; set; }

        event Action OnDeformation;
    }
}