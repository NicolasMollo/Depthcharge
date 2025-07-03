using UnityEngine;

namespace Depthcharge.Environment
{

    [CreateAssetMenu(menuName = "Scriptable Objects/Environment/SO_BackgroundColorConfiguration")]
    public class SO_BackgroundColorConfiguration : ScriptableObject
    {

        [SerializeField] private Color _morningColor = Color.white;
        public Color MorningColor { get => _morningColor; }

        [SerializeField] private Color _afternoonColor = Color.white;
        public Color AfternoonColor { get => _afternoonColor; }

        [SerializeField] private Color _eveningColor = Color.white;
        public Color EveningColor { get => _eveningColor; }

        [SerializeField] private Color _nightColor = Color.white;
        public Color NightColor { get => _nightColor; }

    }

}