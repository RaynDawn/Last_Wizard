using UnityEngine;
using UniBT;
using System.Collections;

public class HeroAttackAction : Action
{
    protected override Status OnUpdate()
    {
        return Status.Success;
    }
}
