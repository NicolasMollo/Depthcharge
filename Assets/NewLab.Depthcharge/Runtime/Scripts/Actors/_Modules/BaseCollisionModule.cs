using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace Depthcharge.Actors.Modules
{

    public abstract class BaseCollisionModule : BaseModule
    {

        protected SpriteRenderer spriteRenderer = null;

        [SerializeField]
        protected Rigidbody2D rb = null;
        [SerializeField]
        protected BoxCollider2D boxCollider = null;
        [SerializeField]
        protected List<BaseCollisionStrategy> collisionStrategies = null;
        [SerializeField]
        protected bool setComponentsAutomatically = false;

        public int LastCollisionLayer { protected set; get; } = -1;

        public override void SetUpModule()
        {
            if (!setComponentsAutomatically) return;

            string message = $"=== {this.name}.CollisionModule.SetUpModule() === This module requires a GameObject owner to function!";
            Assert.IsNotNull(owner, message);
            spriteRenderer = owner.GetComponentInChildren<SpriteRenderer>();
            message = $"=== {owner.name}.CollisionModule.SetUpModule() === spriteRenderer is null!";
            Assert.IsNotNull(spriteRenderer, message);
            message = $"=== {owner.name}.CollisionModule.SetUpModule() === Rigidbody is null!";
            Assert.IsNotNull(rb, message);
            rb.bodyType = RigidbodyType2D.Kinematic;
            rb.useFullKinematicContacts = true;
            message = $"=== {owner.name}.CollisionModule.SetUpModule() === BoxCollider is null!";
            Assert.IsNotNull(boxCollider, message);
            boxCollider.size = new Vector3(spriteRenderer.sprite.bounds.size.x, spriteRenderer.sprite.bounds.size.y, 0);
        }

        public override void EnableModule()
        {
            boxCollider.enabled = true;
        }
        public override void DisableModule()
        {
            boxCollider.enabled = false;
        }

    }

}