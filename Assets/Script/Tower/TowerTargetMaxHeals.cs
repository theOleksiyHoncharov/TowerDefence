
public class TowerTargetMaxHeals : Tower //Башня стреляет по противнику с максимальынм количеством здоровья
{
    protected override void Shoot()
    {
        var enemy = enemyController.GetEnemyMaxHP(transform.position, FireDistance);
        if (enemy == null) 
            return;

        var bullet = objectPool.GetBulletMulty(weaponPoint.position, weaponPoint.rotation);
        bullet.Damage = Damage;
        bullet.Move(enemy);
    }
}
