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

        [SerializeField]
        private bool isOrigin;

        /// <summary>
        /// If the passed in inputs are meant to be an origin point or the current point
        /// </summary>
        public bool IsOrigin
        {
            get
            {
                return isOrigin;
            }
        }

        /// <summary>
        /// Constructs CameraMessageObject with given values
        /// </summary>
        /// <param name="_position"></param>
        /// <param name="_rotation"></param>
        /// <param name="_isOrigin"> If true camera will move there and set as new origin point </param>
        public CameraMessageObject(Vector3 _position, Quaternion _rotation, bool _isOrigin = false)
        {
            position = _position;
            rotation = _rotation;
            isOrigin = _isOrigin;

        }
    }
}
