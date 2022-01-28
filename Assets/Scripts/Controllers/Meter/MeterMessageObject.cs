using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utility.MessageQueue;

namespace UI.Meter
{
    public class MeterMessageObject : MessageObject
    {
        [SerializeField]
        private int meter1Value;
        /// <summary>
        /// How much value needs to be added to meter 1
        /// </summary>
        public int Meter1Value
        {
            get
            {
                return meter1Value;
            }
        }

        [SerializeField]
        private int meter2Value;
        /// <summary>
        /// How much value needs to be added to meter 2
        /// </summary>
        public int Meter2Value
        {
            get
            {
                return meter2Value;
            }
        }

        /// <summary>
        /// Constructor sets both values with given inputs
        /// </summary>
        /// <param name="_meter1"></param>
        /// <param name="_meter2"></param>
        public MeterMessageObject(int _meter1, int _meter2)
        {
            meter1Value = _meter1;
            meter2Value = _meter2;
        }
    }
}
