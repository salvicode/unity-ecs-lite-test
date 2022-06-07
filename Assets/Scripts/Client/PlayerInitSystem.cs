using Leopotam.EcsLite;

namespace Client
{
    using Server;

    public class PlayerInitSystem : IEcsInitSystem
    {
        public void Init(EcsSystems systems)
        {
            var world = systems.GetWorld();
            var playerEntity = world.NewEntity();
            EcsPool<PlayerTag> playerTagPool = world.GetPool<PlayerTag>();
            playerTagPool.Add(playerEntity);

            EcsPool<UnitPosition> unitPositionPool = world.GetPool<UnitPosition>();
            ref var unitTransformComponent = ref unitPositionPool.Add(playerEntity);
            var ecsGameShared = systems.GetShared<EcsGameSharedScript>();
            unitTransformComponent.Position = ecsGameShared.Player.Position;

            var rotationPool = world.GetPool<UnitRotation>();
            ref var rotation = ref rotationPool.Add(playerEntity);
            rotation.Rotation = ecsGameShared.Player.Rotation;

            var playerStatePool = world.GetPool<PlayerStateComponent>();
            ref var playerState = ref playerStatePool.Add(playerEntity);
            playerState.State = PlayerState.Standing;

            //Since the player is unique in the game probably we don't need to connect it.
            ecsGameShared.EntityMonoBehavioursService.ConnectEntityToMonoBehavior(playerEntity, ecsGameShared.Player);
        }
    }
}