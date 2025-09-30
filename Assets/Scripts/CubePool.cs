using System.Collections.Generic;
using UnityEngine;

public class CubePool : ObjectPool<Cube>
{
    protected override void ResetObject(Cube cube)
    {
        cube.ResetCube();
    }
}