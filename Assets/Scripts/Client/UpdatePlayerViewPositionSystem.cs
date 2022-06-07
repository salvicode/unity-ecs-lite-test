using Leopotam.EcsLite;

namespace Client
{
    using Server;

    public class UpdatePlayerViewPositionSystem : IEcsRunSystem
    {
        public void Run(EcsSystems systems)
        {
            var world = systems.GetWorld();
            var player = world.Filter<PlayerTag>().End().GetRawEntities()[0];
            var unitPositionPool = world.GetPool<UnitPosition>();
            var unitRotationPool = world.GetPool<UnitRotation>();
            var playerScript = systems.GetShared<EcsGameSharedScript>().Player;
            playerScript.SetPosition(unitPositionPool.Get(player).Position);
            playerScript.SetRotation(unitRotationPool.Get(player).Rotation);
        }
    }
}