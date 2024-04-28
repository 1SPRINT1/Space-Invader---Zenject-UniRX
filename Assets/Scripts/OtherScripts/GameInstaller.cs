using Zenject;
public class GameInstaller : MonoInstaller<GameInstaller>
{
    public override void InstallBindings()
    {
        Container.BindInterfacesAndSelfTo<GameManager>().AsSingle().NonLazy();
        Container.Bind<Spawner>().FromComponentInHierarchy().AsSingle().NonLazy();
        Container.Bind<ScoreManager>().AsSingle().NonLazy();
        Container.Bind<ProjectTilePool>().FromComponentInHierarchy().AsSingle().NonLazy();
        Container.Bind<Enemy>().FromComponentInHierarchy().AsCached().NonLazy();
        Container.Bind<Pause>().FromComponentInHierarchy().AsCached().NonLazy();
        Container.Bind<EnemyController>().FromComponentInHierarchy().AsCached().NonLazy();
        Container.Bind<EnemyPool>().FromComponentInHierarchy().AsSingle().NonLazy();
        
        // ----------------------------- Interface ----------------------------------------------------------------- 
        Container.Bind<IDescription>().WithId("EnemyController").To<EnemyController>().AsCached();
        Container.Bind<IDescription>().WithId("ShipController").To<ShipController>().AsCached();
        Container.Bind<IDescription>().WithId("ProjectileController").To<ProjectileController>().AsCached();
        
        // --------------------------- MonoBehaviour ------------------------------ 
        Container.Bind<ProjectileController>().FromComponentInHierarchy().AsCached();
        Container.Bind<ShipController>().FromComponentInHierarchy().AsCached();
    }
    
}
