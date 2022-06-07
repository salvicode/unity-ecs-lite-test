using Leopotam.EcsLite;
using UnityEngine;

namespace Client
{
    using Server;

    public class PendingTargetMouseClickSystem : IEcsRunSystem
    {
        //ToDo: Get this information from scene. Could be done via GroundInitSystem or something like that
        private const int GroundLayerMask = 1 << 9;
        public void Run(EcsSystems systems)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit raycastHit;
                if (Physics.Raycast(ray, out raycastHit, 1000.0f, GroundLayerMask))
                {
                    var world = systems.GetWorld();
                    var pendingTargetEntity = world.NewEntity();
                    var unitTransformPool = world.GetPool<UnitPosition>();
                    ref var targetTransform = ref unitTransformPool.Add(pendingTargetEntity);
                    targetTransform.Position = raycastHit.point;
                    world.GetPool<PendingTargetTag>().Add(pendingTargetEntity);
                }
            }
        }
    }
}