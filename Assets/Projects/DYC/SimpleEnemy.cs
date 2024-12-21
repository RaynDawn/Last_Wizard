using UnityEngine;

public class SimpleEnemy : MonoBehaviour
{
    private GameObject hero;
    private int health = 3;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        hero = (GameObject.FindWithTag("Hero") != null) ? GameObject.FindWithTag("Hero") : null;
    }

    // Update is called once per frame
    void Update()
    {
        if (hero != null)
        {
            transform.Translate((hero.transform.position - transform.position).normalized * Time.deltaTime);
        }
    }

    void TakeDamage(int amount)
    {
        health -= amount;
        if (health < 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Bullet")
        {
            TakeDamage(1);
            Destroy(collision.gameObject);
        }
    }
}
