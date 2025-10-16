using UnityEngine;
using Depthcharge.Toolkit;


namespace Depthcharge.Environment
{

    [DisallowMultipleComponent]
    public class EnvironmentRootController : MonoBehaviour
    {

        [Header("BACKGROUND")]
        [SerializeField]
        private BackgroundController seaBackgroundController = null;
        [SerializeField]
        private BackgroundController skyBackgroundController = null;

        [Header("SETTINGS")]
        [SerializeField]
        private bool enableTimeBasedColor = false;
        [SerializeField]
        private SO_DayPhaseConfiguration dayPhaseConfig = default;
        private DayPhaseHelper dayPhaseHelper = null;

        [Header("POSITIONS")]
        [SerializeField]
        private Transform _topSeaTarget = null;
        public Transform TopSeaTarget { get => _topSeaTarget; }
        [SerializeField]
        private Transform _bottomSeaTarget = null;
        public Transform BottomSeaTarget { get => _bottomSeaTarget; }

        private void Awake()
        {
            dayPhaseHelper = new DayPhaseHelper(dayPhaseConfig);
        }
        private void Start()
        {
            SetBackgroundColors();
        }

        private void SetBackgroundColors()
        {
            if (enableTimeBasedColor)
            {
                SetBackgroundColorBasedOnTime(seaBackgroundController);
                SetBackgroundColorBasedOnTime(skyBackgroundController);
            }
            else
            {
                seaBackgroundController.SetMorningColor();
                skyBackgroundController.SetMorningColor();
            }
        }

        private void SetBackgroundColorBasedOnTime(BackgroundController backgroundController)
        {
            switch (dayPhaseHelper.GetDayPhase())
            {
                case DayPhaseHelper.DayPhaseType.Morning:
                    backgroundController.SetMorningColor();
                    break;
                case DayPhaseHelper.DayPhaseType.Afternoon:
                    backgroundController.SetAfternoonColor();
                    break;
                case DayPhaseHelper.DayPhaseType.Evening:
                    backgroundController.SetEveningColor();
                    break;
                case DayPhaseHelper.DayPhaseType.Night:
                    backgroundController.SetNightColor();
                    break;
            }
        }

    }

}