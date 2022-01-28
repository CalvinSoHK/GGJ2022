using UnityEngine;
using Utility.MessageQueue;

namespace CameraManagement
{
    public class CameraMessageObject : MessageObject
    {
        [SerializeField]
        private Vector3 position;

        /// <summary>
        /// Position of the camera object
        /// </summary>
        public Vector3 Position
        {
            get
            {
                return position;
            }
        }

        [SerializeField]
        private Quaternion rotation;

        /// <summary>
        /// Rotation of the camera pos
        /// </summary>
        public Quaternion Rotation
        {
            get
            {
                return rotation;
            }
        }

        /// <summary>
        /// Constructs CameraMessageObject with given values
        /// </summary>
        /// <param name="_position"></param>
        /// <param name="_rotation"></param>
        public CameraMessageObject(Vector3 _position, Quaternion _rotation)
        {
            position = _position;
            rotation = _rotation;
        }
    }
}
