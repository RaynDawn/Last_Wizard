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
				this.DestroyGameObjGracefully();

				UIKit.OpenPanel<GameOverPanel>();
			
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
