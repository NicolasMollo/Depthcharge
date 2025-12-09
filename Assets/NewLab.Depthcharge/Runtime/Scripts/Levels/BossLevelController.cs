using Depthcharge.Actors;
using Depthcharge.Actors.Modules;
using Depthcharge.Environment;
using Depthcharge.Events;
using Depthcharge.UI;
using System.Collections;
using UnityEngine;
using UnityEngine.Assertions;

namespace Depthcharge.LevelManagement
{
    public class BossLevelController : BaseLevelController
    {

        private BossUIController bossUI = null;
        private BaseEnemyController _boss = null;
        public BaseEnemyController Boss { get => _boss; }

        [SerializeField]
        private ShootModuleManager _cannons = null;
        public ShootModuleManager Cannons { get => _cannons;  }


        protected override void SetConfiguration(ref LevelConfiguration configuration)
        {
            configuration = gameLogic.GetLevelConfiguration();
        }
        protected override void ConfigureUI(ref BaseGameUIController UI)
        {
            UI = systemsRoot.UISystem.BossUIController;
        }

        protected override void InternalSetUp()
        {
            GameObject bossObj = Instantiate(_configuration.PrefabBoss, player.transform.parent);
            _boss = bossObj.GetComponent<BaseEnemyController>();
            string message = $"=== BossLevelController.InternalSetUp() === boss is not a BaseEnemyController!";
            Assert.IsNotNull(_boss, message);
            _boss.gameObject.SetActive(false);
            bossUI = UI as BossUIController;
            message = $"=== BossLevelController.InternalSetUp() === bossUI is not a BossUIController!";
            Assert.IsNotNull(bossUI, message);
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
            _audioSource.Play(_configuration.BossMusic);
        }

        protected override void AddListenersToActors()
        {
            base.AddListenersToActors();
            _boss.HealthModule.OnTakeDamage += OnBossTakeDamage;
            _boss.HealthModule.OnDeath += OnBossDeath;
            _boss.HealthModule.OnVulnerable += OnBossVulnerable;
            _boss.HealthModule.OnInvulnerable += OnBossInvulnerable;
            _boss.AnimationModule.SubscribeToOnAnimationEnd(AnimationController.AnimationType.Death, OnBossDeathAnimationEnd);
        }
        protected override void RemoveListenersFromActors()
        {
            _boss.AnimationModule.UnsubscribeFromOnAnimationEnd(AnimationController.AnimationType.Death, OnBossDeathAnimationEnd);
            _boss.HealthModule.OnInvulnerable -= OnBossInvulnerable;
            _boss.HealthModule.OnVulnerable -= OnBossVulnerable;
            _boss.HealthModule.OnDeath -= OnBossDeath;
            _boss.HealthModule.OnTakeDamage -= OnBossTakeDamage;
            base.RemoveListenersFromActors();
        }
        private void OnBossTakeDamage(float damage)
        {
            bossUI.UpdateBossHealthBar(_boss.HealthModule.HealthPercentage);
        }
        private void OnBossDeath()
        {
            _stats.IncreaseEnemiesDefeated();
            _stats.IncreaseScore(_boss.ScorePoints);
            player.InputModule.DisableModule();
            player.HealthModule.SetVulnerability(false);
            player.ShootModule.OnStartReload -= OnPlayerStartReload;
            UI.gameObject.SetActive(false);
        }
        private void OnBossVulnerable()
        {
            bossUI.UpdateBossHealthBar(_boss.HealthModule.HealthPercentage);
        }
        private void OnBossInvulnerable()
        {
            bossUI.SetBossHealthBarColor(Color.gray);
        }
        private void OnBossDeathAnimationEnd()
        {
            StartCoroutine(WaitUntilBossIsFadedOut());
        }
        private IEnumerator WaitUntilBossIsFadedOut()
        {
            yield return new WaitUntil(() => _boss.IsFadedIn == false);
            OnWin?.Invoke();
        }

    }
}