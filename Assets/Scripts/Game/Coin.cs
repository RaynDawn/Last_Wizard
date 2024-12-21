using UnityEngine;
using QFramework;

namespace LastWizard
{
	public partial class Coin : ViewController
	{
		void Start()
		{
			// Code Here
		}
		private void OnTriggerEnter2D(Collider2D other)
		{
			if (other.GetComponent<CollectableArea>())
			{
				Global.Coin.Value++;
				AudioKit.PlaySound("coin");
				this.DestroyGameObjGracefully();
			}
		}
	}
}
