using UnityEngine;
using QFramework;

namespace LastWizard
{
	public partial class Enemy : ViewController,IEnemy
	{
		public float movementSpeed = 3;
		public float health = 3;
		public Color dissolveColor = Color.white;

		//public RectTransform healthBar;
        void Start()
		{
			// Code Here
			Global.EnemyCount.Value++;

			
		}

        private void OnDestroy()
        {
			Global.EnemyCount.Value--;
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
				FXController.Play(Sprite, dissolveColor);
                Global.GenerateDrop(gameObject);
				if(Player.Default)
                {
                    Global.Anger.Value++;
                }
                
            }
		 }
		 private bool IgnoreHurt = false;



        public void Hurt(float value)
        {
			if (IgnoreHurt) return;
			TextController.PlayFloatingText(transform.position, value.ToString());
			Sprite.color = Color.red;
			AudioKit.PlaySound("hurt");
			ActionKit.Delay(0.3f, () =>
				{
					this.Sprite.color = Color.white;
					this.health -= value;
					IgnoreHurt = false;

					//UpdateHealthBar();
				}).Start(this);
			
			
		}

      /*  private void UpdateHealthBar()
        {
            if (healthBar != null)
            {
                float healthPercentage = health / 3.0f; 
                healthBar.sizeDelta = new Vector3(healthPercentage, 1, 1);

            }
        }*/
    }
}
