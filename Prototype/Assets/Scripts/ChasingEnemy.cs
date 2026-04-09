using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChasingEnemy : Enemy
{
    void FixedUpdate()
    {
        if (target == null) return;

        Vector2 dir = (target.position - transform.position).normalized;
        Move(dir);
    }
}
