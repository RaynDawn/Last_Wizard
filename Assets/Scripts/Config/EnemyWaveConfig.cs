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

        [Serializable] //将怪物波次序列化
        public class EnemyWave
        {
            public string waveName;
            public bool isActive = true;
            public float GenerateDuration = 1;//波次间隔
            public GameObject EnemyPrefab;
            public float GenerateTime = 10;//波次持续时间
            public float SpeedScale = 1.0f;//生成敌人的速度比值
            public float HpScale = 1.0f;//生成敌人的生命比值
        }
    }
}

