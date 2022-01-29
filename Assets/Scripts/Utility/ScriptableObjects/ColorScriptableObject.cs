using UnityEngine;

namespace Utility
{
    [CreateAssetMenu(fileName = "ColorObject", menuName = "ScriptableObjects/ColorObject", order = 1)]
    public class ColorScriptableObject : ScriptableObject
    {
        [SerializeField]
        private Color savedColor;

        public Color SavedColor
        {
            get
            {
                return savedColor;
            }
        }
    }
}
