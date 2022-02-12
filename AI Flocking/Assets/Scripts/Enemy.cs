using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Member
{
    protected override Vector3 Combine()
    {
        return memConfig.wanderPriority * Wander();
    }
}
