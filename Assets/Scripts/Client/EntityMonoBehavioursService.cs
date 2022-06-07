using System.Collections.Generic;
using UnityEngine;

namespace Client
{
    public class EntityMonoBehavioursService
    {
        private Dictionary<int, MonoBehaviour> _ecsEntityScriptsDictionary = new Dictionary<int, MonoBehaviour>();

        public void ConnectEntityToMonoBehavior(int entity, MonoBehaviour monoBehaviour)
        {
            _ecsEntityScriptsDictionary.Add(entity, monoBehaviour);
        }

        public void DisconnectEntity(int entity)
        {
            _ecsEntityScriptsDictionary.Remove(entity);
        }
        
        public T GetMonoBehaviourForEntity<T>(int entity) where T : MonoBehaviour
        {
            if (_ecsEntityScriptsDictionary.TryGetValue(entity, out var monoBehaviour))
            {
                return monoBehaviour as T;
            }

            return null;
        }
    }
}