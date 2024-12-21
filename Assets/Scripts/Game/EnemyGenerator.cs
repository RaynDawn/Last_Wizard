using UnityEngine;
using QFramework;
using System;
using System.Collections;
using System.Collections.Generic;

namespace LastWizard
{
	[Serializable] //�����ﲨ�����л�
	public class EnemyWave
	{
		public float GenerateDuration = 1;//���μ��
		public GameObject EnemyPrefab;
		public float GenerateTime = 10;//���γ���ʱ��
	}
	public partial class EnemyGenerator : ViewController
	{
		private float currentSeconds = 0; //��ʱ��
		private float lastSeconds = 0; //����ʱ��
		private float genrateDistance = 10; //���ɾ���
		public List<EnemyWave> enemyWaves = new List<EnemyWave>();
		private Queue<EnemyWave> enemyWavesQueue = new Queue<EnemyWave>();
		public int waveCount = 0; //������
		
		public bool lastWave => waveCount == enemyWaves.Count; //���һ��
		public EnemyWave Wave => currentWave;

		void Start()
		{
			// Code Here
			foreach(var enemyWave in enemyWaves)
            {
				enemyWavesQueue.Enqueue(enemyWave);//���
            }
		}

		private EnemyWave currentWave = null;
        private void Update()
        {
			if(currentWave == null) //���ν��������ò���
            {
				if(enemyWavesQueue.Count >0)
                {
					waveCount++;
					currentWave = enemyWavesQueue.Dequeue();//����
					currentSeconds = 0;
					lastSeconds = 0;
                }
            }

			if(currentWave != null)//���ο�ʼʱ��¼ʱ�䲢���ɵ���
            {
				currentSeconds += Time.deltaTime;
				lastSeconds += Time.deltaTime;

				if (currentSeconds >= currentWave.GenerateDuration) //ÿ��һ��ʱ������һ������
                {
					currentSeconds = 0;

					var player = Player.Default;
					if (player != null)
					{
						var randomAngel = UnityEngine.Random.Range(0, 360f);
						var randomRadius = randomAngel * Mathf.Deg2Rad;
						var direction = new Vector3(Mathf.Cos(randomRadius), Mathf.Sin(randomRadius));
						var generatePos = player.transform.position + direction * genrateDistance; //����λ��

						currentWave.EnemyPrefab.Instantiate().Position(generatePos).Show(); //�ӵ�ǰ���������ɵ�ǰ�ĵ���
					}
				}

				if (lastSeconds >= currentWave.GenerateTime)//���γ���ʱ��ﵽ�ò��ε����ʱ��ʱ���ò���
				{
					currentWave = null;
				}
			}
        }
    }
}
