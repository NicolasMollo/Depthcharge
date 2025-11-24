using UnityEngine;

namespace Depthcharge.Actors.Modules
{
    public abstract class BaseAnimationStrategy : ScriptableObject
    {

        public abstract void Animate(GameObject owner = null, Animator animator = null, AnimationController animation = null);

    }
}