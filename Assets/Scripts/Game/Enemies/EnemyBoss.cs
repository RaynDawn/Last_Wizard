using UnityEngine;
using QFramework;

namespace LastWizard
{
	public partial class EnemyBoss : ViewController,IEnemy
	{
		public float movementSpeed = 5;
		public float dashSpeed = 20;
		public int health = 30;
		public float dashDistance = 5;
       
      
      


        public enum States
		{
			Following,
			Warning,
			Dashing,
			Waiting,
		}

		public FSM<States> FSM = new FSM<States>();
		void Start()
		{
			// Code Here
			UIKit.OpenPanel<EnemyBossHealthBarPanel>(new EnemyBossHealthBarPanelData());
			Global.EnemyBossHealth.Value = health;
            Global.EnemyCount.Value++;
			FSM.State(States.Following).OnFixedUpdate(() =>
            {

				if (Player.Default)
				{
					var direction = (Player.Default.transform.position - transform.position).normalized;

					SelfRigidbody2D.velocity = direction * movementSpeed;

					if((Player.Default.transform.Position() - transform.Position()).magnitude <= dashDistance)
                    {
						FSM.ChangeState(States.Warning);
                    }
				}
				else
				{
					SelfRigidbody2D.velocity = Vector2.zero;
				}
			});
			FSM.State(States.Warning).OnEnter(() =>
            {
				SelfRigidbody2D.velocity = Vector2.zero;
            }).OnUpdate(() =>
            {
				//£¨»æÖÆ¹¥»÷Â·¾¶£©
				if(FSM.SecondsOfCurrentState >= 5)
                {
					FSM.ChangeState(States.Dashing);
                }
            });
			var dashStartPos = Vector3.zero;
			var dashDistanceToPlayer = 0f;
			FSM.State(States.Dashing).OnEnter(() =>
            {
				var direction = (Player.Default.transform.Position() - transform.Position()).normalized;
				dashStartPos = transform.Position();
				SelfRigidbody2D.velocity = direction * dashSpeed;
				dashDistanceToPlayer = (Player.Default.transform.Position() - transform.Position()).magnitude;
			}).OnUpdate(() =>
            {
				var distance = (transform.Position() - dashStartPos).magnitude;
				if(distance >= dashDistanceToPlayer + 10)
                {
					FSM.ChangeState(States.Waiting);
				}
            });
            FSM.State(States.Waiting).OnEnter(() =>
            {
				SelfRigidbody2D.velocity = Vector2.zero;
            }).OnUpdate(() =>
            {
				if(FSM.SecondsOfCurrentState >= 1)
                {
					FSM.ChangeState(States.Following);
                }
            });
			//FSM.State(States.Attaking);
			FSM.StartState(States.Following);
		}

		private void OnDestroy()
		{
			Global.EnemyCount.Value--;
       
			UIKit.ClosePanel<EnemyBossHealthBarPanel>();
        }

		

		private void FixedUpdate()
		{
			FSM.FixedUpdate();
		}
		private void Update()
		{
			FSM.Update();
			if (health <= 0)
			{
				this.DestroyGameObjGracefully();
                if (Player.Default)
                {
                    Global.Anger.Value++;
                }
                Global.GenerateDrop(gameObject);
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
                Global.EnemyBossHealth.Value -= (int)value;
				IgnoreHurt = false;
			}).Start(this);


		}
	}
}
