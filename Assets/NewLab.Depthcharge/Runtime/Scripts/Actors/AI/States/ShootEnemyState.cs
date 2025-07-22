using UnityEngine;
using Depthcharge.Actors.Modules;

namespace Depthcharge.Actors.AI
{

    public class ShootEnemyState : BaseState
    {

        private ShootModule shootModule = null;
        private bool isShootModuleGot = false;

        public override void OnStateEnter()
        {
            Debug.Log($"I'm in {this.name}");
            if (!isShootModuleGot)
            {
                shootModule = fsm.Owner.GetComponentInChildren<ShootModule>();
                isShootModuleGot = true;
            }
            shootModule.Shoot();
            fsm.ChangeState(nextState);
        }

    }

}