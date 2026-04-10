using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySingleShot : EnemyBaseShot
{
    public override void Fire()
    {
        Vector2 dir = GetDirection();
        SpawnBullet(dir);
    }
}