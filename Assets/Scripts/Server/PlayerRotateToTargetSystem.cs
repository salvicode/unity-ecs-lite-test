namespace Server
{
    using Leopotam.EcsLite;
    using UnityEngine;

    internal class PlayerRotateToTargetSystem : IEcsRunSystem
    {
        private const float RotationSpeed = 520.0f;
        public void Run(EcsSystems systems)
        {
            var world = systems.GetWorld();
            var targets = world.Filter<TargetTag>().End();
            if (targets.GetEntitiesCount() == 0)
            {
                return;
            }

            var target = targets.GetRawEntities()[0];
            var positionPool = world.GetPool<UnitPosition>();
            ref var targetPosition = ref positionPool.Get(target);
            
            var player = world.Filter<PlayerTag>().End().GetRawEntities()[0];
            var rotationPool = world.GetPool<UnitRotation>();
            ref var playerPosition = ref positionPool.Get(player);
            ref var playerRotation = ref rotationPool.Get(player);

            var timeService = systems.GetShared<ITimeServiceProvider>().TimeService;

            // The step size is equal to speed times frame time.
            float singleStep = RotationSpeed * timeService.DeltaTime;
            
            Vector3 targetDirection = (targetPosition.Position - playerPosition.Position).normalized;
            var targetRotation = Quaternion.LookRotation(targetDirection);
            playerRotation.Rotation = Quaternion.RotateTowards(playerRotation.Rotation, targetRotation, singleStep);
        }
    }
}