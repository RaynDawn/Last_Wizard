using UnityEngine;
using UniBT;

public class ChasingAction : Action
{
    [SerializeField] float moveSpeed = 1f;

    private Transform transform;
    private Hero hero;

    public override void Awake()
    {
        transform = gameObject.transform;
        hero = gameObject.GetComponent<Hero>();
    }

    protected override Status OnUpdate()
    {
        MoveTowardNearestEnemy();
        return Status.Success;
    }

    void MoveTowardNearestEnemy()
    {
        Vector2 dir = (hero.nearestEnemy.transform.position - transform.position).normalized;
        transform.position += (Vector3)dir.normalized * Time.deltaTime * moveSpeed;
    }
}
