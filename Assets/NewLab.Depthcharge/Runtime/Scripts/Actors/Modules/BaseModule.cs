using UnityEngine;

namespace Depthcharge.Actors.Modules
{

    public abstract class BaseModule : MonoBehaviour
    {

        public virtual void SetUpModule(GameObject owner = null) { }
        public virtual void UpdateModule() { }
        public virtual void CleanUpModule(GameObject owner = null) { }

    }

}