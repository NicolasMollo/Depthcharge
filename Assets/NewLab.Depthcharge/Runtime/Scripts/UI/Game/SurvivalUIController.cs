using System;
using TMPro;
using UnityEngine;

namespace Depthcharge.UI
{

    public class SurvivalUIController : BaseGameUIController
    {

        [Header("TEXTS")]
        [SerializeField]
        private string elapsedTimeTextRoot = string.Empty;
        [SerializeField]
        private TextMeshProUGUI elapsedTimeText = null;

        public override void SetUp(GameUIContext context)
        {
            base.SetUp(context);
            SetScoreText(context.levelController.Stats.Score.ToString());
            SetEnemiesText(context.levelController.Stats.EnemiesDefeated.ToString());
        }

        public void SetElapsedTimeText(TimeSpan timeSpan)
        {
            elapsedTimeText.text = $"{elapsedTimeTextRoot} {timeSpan.ToString(@"hh\:mm\:ss")}";
        }

    }

}