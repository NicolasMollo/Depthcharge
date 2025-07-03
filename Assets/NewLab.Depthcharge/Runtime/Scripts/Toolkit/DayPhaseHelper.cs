using System;

namespace Depthcharge.Toolkit
{

    public class DayPhaseHelper
    {

        public enum DayPhaseType : byte
        {
            Morning,
            Afternoon,
            Evening,
            Night,
            Last
        }

        private SO_DayPhaseConfiguration dayPhaseConfig = null;

        public DayPhaseHelper(SO_DayPhaseConfiguration dayPhaseConfig)
        {
            this.dayPhaseConfig = dayPhaseConfig;
        }

        public DayPhaseType GetDayPhase()
        {

            DayPhaseType dayPhaseType = DayPhaseType.Last;

            if (DateTime.Now.Hour >= dayPhaseConfig.MorningStartHour && DateTime.Now.Hour < dayPhaseConfig.AfternoonStartHour)
                dayPhaseType = DayPhaseType.Morning;
            else if (DateTime.Now.Hour >= dayPhaseConfig.AfternoonStartHour && DateTime.Now.Hour < dayPhaseConfig.EveningStartHour)
                dayPhaseType = DayPhaseType.Afternoon;
            else if (DateTime.Now.Hour >= dayPhaseConfig.EveningStartHour && DateTime.Now.Hour < dayPhaseConfig.NightStartHour)
                dayPhaseType = DayPhaseType.Evening;
            else
                dayPhaseType = DayPhaseType.Night;

            return dayPhaseType;

        }

    }

}