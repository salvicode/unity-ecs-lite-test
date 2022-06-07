
namespace Client
{
    using Leopotam.EcsLite;
    using Server;

    internal class DoorsInitSystem : IEcsInitSystem
    {
        public void Init(EcsSystems systems)
        {
            var ecsGameShared = systems.GetShared<EcsGameSharedScript>();
            var world = systems.GetWorld();
            var unitPositionPool = world.GetPool<UnitPosition>();
            var doorPool = world.GetPool<DoorComponent>();
            foreach (var doorScript in ecsGameShared.DoorsContainer.Doors)
            {
                var doorEntity = world.NewEntity();
                
                ref var doorPosition = ref unitPositionPool.Add(doorEntity);
                doorPosition.Position = doorScript.DoorPosition;
                
                ref var doorComponent = ref doorPool.Add(doorEntity);
                doorComponent.ButtonPosition = doorScript.DoorButtonPosition;
                doorComponent.ClosePosition = doorScript.ClosePosition;
                doorComponent.OpenPosition = doorScript.OpenPosition;
                doorComponent.ButtonRadius = doorScript.DoorButtonRadius;
                doorComponent.DoorSpeed = doorScript.Speed;
                
                ecsGameShared.EntityMonoBehavioursService.ConnectEntityToMonoBehavior(doorEntity, doorScript);
            }
        }
    }

    //ToDo: Should be broken into several components?
}