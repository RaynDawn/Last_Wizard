using System;
using System.Collections;
using System.Collections.Generic;
using QFramework;
using UnityEngine;

namespace LastWizard
{
    [CreateAssetMenu]
    public class EnemyWaveConfig : ScriptableObject
    {
        [SerializeField]
        public List<EnemyWaveGroup> enemyWaveGroups = new List<EnemyWaveGroup>();

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

    }

    [Serializable]
    public class EnemyWaveGroup
    {
        public string groupName;
        public float groupTime = 10;
        [TextArea]
        public string groupDescription = string.Empty;
        [SerializeField]
        public List<EnemyWave> enemyWaves = new List<EnemyWave>();
    }

    [Serializable] //�����ﲨ�����л�
    public class EnemyWave
    {
        public string waveName;
        public bool isActive = true;
        public float GenerateDuration = 1;//���μ��
        public GameObject EnemyPrefab;
        public float GenerateTime = 10;//���γ���ʱ��
        public float SpeedScale = 1.0f;//���ɵ��˵��ٶȱ�ֵ
        public float HPScale = 1.0f;//���ɵ��˵�������ֵ
    }
}

