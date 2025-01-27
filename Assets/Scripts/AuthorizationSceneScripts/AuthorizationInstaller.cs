using UnityEngine;
using Zenject;

namespace StarProject
{
    public class AuthorizationInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<Authorization>().AsSingle();
            Container.Bind<CheckAuthorization>().AsSingle();
        }
    }
}