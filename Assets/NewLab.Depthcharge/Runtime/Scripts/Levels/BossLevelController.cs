using Depthcharge.Actors;
using Depthcharge.Events;
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
            _boss.gameObject.SetActive(false);
            bossUI = UI as BossUIController;
            if (bossUI == null)
            {
                Debug.LogError($"=== BossLevelController.InternalSetUp() === bossUI is not a BossUIController!");
                return;
            }
        }
        protected override void AddListeners()
        {
            base.AddListeners();
            GameEventBus.OnGameStart += OnGameStart;
        }
        protected override void RemoveListeners()
        {
            GameEventBus.OnGameStart -= OnGameStart;
            base.RemoveListeners();
        }
        private void OnGameStart()
        {
            _boss.gameObject.SetActive(true);
        }

        protected override void AddListenersToActors()
        {
            base.AddListenersToActors();
            _boss.HealthModule.OnTakeDamage += OnBossTakeDamage;
            _boss.HealthModule.OnDeath += OnBossDeath;
        }
        protected override void RemoveListenersFromActors()
        {
            base.RemoveListenersFromActors();
            _boss.HealthModule.OnTakeDamage -= OnBossTakeDamage;
            _boss.HealthModule.OnDeath -= OnBossDeath;
        }
        private void OnBossTakeDamage(float damage)
        {
            bossUI.UpdateBossHealthBar(_boss.HealthModule.HealthPercentage);
        }
        private void OnBossDeath()
        {
            _stats.IncreaseEnemiesDefeated();
            _stats.IncreaseScore(_boss.ScorePoints);
            OnWin?.Invoke();
        }

    }
}