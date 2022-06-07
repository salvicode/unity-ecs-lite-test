namespace Server
{
    using Leopotam.EcsLite;
    using UnityEngine;

    public class PlayerMoveToTargetSystem : IEcsRunSystem
    {
        private const float MoveSpeed = 4.0f;
        private const float TargetRemovalThreshold = 0.05f;
        
        public void Run(EcsSystems systems)
        {
            var world = systems.GetWorld();
            var targets = world.Filter<TargetTag>().End();
            if (targets.GetEntitiesCount() == 0)
            {
                return;
            }
            var target = targets.GetRawEntities()[0];
            var player = world.Filter<PlayerTag>().End().GetRawEntities()[0];
            var unitPositionPool = world.GetPool<UnitPosition>();
            ref var playerPosition = ref unitPositionPool.Get(player);
            var time = systems.GetShared<ITimeServiceProvider>().TimeService.DeltaTime;
            float step = MoveSpeed * time;
            var targetPosition = unitPositionPool.Get(target).Position;
            playerPosition.Position = Vector3.MoveTowards(playerPosition.Position, targetPosition, step);
            
            //Basically can be moved to another system responsible for removing target
            if ((targetPosition - playerPosition.Position).sqrMagnitude <= TargetRemovalThreshold)
            {
                world.DelEntity(target);
            }
        }
    }
}