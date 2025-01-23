using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using QFramework;
using System.Linq;

namespace LastWizard
{
	public partial class KnifeAbility : ViewController
	{
		private float mCurrentSeconds = 0;
		private float knifeSpeed = 8.0f;
		void Start()
		{
			// Code Here
		}
        private void Update()
        {
			mCurrentSeconds += Time.deltaTime;
			
			if(mCurrentSeconds >= 1)
            {
				mCurrentSeconds = 0;

				var enemies = FindObjectsByType<Enemy>(FindObjectsInactive.Exclude, FindObjectsSortMode.None);

				var enemy = enemies.OrderBy(enemy => (Player.Default.transform.position - enemy.transform.position).magnitude).FirstOrDefault();
				
					if (enemy) 
					{
					Knife.Instantiate().Position(this.Position()).Show().Self(self =>
					{
						var rigidbody2D = self.GetComponent<Rigidbody2D>();
						var direction = (enemy.Position() - Player.Default.Position()).normalized;
						rigidbody2D.velocity = direction * knifeSpeed;
						var hp = 1;
						var trans = self.gameObject.GetComponent<Transform>();
						trans.up = direction.normalized;
						self.OnTriggerEnter2DEvent(collider =>
                        {
							if (collider.gameObject.tag != "Enemy") return;
							var hurtBox = collider.GetComponent<HurtBox>();
							if (hurtBox)
                            {
								if (hurtBox.Owner.CompareTag("Enemy"))
								{
									
									hurtBox.Owner.GetComponent<Enemy>().Hurt(Global.SampleAbilityDamage.Value);

									hp--;

									if (hp <= 0)
									{
										self.DestroyGameObjGracefully();
									}
								}
							}
							
                        }).UnRegisterWhenGameObjectDestroyed(self);

						ActionKit.OnUpdate.Register(() =>
                        {
							if(Player.Default != null)
                            {
								if ((Player.Default.Position() - self.Position()).magnitude > 20)
								{
									self.DestroyGameObjGracefully();
								}
							}
							
                        }).UnRegisterWhenGameObjectDestroyed(self);
					});
						
					}
				
			}
		}
    }
}
