using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QFramework;

namespace LastWizard
{
    public class Global
    {
        // Start is called before the first frame update
        public static BindableProperty<int> Exp = new BindableProperty<int>(0); //����ֵ
        public static BindableProperty<int> Lv = new BindableProperty<int>(1); //�ȼ�
        public static BindableProperty<float> SampleAbilityDamage = new BindableProperty<float>(1);//�˺�
        public static BindableProperty<float> CurrentTime = new BindableProperty<float>(0);//ʱ��
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
    
}
