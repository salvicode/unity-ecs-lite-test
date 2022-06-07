using Leopotam.EcsLite;

namespace Client
{
    using Server;

    public class UpdatePlayerViewSystem : IEcsRunSystem
    {
        public void Run(EcsSystems systems)
        {
            var world = systems.GetWorld();
            var player = world.Filter<PlayerTag>().End().GetRawEntities()[0];
            var unitPositionPool = world.GetPool<UnitPosition>();
            var unitRotationPool = world.GetPool<UnitRotation>();
            var playerStatePool = world.GetPool<PlayerStateComponent>();
            var playerScript = systems.GetShared<EcsGameSharedScript>().Player;
            playerScript.SetPosition(unitPositionPool.Get(player).Position);
            playerScript.SetRotation(unitRotationPool.Get(player).Rotation);
            var playerState = playerStatePool.Get(player).State;
            if (playerState == PlayerState.Standing)
            {
                playerScript.StopMoveAnimation();
            }
            else
            {
                playerScript.StartMoveAnimation();
            }
        }
    }
}