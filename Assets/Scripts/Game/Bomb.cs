using UnityEngine;
using QFramework;

namespace LastWizard
{
	public partial class Bomb : ViewController
	{
		private float mCurrentSeconds = 0;
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
				
				
			
		}
	}
}
