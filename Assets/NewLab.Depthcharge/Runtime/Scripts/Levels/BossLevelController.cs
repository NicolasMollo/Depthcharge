using Depthcharge.Actors;
using Depthcharge.UI;
using UnityEngine;

namespace Depthcharge.LevelManagement
{
    public class BossLevelController : BaseLevelController
    {

        private BossUIController bossUI = null;
        private BaseEnemyController _boss = null;
        public BaseEnemyController Boss { get => _boss; }

        protected override void ConfigureUI(ref BaseGameUIController UI)
        {
            UI = systemsRoot.UISystem.BossUIController;
        }
        protected override void InternalSetUp()
        {
            GameObject bossObj = Instantiate(_configuration.PrefabBoss, player.transform.parent);
            _boss = bossObj.GetComponent<BaseEnemyController>();
            if (_boss == null)
            {
                Debug.LogError($"=== BossLevelController.InternalSetUp() === boss is not a BaseEnemyController!");
                return;
            }
            bossUI = UI as BossUIController;
            if (bossUI == null)
            {
                Debug.LogError($"=== BossLevelController.InternalSetUp() === bossUI is not a BossUIController!");
                return;
            }
        }
        protected override void AddListenersToActors()
        {
            base.AddListenersToActors();
            _boss.HealthModule.OnTakeDamage += OnBossTakeDamage;
        }
        protected override void RemoveListenersFromActors()
        {
            base.RemoveListenersFromActors();
            _boss.HealthModule.OnTakeDamage -= OnBossTakeDamage;
        }

        private void OnBossTakeDamage(float damage)
        {
            bossUI.UpdateBossHealthBar(_boss.HealthModule.HealthPercentage);
        }

    }
}