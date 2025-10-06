using UnityEngine;

namespace Depthcharge.Toolkit
{

    [CreateAssetMenu(menuName = "Scriptable Objects/Toolkit/SO_DayPhaseConfiguration")]
    public class SO_DayPhaseConfiguration : ScriptableObject
    {

        [SerializeField] private float _morningStartHour;
        public float MorningStartHour { get => _morningStartHour; }

        [SerializeField] private float _afternoonStartHour;
        public float AfternoonStartHour { get => _afternoonStartHour; }

        [SerializeField] private float _eveningStartHour;
        public float EveningStartHour { get => _eveningStartHour; }

        [SerializeField] private float _nightStartHour;
        public float NightStartHour { get => _nightStartHour; }

    }

}