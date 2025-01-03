using UnityEngine;
using QFramework;

namespace LastWizard
{
	public partial class HP : ViewController
	{
		void Start()
		{
			// Code Here
		}
		private void OnTriggerEnter2D(Collider2D other)
		{
			if (other.GetComponent<CollectableArea>())
			{
				if(Global.Hp.Value < Global.MaxHp.Value)
                {
					Global.Hp.Value++;
					AudioKit.PlaySound("hp");
					this.DestroyGameObjGracefully();
				}
				else
                {
					AudioKit.PlaySound("hp");
					this.DestroyGameObjGracefully();
				}
			}
		}
	}
}
