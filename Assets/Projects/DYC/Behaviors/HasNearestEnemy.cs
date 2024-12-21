using UnityEngine;
using UniBT;
using System.Collections.Generic;

public class HasNearestEnemy : Conditional
{
    private Hero hero;
    protected override void OnAwake()
    {
        hero = gameObject.GetComponent<Hero>();
    }
    protected override bool IsUpdatable()
    {
        if (hero.nearestEnemy != null) return true;
        else return false;
    }
}

