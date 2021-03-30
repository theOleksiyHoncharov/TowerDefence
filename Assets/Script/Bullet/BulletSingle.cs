using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSingle : Bullet 
{
    protected override void Hit()
    {
        enemy.SetDamage(damage);
    }
}
