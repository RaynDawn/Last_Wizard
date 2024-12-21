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
        public static BindableProperty<int> Coin = new BindableProperty<int>(0);//金币
        public static BindableProperty<int> Hp = new BindableProperty<int>(5);//生命值

        #endregion 

        [RuntimeInitializeOnLoadMethod]
        public static void ResetData() //重置数据
        {
            Exp.Value = 0;
            Lv.Value = 1;
            SampleAbilityDamage.Value = 1;
            SampleAbilityRate.Value = 1.5f;
            CurrentTime.Value = 0;
            EnemyCount.Value = 0;
            Hp.Value = 5;
        }

        public static int LevelUpExp()
        { 
         return Lv.Value * 5;
        }

        public static void GenerateDrop(GameObject gameObject)
        {
            var random = Random.Range(0, 100f);
            if(random <= 90)
            {
                DropManager.Default.EXP.Instantiate().Position(gameObject.Position()).Show(); //生成经验掉落
            }
            else
            {
                DropManager.Default.Coin.Instantiate().Position(gameObject.Position()).Show();//生成金币
            }
               
        }

        public static void AutoInit()
        {
            ResKit.Init();
            UIKit.Root.SetResolution(1980, 1080,1);
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
