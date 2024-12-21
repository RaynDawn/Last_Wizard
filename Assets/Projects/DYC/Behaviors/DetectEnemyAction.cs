using System.Collections.Generic;
using UnityEngine;
using UniBT;

public class DetectEnemyAction : Action
{
    public float detectionRadius = 5f;      // Outer detection radius

    private GameObject[] enemies;       // all GO with 'Enemy' tag in range
    private GameObject nearestEnemy;        // Nearest enemy in range

    private Transform transform;
    private Hero hero;

    public override void Awake()
    {
        transform = gameObject.transform;
        hero = gameObject.GetComponent<Hero>();
    }
    protected override Status OnUpdate()
    {
        enemies = GetAllEnemiesAround();
        nearestEnemy = GetNearestEnemy(enemies);

        hero.enemies = enemies;
        hero.nearestEnemy = nearestEnemy;

        if (enemies.Length > 0)
        {
            return Status.Success;
        }
        else
        {
            return Status.Running;
        }
    }

    GameObject[] GetAllEnemiesAround()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, detectionRadius);
        List<GameObject> enemies = new List<GameObject>();
        foreach (Collider2D hit in hits)
        {
            if (hit.tag == "Enemy")
            {
                enemies.Add(hit.gameObject);
            }
        }
        return enemies.ToArray();
    }

    GameObject GetNearestEnemy(GameObject[] enemies)
    {
        if (enemies.Length == 0) return null;

        GameObject cloestEnemy = enemies[0];
        foreach (GameObject enemy in enemies)
        {
            float oldDis = Vector3.Distance(transform.position, cloestEnemy.transform.position);
            float newDis = Vector3.Distance(transform.position, enemy.transform.position);
            if (newDis < oldDis) cloestEnemy = enemy.gameObject;
        }

        return cloestEnemy;
    }
}
