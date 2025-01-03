using UnityEngine;
using QFramework;

namespace LastWizard
{
	public partial class BombAbility : ViewController
	{
		private float mCurrentSeconds = 0;
        private float bombSpeed = 5.0f;

        void Start()
		{
			// Code Here
		}
        private void Update()
        {
			mCurrentSeconds += Time.deltaTime;

			if (mCurrentSeconds >= Global.BombAbilityRate.Value) //¼¼ÄÜ¼ä¸ô
			{
				mCurrentSeconds = 0;

				Bomb.Instantiate().Show().Position(this.Position()).Self(self =>
					{
						self.transform.SetParent(null);
						
						var rigibody2D = self.GetComponent<Rigidbody2D>();

						var enemies = FindObjectsByType<Enemy>(FindObjectsInactive.Exclude, FindObjectsSortMode.None);

						var finalDir = Vector2.zero;

						foreach (var enemy in enemies)
						{
							finalDir += (Vector2)(transform.position - enemy.transform.position);
						}

						rigibody2D.AddForce(bombSpeed * -finalDir.normalized, ForceMode2D.Impulse);
					});
			}
		}

    }
}
