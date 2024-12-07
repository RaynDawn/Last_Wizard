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
		}

    private void Update()
    {
			if(Player.Default)
            {
				// var player = FindObjectOfType<Player>();
				var direction = (Player.Default.transform.position - transform.position).normalized;
				transform.Translate(direction * Time.deltaTime * movementSpeed);
			}
			
			if(health <= 0 )
            {
				this.DestroyGameObjGracefully();
				Global.Exp.Value++;//销毁时加经验值
				 // UIKit.OpenPanel<GamePassPanel>();

            }
    }
}
}
