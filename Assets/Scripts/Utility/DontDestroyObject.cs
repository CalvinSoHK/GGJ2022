using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utility
{
    /// <summary>
    /// Marks object to not be destroyed
    /// </summary>
    public class DontDestroyObject : MonoBehaviour
    {
        private void Awake()
        {
            GameObject.DontDestroyOnLoad(this.gameObject);
        }
    }
}
