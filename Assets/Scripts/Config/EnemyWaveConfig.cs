using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LastWizard
{
    [CreateAssetMenu]
    public class EnemyWaveConfig : ScriptableObject
    {

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public class EnemyWaveGroup
        {
            public string groupName;
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
            public float HpScale = 1.0f;//���ɵ��˵�������ֵ
        }
    }
}

