using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QFramework;

namespace LastWizard
{
    public class Global : Architecture<Global>
    {
        // Start is called before the first frame update
        #region Model
        public static BindableProperty<int> Exp = new BindableProperty<int>(0); //经验值
        public static BindableProperty<int> Lv = new BindableProperty<int>(1); //等级
        public static BindableProperty<float> SampleAbilityDamage = new BindableProperty<float>(1);//攻击伤害
        public static BindableProperty<float> CurrentTime = new BindableProperty<float>(0);//时间
        public static BindableProperty<int> EnemyCount = new BindableProperty<int>(0); //怪物数量
        public static BindableProperty<float> SampleAbilityRate = new BindableProperty<float>(1.5f);//攻击间隔
        public static BindableProperty<float> BombAbilityRate = new BindableProperty<float>(5);//技能间隔
        public static BindableProperty<int> Coin = new BindableProperty<int>(0);//金币
        public static BindableProperty<int> Hp = new BindableProperty<int>(5);//生命值
        public static BindableProperty<int> MaxHp = new BindableProperty<int>(5);//最大生命值
        public static BindableProperty<float> BombAbilityDamage = new BindableProperty<float>(99);//炸弹伤害
        public static BindableProperty<float> BombAbilityRange = new BindableProperty<float>(5);//炸弹范围
        public static BindableProperty<float> DestroyTime = new BindableProperty<float>(10);//炸弹引信延时时间
        public static BindableProperty<float> CritRate = new BindableProperty<float>(0.1f);//暴击率
        public static BindableProperty<int> GuardNum = new BindableProperty<int>(0);
        public static BindableProperty<int> EnemyBossHealth = new BindableProperty<int>(50);
        public static BindableProperty<int> EnemyBossMaxHealth = new BindableProperty<int>(50);
        public static BindableProperty<int> Anger = new BindableProperty<int>(0);
        public static BindableProperty<int> MaxAnger = new BindableProperty<int>(100);

        #endregion 

        [RuntimeInitializeOnLoadMethod]
        public static void ResetData() //重置数据
        {
            Exp.Value = 0;
            Lv.Value = 1;
            SampleAbilityDamage.Value = 1;
            SampleAbilityRate.Value = 1.5f;
            BombAbilityRate.Value = 5;
            CurrentTime.Value = 0;
            EnemyCount.Value = 0;
            MaxHp.Value = 100;
            Hp.Value = MaxHp.Value;
            BombAbilityDamage.Value = 20;
            BombAbilityRange.Value = 5;
            DestroyTime.Value = 10;
            CritRate.Value = 0.1f;
            GuardNum.Value = 0;
            EnemyBossMaxHealth.Value = 30;
            EnemyBossHealth.Value = EnemyBossMaxHealth.Value;
            Anger.Value = 0;
            MaxAnger.Value = 100;
            Coin.Value = 0;
        }

        public static int LevelUpExp()
        { 
         return Lv.Value * 5;
        }

        public static void GenerateDrop(GameObject gameObject)
        {
            if (gameObject == null) return;

            // 概率（百分比），可按需调整，合计不必强制为 100，但推荐如此以便直观理解
            const float probExp = 90f;
            const float probCoin = 5f;
            const float probHP = 3f;
            const float probGetAllExp = 2f;

            // 使用单次随机并按累积区间判断，避免多次独立随机导致的条件概率混淆
            float r = Random.Range(0f, 100f);

            if (r < probExp)
            {
                DropManager.Default.EXP.Instantiate().Position(gameObject.Position()).Show(); //生成经验掉落
                return;
            }

            if (r < probExp + probCoin)
            {
                DropManager.Default.Coin.Instantiate().Position(gameObject.Position()).Show();//生成金币
                return;
            }

            if (r < probExp + probCoin + probHP)
            {
                DropManager.Default.HP.Instantiate().Position(gameObject.Position()).Show();//生成血瓶
                return;
            }

            if (r < probExp + probCoin + probHP + probGetAllExp)
            {
                DropManager.Default.GetAllExp.Instantiate().Position(gameObject.Position()).Show();//生成经验自动拾取
                return;
            }

            // 如果总概率小于 100%，这里可选择什么也不生成或作为默认处理；当前实现为不生成任何掉落。
        }

        public static void AutoInit()
        {
            ResKit.Init();
            UIKit.Root.SetResolution(1980, 1080,1);
            Global.Hp.Value = Global.MaxHp.Value;
            Global.MaxHp.Value = PlayerPrefs.GetInt(nameof(MaxHp), 5);//初始化最大生命值
            Global.MaxHp.Register(maxhp =>
            {
                PlayerPrefs.SetInt(nameof(MaxHp), maxhp);//加载最大生命值
            });
        }
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        protected override void Init()
        {
           
        }
    }
    
}
