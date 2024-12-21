using UnityEngine;
using QFramework;

namespace LastWizard
{
	public partial class SampleAbility : ViewController
	{
		private float mCurrentSeconds = 0;
		void Start()
		{
			// Code Here
		}

        private void Update()
        {
			mCurrentSeconds += Time.deltaTime;

			if(mCurrentSeconds >=  Global.SampleAbilityRate.Value) //�˺����
            {
				mCurrentSeconds = 0;
				var enemies = FindObjectsByType<Enemy>(FindObjectsInactive.Exclude,FindObjectsSortMode.None);

				foreach(Enemy enemy in enemies)
                {
					var distance = (Player.Default.transform.position - enemy.transform.position).magnitude;

					if(distance <= 3) //�˺�����
                    {
						enemy.Hurt(Global.SampleAbilityDamage.Value);
                    }
                }
            }
        }
    }
}
