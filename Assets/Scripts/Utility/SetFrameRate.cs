using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utility
{
    public class SetFrameRate : MonoBehaviour
    {
        [SerializeField]
        private int targetFramerate;

        // Start is called before the first frame update
        void Start()
        {
            Application.targetFrameRate = targetFramerate;
        }
    }
}
