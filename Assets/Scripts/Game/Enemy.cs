using UnityEngine;
using QFramework;

namespace LastWizard
{
	public partial class Enemy : ViewController
	{
		public float movementSpeed = 3;
		public float health = 3;
		void Start()
		{
			// Code Here
			Global.EnemyCount.Value++;
		}
        private void FixedUpdate()
        {
			if (Player.Default)
			{
				var direction = (Player.Default.transform.position - transform.position).normalized;
				
				SelfRigidbody2D.velocity = direction * movementSpeed;
			}
			else
            {
				SelfRigidbody2D.velocity = Vector2.zero;
            }
		}
        private void Update()
		 {
			if(health <= 0 )
            {
				this.DestroyGameObjGracefully();
				Global.EnemyCount.Value--;
				Global.GenerateDrop(gameObject);
			}
		 }
		 private bool IgnoreHurt = false;
		public void Hurt(float value)
        {
			if (IgnoreHurt) return;
            
				Sprite.color = Color.red;
			    AudioKit.PlaySound("hurt");
			ActionKit.Delay(0.3f, () =>
				{
					this.Sprite.color = Color.white;
					this.health -= Global.SampleAbilityDamage.Value;
					IgnoreHurt = false;
				}).Start(this);
			
			
		}
	}
}
