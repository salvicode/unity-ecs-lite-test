using Leopotam.EcsLite;
using Server;
using UnityEngine;

namespace Client
{
    public class EcsGameScript : MonoBehaviour
    {
        [SerializeField] 
        private EcsGameSharedScript _ecsGameShared;
        
        private EcsSystems _systems;
        private EcsWorld _world;

        private void Start()
        {
            _world = new EcsWorld();
            _systems = new EcsSystems(_world, _ecsGameShared);

            _systems
                    //Client side systems
                .Add(new PlayerInitSystem())
                .Add(new DoorsInitSystem())
                .Add(new TimeSystem())
                .Add(new PendingTargetMouseClickSystem())
                    
                //Could be done on server
                .Add(new PendingTargetProcessingSystem())
                .Add(new PlayerRotateToTargetSystem())
                .Add(new PlayerMoveToTargetSystem()) // Here target is removed. 
                .Add(new DoorButtonPlayerCollidingSystem())
                
                //Client side views updates
                .Add(new UpdatePlayerViewPositionSystem())
                .Add(new UpdateDoorViewPositionSystem())
                .Init();
        }


        private void Update()
        {
            _systems.Run();
        }

        private void OnDestroy()
        {
            _systems?.Destroy();
            _systems = null;

            _world?.Destroy();
            _world = null;
        }
    }
}