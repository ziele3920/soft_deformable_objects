using UnityEngine;
using Autofac;

public class Bootstrapper : MonoBehaviour {
    void Awake() {
        var builder = new ContainerBuilder();
        DependencyResolver.Container = builder.Build();
    }


}