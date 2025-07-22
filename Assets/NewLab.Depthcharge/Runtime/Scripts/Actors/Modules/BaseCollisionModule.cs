using System.Collections.Generic;
using UnityEngine;

namespace Depthcharge.Actors.Modules
{

    [RequireComponent(typeof(BoxCollider2D))]
    public class BaseCollisionModule : BaseModule
    {

        [SerializeField]
        protected Rigidbody2D rb = null;
        protected SpriteRenderer spriteRenderer = null;
        protected BoxCollider2D boxCollider = null;

        [SerializeField]
        protected bool setComponentsAutomatically = false;

        [SerializeField]
        protected List<BaseCollisionStrategy> collisionStrategies = null;

        public override void SetUpModule(GameObject owner = null)
        {

            if (!setComponentsAutomatically) return;

            if (owner == null)
            {
                Debug.LogError($"=== {this.name}.CollisionModule.SetUpModule() === This module requires a GameObject owner to function!");
                return;
            }

            spriteRenderer = owner.GetComponentInChildren<SpriteRenderer>();
            if (spriteRenderer == null)
            {
                Debug.LogError($"=== {this.name}.CollisionModule.SetUpModule() === spriteRenderer is null!");
                return;
            }
            rb = GetComponent<Rigidbody2D>();
            rb.bodyType = RigidbodyType2D.Kinematic;
            rb.useFullKinematicContacts = true;
            boxCollider = GetComponent<BoxCollider2D>();
            boxCollider.size = new Vector3(spriteRenderer.sprite.bounds.size.x, spriteRenderer.sprite.bounds.size.y, 0);

        }

    }

}