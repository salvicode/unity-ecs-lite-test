using Leopotam.EcsLite;
using UnityEngine;

namespace Client
{
    using Server;

    public class TimeSystem : IEcsRunSystem
    {
        public void Run(EcsSystems systems)
        {
            var timeServiceProvider = systems.GetShared<ITimeServiceProvider>();
            var timeService = timeServiceProvider.TimeService;
            timeService.Time = Time.time;
            timeService.UnscaledTime = Time.unscaledTime;
            timeService.DeltaTime = Time.deltaTime;
            timeService.UnscaledDeltaTime = Time.unscaledDeltaTime;
        }
    }
}