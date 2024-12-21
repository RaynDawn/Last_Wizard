using UnityEngine;

public class SimpleGun : MonoBehaviour
{
    public Sprite bulletSprite;
    private GameObject bullet;

    private void Awake()
    {
        bullet = new GameObject("Bullet");
        bullet.tag = "Bullet";
        bullet.transform.parent = this.transform;
        bullet.transform.localScale = Vector3.one / 4;

        SpriteRenderer bulletSr = bullet.AddComponent<SpriteRenderer>();
        bulletSr.sprite = bulletSprite;
        Rigidbody2D bulletRb = bullet.AddComponent<Rigidbody2D>();
        bulletRb.gravityScale = 0;
        CircleCollider2D bulletCollider = bullet.AddComponent<CircleCollider2D>();
        bulletCollider.isTrigger = true;

        bullet.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Shoot(Vector2 dir)
    {
        GameObject bulletCpy = Instantiate(bullet, transform.position, transform.rotation);
        bulletCpy.SetActive(true);
        bulletCpy.GetComponent<Rigidbody2D>().AddForce(dir * 10f, ForceMode2D.Impulse);
        Destroy(bulletCpy, 5);
    }

}
