using UnityEngine;
using QFramework;
using System;
using System.Collections;
using System.Collections.Generic;

namespace LastWizard
{
/*
	[Serializable] //�����ﲨ�����л�
	public class EnemyWave
	{
		public string waveName;
		public bool isActive = true;
		public float GenerateDuration = 1;//ˢ�ּ��
		public GameObject EnemyPrefab;
		public float GenerateTime = 10;//���γ���ʱ��
		public float SpeedScale = 1.0f;//���ɵ��˵��ٶȱ�ֵ
		public float HPScale = 1.0f;//���ɵ��˵�������ֵ
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

        private float currentSeconds = 0; //��ʱ��
		private float lastSeconds = 0; //����ʱ��
		private float genrateDistance = 10; //���ɾ���

		private Queue<EnemyWave> enemyWavesQueue = new Queue<EnemyWave>();
		public int waveCount = 0; //������
		private int totalWaveCount = 0; //�ܲ�����

        public bool lastWave => waveCount == totalWaveCount; //���һ��
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
			if(currentWave == null) //���ν��������ò���
            {
				if(enemyWavesQueue.Count >0)
                {
					waveCount++;
					currentWave = enemyWavesQueue.Dequeue();//����
					currentSeconds = 0;
					lastSeconds = 0;
					Debug.Log("currentWave:" + currentWave.waveName);
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

						currentWave.EnemyPrefab.Instantiate().Position(generatePos).Self(self =>
						{
                            self.GetComponent<Enemy>().movementSpeed *= currentWave.SpeedScale;
                            self.GetComponent<Enemy>().health *= currentWave.HPScale;
                        }).Show(); //�ӵ�ǰ���������ɵ�ǰ�ĵ���
						
                    }
				}

				if (lastSeconds >= currentWave.GenerateTime)//���γ���ʱ��ﵽ�ò��ε����ʱ��ʱ���ò���
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
