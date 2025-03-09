using UnityEngine;
using QFramework;
using System;
using System.Collections;
using System.Collections.Generic;

namespace LastWizard
{
/*
	[Serializable] //将怪物波次序列化
	public class EnemyWave
	{
		public string waveName;
		public bool isActive = true;
		public float GenerateDuration = 1;//刷怪间隔
		public GameObject EnemyPrefab;
		public float GenerateTime = 10;//波次持续时间
		public float SpeedScale = 1.0f;//生成敌人的速度比值
		public float HPScale = 1.0f;//生成敌人的生命比值
	}
*/
	/*[Serializable]
	public class EnemyWaveGroup
	{
		public string groupName;
		[TextArea]
		public string groupDescription = string.Empty;
		[SerializeField]
		public List<EnemyWave> enemyWaves = new List<EnemyWave>();
	}*/

	public partial class EnemyGenerator : ViewController
	{
        [SerializeField]

        public EnemyWaveConfig Config;
       /* public List<EnemyWaveGroup> enemyWaveGroups = new List<EnemyWaveGroup>();*/

        private float currentSeconds = 0; //计时器
		private float lastSeconds = 0; //持续时间
		private float genrateDistance = 10; //生成距离

		private Queue<EnemyWave> enemyWavesQueue = new Queue<EnemyWave>();
		public int waveCount = 0; //波次数
		private int totalWaveCount = 0; //总波次数

        public bool lastWave => waveCount == totalWaveCount; //最后一波
		public EnemyWave Wave => currentWave;

		void Start()
		{
			// Code Here
			foreach (var group in Config.enemyWaveGroups)
			{
				foreach (var wave in group.enemyWaves)
				{
                    enemyWavesQueue.Enqueue(wave);
					totalWaveCount++;
					Debug.Log("waveCount:" + waveCount);
                }
			}
		}

		private EnemyWave currentWave = null;
		//private EnemyWaveGroup currentGroup = null;
        private void Update()
        {
			if(currentWave == null) //波次结束后重置波次
            {
				if(enemyWavesQueue.Count >0)
                {
					waveCount++;
					currentWave = enemyWavesQueue.Dequeue();//出队
					currentSeconds = 0;
					lastSeconds = 0;
					Debug.Log("currentWave:" + currentWave.waveName);
                }
            }

			if(currentWave != null)//波次开始时记录时间并生成敌人
            {
				currentSeconds += Time.deltaTime;
				lastSeconds += Time.deltaTime;

				if (currentSeconds >= currentWave.GenerateDuration) //每隔一段时间生成一波敌人
                {
					currentSeconds = 0;
					
                    var player = Player.Default;
					if (player != null)
					{
						var randomAngel = UnityEngine.Random.Range(0, 360f);
						var randomRadius = randomAngel * Mathf.Deg2Rad;
						var direction = new Vector3(Mathf.Cos(randomRadius), Mathf.Sin(randomRadius));
						var generatePos = player.transform.position + direction * genrateDistance; //生成位置

						currentWave.EnemyPrefab.Instantiate().Position(generatePos).Self(self =>
						{
                            self.GetComponent<Enemy>().movementSpeed *= currentWave.SpeedScale;
                            self.GetComponent<Enemy>().health *= currentWave.HPScale;
                        }).Show(); //从当前波次中生成当前的敌人
						
                    }
				}

				if (lastSeconds >= currentWave.GenerateTime)//波次持续时间达到该波次的最大时长时重置波次
				{
					currentWave = null;
					/*if(lastSeconds >= currentGroup.groupTime)
                    {
                        currentGroup = null;
                    }*/
                }
			}
        }
    }
}
