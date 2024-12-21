using UnityEngine;
using QFramework;
using System;
using System.Collections;
using System.Collections.Generic;

namespace LastWizard
{
	[Serializable] //将怪物波次序列化
	public class EnemyWave
	{
		public float GenerateDuration = 1;//波次间隔
		public GameObject EnemyPrefab;
		public float GenerateTime = 10;//波次持续时间
	}
	public partial class EnemyGenerator : ViewController
	{
		private float currentSeconds = 0; //计时器
		private float lastSeconds = 0; //持续时间
		private float genrateDistance = 10; //生成距离
		public List<EnemyWave> enemyWaves = new List<EnemyWave>();
		private Queue<EnemyWave> enemyWavesQueue = new Queue<EnemyWave>();
		public int waveCount = 0; //波次数
		
		public bool lastWave => waveCount == enemyWaves.Count; //最后一波
		public EnemyWave Wave => currentWave;

		void Start()
		{
			// Code Here
			foreach(var enemyWave in enemyWaves)
            {
				enemyWavesQueue.Enqueue(enemyWave);//入队
            }
		}

		private EnemyWave currentWave = null;
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

						currentWave.EnemyPrefab.Instantiate().Position(generatePos).Show(); //从当前波次中生成当前的敌人
					}
				}

				if (lastSeconds >= currentWave.GenerateTime)//波次持续时间达到该波次的最大时长时重置波次
				{
					currentWave = null;
				}
			}
        }
    }
}
