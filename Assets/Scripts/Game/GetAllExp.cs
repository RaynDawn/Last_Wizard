using UnityEngine;
using QFramework;

namespace LastWizard
{
	public partial class GetAllExp : ViewController
	{
		private float expSpeed = 10;
		void Start()
		{
			// Code Here
		}

		private void OnTriggerEnter2D(Collider2D other)
		{
			if (other.GetComponent<CollectableArea>())
			{
				var exps = FindObjectsByType<EXP>(FindObjectsInactive.Exclude, FindObjectsSortMode.None);
				foreach (EXP exp in exps)
				{
					ActionKit.OnUpdate.Register(() =>
					{
						var player = Player.Default;
						if(player != null)
                        {
							var direction = player.Position() - exp.Position();
							exp.transform.Translate(direction.normalized * Time.deltaTime * expSpeed);
                        }
					}).UnRegisterWhenGameObjectDestroyed(exp);
				}
				AudioKit.PlaySound("get_all_exp");
				this.DestroyGameObjGracefully();
			}
		}
	}
}
