using Client.Door;
using Client.Player;
using UnityEngine;

namespace Client
{
    using Server;

    public class EcsGameSharedScript : MonoBehaviour, ITimeServiceProvider, IEntityMonoBehaviourServiceProvider
    {
        public PlayerScript Player;
        public DoorsContainerScript DoorsContainer;
        private TimeService _timeService = new TimeService();
        private EntityMonoBehavioursService _entityMonoBehavioursService = new EntityMonoBehavioursService();

        public TimeService TimeService => _timeService;

        public EntityMonoBehavioursService EntityMonoBehavioursService => _entityMonoBehavioursService;
    }
}