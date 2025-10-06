using Depthcharge.Actors.Modules;

namespace Depthcharge.Actors.AI
{

    public class ShootEnemyState : BaseState
    {

        private ShootModule shootModule = null;

        public override void SetUp()
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