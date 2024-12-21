using UnityEngine;
using UniBT;

public class IsInChasingRange : Conditional
{
    public float thresholdRadius = 3;

    private Hero hero;
    private Transform transform;
    protected override void OnAwake()
    {
        hero = gameObject.GetComponent<Hero>();
        transform = gameObject.transform;
    }
    protected override bool IsUpdatable()
    {
        float distance = Vector2.Distance(hero.nearestEnemy.transform.position, transform.position);
        if (distance >= thresholdRadius) return true;
        else return false;
    }
}
