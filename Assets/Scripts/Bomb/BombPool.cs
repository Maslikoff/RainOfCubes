using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombPool : ObjectPool<Bomb>
{
    protected override void ResetObject(Bomb bomb)
    {
        bomb.ResetBomb();
    }
}
