using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utility.MessageQueue;

namespace ChoiceManagement
{
    public class ChoiceMessageObject : MessageObject
    {
        [SerializeField]
        private CharacterEnum chosenCharacter;

        /// <summary>
        /// Character that was chosen (maybe use to change prefab?)
        /// </summary>
        public CharacterEnum ChosenCharacter
        {
            get
            {
                return chosenCharacter;
            }
        }

        [SerializeField]
        private int originalCount;

        /// <summary>
        /// The original count that we start with
        /// </summary>
        public int OriginalCount
        {
            get
            {
                return originalCount;
            }
        }

        [SerializeField]
        private int targetCount;

        /// <summary>
        /// Target count we want to end with
        /// </summary>
        public int TargetCount
        {
            get
            {
                return targetCount;
            }
        }

        public ChoiceMessageObject(CharacterEnum _chosenCharacter, int _originalCount, int _targetCount)
        {
            chosenCharacter = _chosenCharacter;
            originalCount = _originalCount;
            targetCount = _targetCount;
        }
    }

    public enum CharacterEnum
    {
        None = 0,
        Character1 = 1,
        Character2 =2
    }
}
