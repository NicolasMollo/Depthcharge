using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace Depthcharge.Actors.Modules
{

    public abstract class BaseCollisionModule : BaseModule
    {

        [SerializeField]
        protected Rigidbody2D rb = null;
        [SerializeField]
        protected BoxCollider2D boxCollider = null;
        [SerializeField]
        protected List<BaseCollisionEnterStrategy> collisionStrategies = null;
        [SerializeField]
        protected List<BaseCollisionStayStrategy> stayStrategies = null;
        [SerializeField]
        protected List<BaseCollisionExitStrategy> exitStrategies = null;

        [SerializeField]
        protected bool setComponentsAutomatically = false;

        public Vector2 ColliderSize { get => boxCollider.size; }
        public int LastCollisionLayer { protected set; get; } = -1;
        public bool IsEnable { get; private set; } = true;

        public override void SetUpModule()
        {
            if (!setComponentsAutomatically) return;

            string message = $"=== {this.name}.CollisionModule.SetUpModule() === This module requires a GameObject owner to function!";
            Assert.IsNotNull(owner, message);
            message = $"=== {owner.name}.CollisionModule.SetUpModule() === Rigidbody is null!";
            Assert.IsNotNull(rb, message);
            message = $"=== {owner.name}.CollisionModule.SetUpModule() === BoxCollider is null!";
            Assert.IsNotNull(boxCollider, message);
            SetModuleAutomatically();
        }
        private void SetModuleAutomatically()
        {
            rb.bodyType = RigidbodyType2D.Kinematic;
            rb.useFullKinematicContacts = true;
            boxCollider.size = new Vector3(owner.SpriteRenderer.sprite.bounds.size.x, owner.SpriteRenderer.sprite.bounds.size.y, 0);
        }

        public override void EnableModule()
        {
            boxCollider.enabled = true;
            IsEnable = boxCollider.enabled;
        }
        public override void DisableModule()
        {
            boxCollider.enabled = false;
            IsEnable = boxCollider.enabled;
        }

    }

}