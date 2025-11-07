using System;
using UnityEngine;

namespace Depthcharge.Actors.Modules
{

    public class HealthModule : BaseModule
    {

        [SerializeField]
        [Range(1.0f, 1000.0f)]
        private float maxHealth = 1.0f;

        private float _health = 0.0f;
        private bool isVulnerable = false;
        private float Health
        {
            get => _health < 0 ? _health = 0 : _health > maxHealth ? maxHealth : _health;
            set => _health = value;
        }
        public float HealthPercentage
        {
            get => Health / maxHealth;
        }

        public Action<float> OnTakeDamage = null;
        public Action<float> OnTakeHealth = null;
        public Action OnDeath = null;
        public Action OnVulnerable = null;
        public Action OnInvulnerable = null;
        public Action OnReachHalfHealth = null;
        public Action OnReachAQuarterHealth = null;

        private void Awake()
        {
            Health = maxHealth;
            isVulnerable = true;
            IsModuleSetUp = true;
        }

        public void TakeMaxDamage()
        {
            if (!isVulnerable)
            {
                return;
            }
            Health -= maxHealth;
            OnTakeDamage?.Invoke(Health);
            OnDeath?.Invoke();
        }
        public void TakeDamage(float damage)
        {
            if (!isVulnerable)
            {
                return;
            }
            Health -= damage;
            OnTakeDamage?.Invoke(Health);
            CheckHealthState();
            if (Health == 0)
            {
                OnDeath?.Invoke();
            }
        }
        public void TakeHealth(float health)
        {
            Health += health;
            OnTakeHealth?.Invoke(Health);
            CheckHealthState();
        }
        public void SetMaxHealth(float maxHealth)
        {
            this.maxHealth = maxHealth;
        }
        public void ResetHealth()
        {
            Health = maxHealth;
        }

        public void SetVulnerability(bool isVulnerable)
        {
            this.isVulnerable = isVulnerable;
            if (this.isVulnerable)
            {
                OnVulnerable?.Invoke();
            }
            else
            {
                OnInvulnerable?.Invoke();
            }
        }

        private void CheckHealthState()
        {
            const float HALF_HEALTH = 0.5f;
            const float QUARTER_HEALTH = 0.25f;

            if (HealthPercentage <= 0)
            {
                return;
            }
            else if (HealthPercentage <= QUARTER_HEALTH)
            {
                OnReachAQuarterHealth?.Invoke();
            }
            else if (HealthPercentage <= HALF_HEALTH)
            {
                OnReachHalfHealth?.Invoke();
            }
        }

    }

}