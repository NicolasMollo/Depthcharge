using UnityEngine;

namespace Depthcharge.Actors.Modules
{
    public abstract class BaseAnimationStrategy : ScriptableObject
    {
        public abstract void Animate(Actor owner, Animator animator = null, AnimationController animation = null);
    }
}