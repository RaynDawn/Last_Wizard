using UnityEngine;
using QFramework;

namespace LastWizard
{
	public partial class Player : ViewController
	{
	    public float movementSpeed = 5;
		
		public static Player Default;

		[SerializeField] bool isPlayerControl = false;

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
				if (Collider2D.gameObject.tag != "Enemy") return;

				var hurtBox = Collider2D.GetComponent<HurtBox>();
				if (hurtBox)
				{
					if (hurtBox.Owner.CompareTag("Enemy"))
					{
						Global.Hp.Value--;
					}
				}
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

				/*Enemy e;
				var hit = Collider2D.gameObject.transform.parent.GetComponent<Enemy>();
				//var test = Collider2D.gameObject.transform.parent.TryGetComponent<Enemy>(out e);
				if (hit)
                {
					if (hit.gameObject.transform.parent.CompareTag("Enemy"))
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
                }*/
				
			
			}).UnRegisterWhenGameObjectDestroyed(gameObject);
		}

		void Update()
		 {
            if (isPlayerControl)
            {
				Movement();
            }
            

        }

		void Movement()
		{
            var horizontal = Input.GetAxis("Horizontal");
            var vertical = Input.GetAxis("Vertical");

            var direction = new Vector2(horizontal, vertical).normalized;

            SelfRigidbody2D.velocity = direction * movementSpeed;
        }
}
}
