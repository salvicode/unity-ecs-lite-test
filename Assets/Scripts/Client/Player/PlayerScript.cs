using UnityEngine;

namespace Client.Player
{
    public class PlayerScript : MonoBehaviour
    {

        public Vector3 Position => transform.position;
        public Quaternion Rotation => transform.rotation;
        public void SetPosition(Vector3 position)
        {
            transform.position = position;
        }

        public void SetRotation(Quaternion rotation)
        {
            transform.rotation = rotation;
        }
    }
}