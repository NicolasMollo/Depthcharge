namespace Depthcharge.Actors.AI
{
    public class PlayerBulletDeathState : BaseBulletDeathState
    {
        public override void OnStateEnter()
        {
            bullet.Deactivation();
        }
    }

}