using UnityEngine;

namespace Depthcharge.UI
{

    [DisallowMultipleComponent]
    public class UISystem : MonoBehaviour
    {

        [SerializeField]
        private StartUIController _startUI = null;
        public StartUIController StartUI { get => _startUI; }

        [SerializeField]
        private CampaignUIController _campaignUI = null;
        public CampaignUIController CampaignUI { get => _campaignUI; }

        [SerializeField]
        private SurvivalUIController _survivalUI = null;
        public SurvivalUIController SurvivalUI { get => _survivalUI; }

        public void SetUp()
        {

        }
        public void SetStartUIActiveness(bool activeness)
        {
            _startUI.gameObject.SetActive(activeness);
        }

        public void SetCampaignUIActiveness(bool activeness)
        {
            _campaignUI.gameObject.SetActive(activeness);
        }

        public void SetSurvivalUIActiveness(bool activeness)
        {
            _survivalUI.gameObject.SetActive(activeness);
        }

    }

}