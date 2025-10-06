using UnityEngine;

namespace Depthcharge.Actors.Modules
{

    public class TransformMovementAdapter : BaseMovementAdapter
    {

        [SerializeField]
        private Transform targetTransform = null;

        public override Vector2 GetPosition()
        {
            return targetTransform.position;
        }

        public override void MoveTo(Vector2 position)
        {
            targetTransform.position = position;
        }

        public override void Translate(Vector2 translation)
        {
            targetTransform.Translate(translation);
        }

    }

}