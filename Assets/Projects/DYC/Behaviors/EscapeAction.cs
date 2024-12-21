using UniBT;
using UnityEngine;

public class EscapeAction : Action
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
        GetMoveDirection();
        return Status.Success;
    }

    void GetMoveDirection()
    {
        Vector2 finalDir = Vector2.zero;
        foreach (var enemy in hero.enemies)
        {
            finalDir += (Vector2)(transform.position - enemy.transform.position);
        }
        transform.position += (Vector3)finalDir.normalized * Time.deltaTime * moveSpeed;
    }
}
