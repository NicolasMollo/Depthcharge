namespace Depthcharge.Actors
{
    internal class PlayerBulletBehaviour : BaseBulletBehaviour
    {
        public override void OnBulletDeath(string endOfMapTag)
        {
            owner.Deactivation();
        }

    }
}