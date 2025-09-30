using Depthcharge.UI.EndGame;
using UnityEngine;

namespace Depthcharge.UI
{

    [DisallowMultipleComponent]
    public class UISystem : MonoBehaviour
    {

        [SerializeField]
        private UI_StartController _startUI = null;
        public UI_StartController StartUI { get => _startUI; }

        private BaseGameUIController _currentGameUI = null;
        public BaseGameUIController CurrentGameUI { get => _currentGameUI; }
        [SerializeField]
        private CampaignUIController _campaignUI = null;
        public CampaignUIController CampaignUI { get => _campaignUI; }

        [SerializeField]
        private SurvivalUIController _survivalUI = null;
        public SurvivalUIController SurvivalUI { get => _survivalUI; }

        [SerializeField]
        private UI_EndGameController _winUI = null;
        public UI_EndGameController WinUI { get => _winUI; }

        [SerializeField]
        private UI_EndGameController _loseUI = null;
        public UI_EndGameController LoseUI { get => _loseUI; }



        public void SetUp()
        {
            bool startUIActiveness = true;
            SetStartUIActiveness(startUIActiveness);
            SetCampaignUIActiveness(!startUIActiveness);
            SetSurvivalUIActiveness(!startUIActiveness);
            SetWinUIActiveness(!startUIActiveness);
            SetLoseUIActiveness(!startUIActiveness);
        }

        public void SetStartUIActiveness(bool activeness)
        {
            _startUI.gameObject.SetActive(activeness);
        }

        public void SetCurrentGameUI(BaseGameUIController UI)
        {
            _currentGameUI = UI;
        }

        public void SetCampaignUIActiveness(bool activeness)
        {
            _campaignUI.gameObject.SetActive(activeness);
            if (activeness)
                _currentGameUI = _campaignUI;
        }

        public void SetSurvivalUIActiveness(bool activeness)
        {
            _survivalUI.gameObject.SetActive(activeness);
            if (activeness)
                _currentGameUI = _survivalUI;
        }

        public void SetWinUIActiveness(bool activeness)
        {
            _winUI.gameObject.SetActive(activeness);
        }

        public void SetLoseUIActiveness(bool activeness)
        {
            _loseUI.gameObject.SetActive(activeness);
        }

    }

}