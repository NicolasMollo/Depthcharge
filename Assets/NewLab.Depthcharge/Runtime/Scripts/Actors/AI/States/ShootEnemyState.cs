using Depthcharge.Actors.Modules;

namespace Depthcharge.Actors.AI
{

    public class ShootEnemyState : BaseState
    {

        private ShootModule shootModule = null;

        private void Awake()
        {
            shootModule = fsm.Owner.GetComponentInChildren<ShootModule>();
        }

        public override void OnStateEnter()
        {
            shootModule.Shoot();
            fsm.GoToTheNextState();
        }

    }

}