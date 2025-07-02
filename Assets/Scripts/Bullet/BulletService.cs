namespace ForgottonChambers.Bullets
{
    public class BulletService
    {
        private BulletPool _playerBulletPool;


        public BulletService(BulletView defaultBulletPrefab, BulletScriptableObject defaultBulletData, int defaultPoolSize)
        {
            _playerBulletPool = new BulletPool(defaultBulletPrefab, defaultBulletData, defaultPoolSize);
        }

        public BulletController GetBullet()
        {
            return _playerBulletPool.GetBullet();
        }
    }
}