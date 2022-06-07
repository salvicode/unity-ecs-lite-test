namespace Server
{
    using Leopotam.EcsLite;

    /// <summary>
    /// Takes entity with a component <see cref="PendingTargetTag"/> and creates a Target entity.
    /// If there are several new targets the system will take the first entity with a component <see cref="PendingTargetTag"/>
    /// It will create a target entity with <see cref="TargetTag"/> marker and remove pendingTarget
    /// </summary>
    public class PendingTargetProcessingSystem : IEcsRunSystem
    {
        public void Run(EcsSystems systems)
        {
            var world = systems.GetWorld();
            var pendingTargets = world.Filter<PendingTargetTag>().End();
            if (pendingTargets.GetEntitiesCount() == 0)
            {
                return;
            }

            var oldTargets = world.Filter<TargetTag>().End();
            foreach (var oldTarget in oldTargets)
            {
                world.DelEntity(oldTarget);
            }

            var pendingTarget = pendingTargets.GetRawEntities()[0];
            var target = world.NewEntity();
            var unitPositionPool = world.GetPool<UnitPosition>();
            ref var newTargetPosition = ref unitPositionPool.Get(pendingTarget);
            
            ref var targetPosition = ref unitPositionPool.Add(target);
            targetPosition.Position = newTargetPosition.Position;
            var targetPool = world.GetPool<TargetTag>();
            targetPool.Add(target);
            
            world.DelEntity(pendingTarget);
        }
    }
}