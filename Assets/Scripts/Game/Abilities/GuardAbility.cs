using UnityEngine;
using QFramework;
using System.Collections;
using System.Collections.Generic;

namespace LastWizard
{
/*	public class Guard
	{
		public GameObject GuardPrefab;
	}*/
	public partial class GuardAbility : ViewController
	{
		private float mCurrentSeconds = 0;
		
		private List<GameObject> guard = new List<GameObject>();
       

        void Start()
		{
			// Code Here
			
		}

		public void GuardUpgrade(int num)
        {
			num = Mathf.Clamp(num, 0, 3);
			if(guard.Count > 0)
            {
				Debug.Log(guard.Count);
				foreach(GameObject guard in guard)
                {
					Destroy(guard.gameObject);
                }
				guard.Clear();
            }
			
			for(float i = 0; i < 360; i += (360 / num))
            {
				InstantiateGuard(i);
				Debug.Log(i);
            }
        }

		void InstantiateGuard(float degree)
        {
			Guard.Instantiate().Position(this.Position()).Show().Self(self =>
			{
				self.transform.SetParent(this.transform);

				var radius = 3;

				guard.Add(self.gameObject);

				ActionKit.OnUpdate.Register(() =>
				{
					var circleLocalPos = new Vector2(Mathf.Cos(degree * Mathf.Deg2Rad), Mathf.Sin(degree * Mathf.Deg2Rad)) * radius;

					self.LocalPosition2D(circleLocalPos);

					var trans = self.gameObject.GetComponent<Transform>();

					var direction = (self.Position() - Player.Default.Position()).normalized;

					trans.up = direction.normalized;

				}).UnRegisterWhenGameObjectDestroyed(self);

				self.OnTriggerEnter2DEvent(collider =>
				{
					if (collider.gameObject.tag != "Enemy") return;

					var hurtBox = collider.GetComponent<HurtBox>();

					if (hurtBox)
					{
						if (hurtBox.Owner.CompareTag("Enemy"))
						{
							var enemy = hurtBox.Owner.GetComponent<IEnemy>();
							if (enemy != null)
							{
								enemy.Hurt(Global.SampleAbilityDamage.Value);
							}
						}
					}
				}).UnRegisterWhenGameObjectDestroyed(self);
			});
		}

        private void Update()
        {
			mCurrentSeconds += Time.deltaTime;
			if (mCurrentSeconds >= 10)
            {
				mCurrentSeconds = 0;
				if (Global.GuardNum.Value <= 3)
					Global.GuardNum.Value++;

			}
			transform.rotation = Quaternion.Euler(0, 0, Time.time * 60);

		}
    }
}
