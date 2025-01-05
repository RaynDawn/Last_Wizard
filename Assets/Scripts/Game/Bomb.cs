using UnityEngine;
using QFramework;
using System.Collections.Generic;
using System.Collections;

namespace LastWizard
{
	public partial class Bomb : ViewController
	{
        /*private float mCurrentSeconds = 0;
		void Start()
		{
			HitBox.OnTriggerEnter2DEvent(Collider2D =>
			{
				var hitBox = Collider2D.GetComponent<Collider2D>();
				if (hitBox != null)
				{
					if (hitBox.gameObject.transform.parent.CompareTag("Enemy"))
					{
						var enemies = FindObjectsByType<Enemy>(FindObjectsInactive.Exclude, FindObjectsSortMode.None);
						foreach (Enemy enemy in enemies)
						{
							{
								var distance = (this.transform.position - enemy.transform.position).magnitude;

								if (distance <= 5) //ÉËº¦¾àÀë
								{
									enemy.Hurt(enemy.health);
								}
							}
						}
						AudioKit.PlaySound("bomb");
						this.DestroyGameObjGracefully();
					}
				}
			}).UnRegisterWhenGameObjectDestroyed(gameObject);
		}
        private void Update()
        {
			mCurrentSeconds += Time.deltaTime;

			if (mCurrentSeconds >= 3)
			{
				mCurrentSeconds = 0;
				var enemies = FindObjectsByType<Enemy>(FindObjectsInactive.Exclude, FindObjectsSortMode.None);
				foreach (Enemy enemy in enemies)
				{
						var distance = (this.transform.position - enemy.transform.position).magnitude;

						if (distance <= 5) //ÉËº¦¾àÀë
						{
							enemy.Hurt(enemy.health);
						}
				}
				AudioKit.PlaySound("bomb");
				this.DestroyGameObjGracefully();
			}
				
				
			
		}*/

        private float mCurrentSeconds = 0;

        private void Start()
        {
            HitBox.OnTriggerEnter2DEvent(Collider2D =>
            {
                var hitBox = Collider2D.GetComponent<Collider2D>();
                if (hitBox != null)
                {
                    if (hitBox.gameObject.transform.parent.CompareTag("Enemy"))
                    {
						mCurrentSeconds = 99;
                    }
                }
            }).UnRegisterWhenGameObjectDestroyed(gameObject);
        }

        private void Update()
        {
            mCurrentSeconds += Time.deltaTime;
            if (mCurrentSeconds >= 3)
			{
				Explode();
                AudioKit.PlaySound("bomb");
                this.DestroyGameObjGracefully();
            }
        }

		void Explode()
		{
            Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, 5);
            if (hits.Length > 0)
            {
                foreach (Collider2D hit in hits)
                {
                    if (hit.transform.parent.CompareTag("Enemy"))
                    {
                        hit.transform.parent.GetComponent<Enemy>().Hurt(9999);
						
                    }
                }
            }
		}
    }
}
