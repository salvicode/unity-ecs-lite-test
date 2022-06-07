using Client.Door;
using Leopotam.EcsLite;

namespace Client
{
    using Server;

    internal class UpdateDoorViewPositionSystem : IEcsRunSystem
    {
        public void Run(EcsSystems systems)
        {
            var world = systems.GetWorld();
            var ecsGameShared = systems.GetShared<EcsGameSharedScript>();
            var doors = world.Filter<DoorComponent>().End();
            var positionPool = world.GetPool<UnitPosition>();
            foreach (var door in doors)
            {
                ref var doorPosition = ref positionPool.Get(door);
                var doorScript = ecsGameShared.EntityMonoBehavioursService.GetMonoBehaviourForEntity<DoorScript>(door);
                doorScript.SetDoorPosition(doorPosition.Position);
            }
        }
    }
}