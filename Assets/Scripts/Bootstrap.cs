using UnityEngine;
using Zenject;

public class Bootstrap : MonoInstaller
{
    public override void InstallBindings()
    {
        // Container.InstantiatePrefabForComponent().Bind<Bird>().AsSingle();
    }

}