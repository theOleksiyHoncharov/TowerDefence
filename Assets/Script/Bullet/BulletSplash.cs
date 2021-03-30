using UnityEngine;

public class BulletSplash : Bullet 
{
    [SerializeField]
    ParticleSystem boom;

    const int RANGE_MIN = 1;
    const int RANGE_MAX = 20;

    [Range(RANGE_MIN, RANGE_MAX)]
    [SerializeField]
    private int range = RANGE_MIN;

    protected override void Hit()
    {
        if (boom != null)
        {
            boom.Play();
        }

        var enems = enemyController.GetEnemysInRange(enemyTransform.position, range);

        for (int i = 0; i < enems.Count; i++)
        {
            enems[i].SetDamage(damage);
        }
    }
}
