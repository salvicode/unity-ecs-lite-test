using UnityEngine;

namespace Client.Door
{
    public class DoorScript : MonoBehaviour
    {
        public float Speed = 0.1f;
        public GameObject DoorButton;
        public GameObject DoorObject;
        public Transform ClosePositionTransform;
        public Transform OpenPositionTransform;

        public float DoorButtonRadius => DoorButton.GetComponent<SphereCollider>().radius;
        public Vector3 DoorButtonPosition => DoorButton.transform.position;
        public Vector3 DoorPosition => DoorObject.transform.position;
        public Vector3 ClosePosition => ClosePositionTransform.position;
        public Vector3 OpenPosition => OpenPositionTransform.position;
        
        public void SetDoorPosition(Vector3 position)
        {
            DoorObject.transform.position = position;
        }
    }
}