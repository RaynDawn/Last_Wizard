using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QFramework;

namespace LastWizard
{
    public class Global
    {
        // Start is called before the first frame update
        public static BindableProperty<int> Exp = new BindableProperty<int>(0); //经验值
        public static BindableProperty<int> Lv = new BindableProperty<int>(1); //等级
        public static BindableProperty<float> SampleAbilityDamage = new BindableProperty<float>(1);//伤害
        public static BindableProperty<float> CurrentTime = new BindableProperty<float>(0);//时间
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
    
}
