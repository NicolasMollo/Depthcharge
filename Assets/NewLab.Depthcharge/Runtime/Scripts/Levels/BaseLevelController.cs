using Depthcharge.Actors;
using Depthcharge.GameManagement;
using Depthcharge.UI;
using System;
using UnityEngine;

namespace Depthcharge.LevelManagement
{

    [DisallowMultipleComponent]
    public abstract class BaseLevelController : MonoBehaviour
    {

        protected GameSystemsRoot systemsRoot = null;
        public GameSystemsRoot SystemsRoot { get => systemsRoot; }
        protected BaseGameUIController UI = null;
        public BaseGameUIController UIController { get => UI; }
        protected GameLogic gameLogic = null;
        protected GameStateManager gameStateManager = null;
        protected GameUIContext UIContext = default;
        protected WinConditionContainer _winCondition = null;
        public WinConditionContainer WinCondition { get => _winCondition; }
        protected LevelConfiguration _configuration = null;
        public LevelConfiguration Configuration { get => _configuration; }
        protected LevelStats _stats = null;
        public LevelStats Stats { get => _stats; }

        [SerializeField]
        protected PlayerController player = null;
        public PlayerController Player { get => player; }

        public Action OnWin = null;

        private void Start()
        {
            systemsRoot = GameSystemsRoot.Instance;
            gameLogic = GameLogic.Instance;
            gameStateManager = GameStateManager.Instance;
            SetUp();
            AddListeners();
        }
        private void OnDestroy()
        {
            RemoveListeners();
            CleanUp();
        }

        protected virtual void SetUp()
        {
            _stats = new LevelStats();
            _configuration = gameLogic.GetLevelConfiguration();
            int randomIndex = UnityEngine.Random.Range(0, _configuration.WinConditions.Count);
            _winCondition = _configuration.WinConditions[randomIndex];
            player.SetUp();
            ConfigureUI(ref UI);
            SetGameUIContext(ref UIContext);
            //UIContext.player = player;
            //UIContext.levelController = this;
            UI.SetUp(UIContext);
            gameStateManager.SetStateOnPreGame(this);
        }

        protected abstract void ConfigureUI(ref BaseGameUIController UI);
        protected virtual void SetGameUIContext(ref GameUIContext context)
        {
            context.player = player;
            context.levelController = this;
        }

        protected virtual void CleanUp()
        {
            player.CleanUp();
        }

        protected virtual void AddListeners()
        {
            player.ShootModule.OnShoot += OnPlayerShoot;
            player.ShootModule.OnStartReload += OnPlayerStartReload;
            player.ShootModule.OnReloaded += OnPlayerReloaded;
            player.HealthModule.OnTakeDamage += OnPlayerTakeDamage;
            player.HealthModule.OnTakeHealth += OnPlayerTakeHealth;
        }
        protected virtual void RemoveListeners()
        {
            player.ShootModule.OnShoot -= OnPlayerShoot;
            player.ShootModule.OnStartReload -= OnPlayerStartReload;
            player.ShootModule.OnReloaded -= OnPlayerReloaded;
            player.HealthModule.OnTakeDamage -= OnPlayerTakeDamage;
            player.HealthModule.OnTakeHealth -= OnPlayerTakeHealth;
        }

        private void OnPlayerTakeDamage(float damage)
        {
            UI.UpdateHealthBar(player.HealthModule.HealthPercentage);
        }
        private void OnPlayerTakeHealth(float health)
        {
            UI.UpdateHealthBar(player.HealthModule.HealthPercentage);
        }
        private void OnPlayerShoot()
        {
            UI.AddAmmoTransparency();
        }
        private void OnPlayerStartReload(bool isReloading)
        {
            UI.DecreaseReloadBar(player.ShootModule.ReloadTime);
        }
        private void OnPlayerReloaded()
        {
            UI.RemoveAmmoTransparency();
            UI.ResetReloadBar();
        }

    }

}