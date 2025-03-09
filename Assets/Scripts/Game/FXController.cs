using UnityEngine;
using QFramework;
using Unity.VisualScripting;

namespace LastWizard
{
	public partial class FXController : ViewController
	{
		private static FXController Default;

        private void Awake()
		{
            Default = this;
        }

        private void OnDestroy()
        {
            Default = null;
        }

		public static void Play(SpriteRenderer sprite, Color dissolveColor)
		{
			Default.EnemyDieFX.Instantiate().Position(sprite.Position()).LocalScale(sprite.Scale()).Self(self =>
			{
				self.sprite = sprite.sprite;
				self.GetComponent<Dissolve>().DissolveColor = dissolveColor;
			}).Show();
        }
        void Start()
			{
				// Code Here
				
			}
	}
}
