namespace Server
{
    using Leopotam.EcsLite;
    using UnityEngine;

    internal class DoorButtonPlayerCollidingSystem : IEcsRunSystem
    {
        public void Run(EcsSystems systems)
        {
            var world = systems.GetWorld();
            var unitPositionPool = world.GetPool<UnitPosition>();
            var player = world.Filter<PlayerTag>().End().GetRawEntities()[0];
            ref var playerPosition = ref unitPositionPool.Get(player);
            
            var doors = world.Filter<DoorComponent>().End();
            var doorPool = world.GetPool<DoorComponent>();
            var timeService = systems.GetShared<ITimeServiceProvider>().TimeService;
            foreach (var door in doors)
            { 
                ref var doorComponent = ref doorPool.Get(door);
                ref var doorPosition = ref unitPositionPool.Get(door);
                
                if ((playerPosition.Position - doorComponent.ButtonPosition).sqrMagnitude <=
                    doorComponent.ButtonRadius * doorComponent.ButtonRadius)
                {
                    float step = doorComponent.DoorSpeed * timeService.DeltaTime;
                    doorPosition.Position = Vector3.MoveTowards(doorPosition.Position, doorComponent.OpenPosition, step);
                }
            }
        }
    }
}