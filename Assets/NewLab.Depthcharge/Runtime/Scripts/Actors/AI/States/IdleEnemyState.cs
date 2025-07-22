using System.Collections;
using UnityEngine;
using Depthcharge.Actors.Modules;

namespace Depthcharge.Actors.AI
{

    public class IdleEnemyState : BaseState
    {

        private ShootModule shootModule = null;
        private bool isShootModuleGot = false;

        [SerializeField]
        [Range(0.0f, 10.0f)]
        private float additionalTime = 0;

        public override void OnStateEnter()
        {
            Debug.Log($"I'm in {this.name}");
            if (!isShootModuleGot)
            {
                shootModule = fsm.Owner.GetComponentInChildren<ShootModule>();
                isShootModuleGot = true;
            }
            StartCoroutine(WaitAdditionalTime(!shootModule.IsReloading, additionalTime));
        }

        private IEnumerator WaitAdditionalTime(bool condition, float additionalDelay)
        {
            yield return new WaitUntil(() => condition);
            yield return new WaitForSeconds(additionalDelay);
            fsm.ChangeState(nextState);
        }

    }

}