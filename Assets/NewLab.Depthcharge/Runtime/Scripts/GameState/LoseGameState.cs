using Depthcharge.Actors.AI;

namespace Depthcharge.GameManagement.AI
{
    public class LoseGameState : BaseState
    {

        private GameStateManager stateManager = null;
        private bool isOwnerGot = false;

        public override void OnStateEnter()
        {
            if (!isOwnerGot)
            {
                stateManager = fsm.Owner.GetComponent<GameStateManager>();
                isOwnerGot = !isOwnerGot;
            }
        }

    }
}