
public class TowerTargetFastest : Tower //Башня стреляет по противнику, который дальше всего прошел
{
    protected override void Shoot()
    {
        var enemy = enemyController.GetСlosestToTarget(transform.position, FireDistance);
        if (enemy == null)
            return;

        var bullet = objectPool.GetBulletSingle(weaponPoint.position, weaponPoint.rotation);
        bullet.Damage = Damage;
        bullet.Move(enemy);
    }
}
