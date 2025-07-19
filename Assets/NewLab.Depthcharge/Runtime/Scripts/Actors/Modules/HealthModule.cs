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
        private float Health
        {
            get => _health < 0 ? _health = 0 : _health > maxHealth ? maxHealth : _health;
            set => _health = value;
        }

        public Action<float> OnTakeDamage = null;
        public Action<float> OnTakeHealth = null;
        public Action<float> OnDeath = null;

        #region API

        public override void SetUpModule(GameObject owner = null)
        {

            Health = maxHealth;

        }

        public void TakeDamage(float damage)
        {

            Health -= damage;
            OnTakeDamage?.Invoke(Health);
            if (Health == 0)
            {
                OnDeath?.Invoke(Health);
            }

        }

        public void TakeHealth(float health)
        {

            Health += health;
            OnTakeHealth?.Invoke(Health);

        }

        #endregion

    }

}