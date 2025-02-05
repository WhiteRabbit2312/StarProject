using UnityEngine;
using Zenject;

namespace StarProject
{
    public class DatabaseInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<Database>().AsSingle();
        }
    }
}