using UnityEngine;
using QFramework;

namespace LastWizard
{
	public partial class Player : ViewController
	{
	    public float movementSpeed = 5;
		
		public static Player Default;

        private void Awake()
        {
            Player.Default = this;
        }

        private void OnDestroy()
        {
            Default = null;
        }
        void Start()
		{
			// Code Here
			
			HurtBox.OnTriggerEnter2DEvent(Collider2D => 
			{
				var hitBox = Collider2D.GetComponent<Collider2D>();
				if (hitBox != null)
                {
					if (hitBox.gameObject.transform.parent.CompareTag("Enemy"))
                    {
						Global.Hp.Value--;
						
						if (Global.Hp.Value <= 0)
                        {
							AudioKit.PlaySound("die");
							this.DestroyGameObjGracefully();
							UIKit.OpenPanel<GameOverPanel>();
						}
                        else
                        {
							AudioKit.PlaySound("pain");
						}
					}
                }
				
			
			}).UnRegisterWhenGameObjectDestroyed(gameObject);
		}

		void Update()
		 {
            var horizontal = Input.GetAxis("Horizontal");
            var vertical = Input.GetAxis("Vertical");

            var direction = new Vector2(horizontal, vertical).normalized;

            SelfRigidbody2D.velocity = direction * movementSpeed;

        }
}
}
