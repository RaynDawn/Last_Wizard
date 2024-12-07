using UnityEngine;
using QFramework;

namespace LastWizard
{
	public partial class EnemyGenerator : ViewController
	{
		private float mCurrentSeconds = 0; //计时器
		private float genrateDistance = 10; //生成距离
		void Start()
		{
			// Code Here
		}

        private void Update()
        {
            mCurrentSeconds += Time.deltaTime;
			if(mCurrentSeconds >= 1) //生成间隔
            {
				mCurrentSeconds = 0;

				var player = Player.Default; 
				if(player != null)
                {
					var randomAngel = Random.Range(0, 360f);
					var randomRadius = randomAngel * Mathf.Deg2Rad;
					var direction = new Vector3(Mathf.Cos(randomRadius), Mathf.Sin(randomRadius));
					var generatePos = player.transform.position + direction * genrateDistance; //生成位置

					Enemy.Instantiate().Position(generatePos).Show();
				}
				
            }
        }
    }
}
