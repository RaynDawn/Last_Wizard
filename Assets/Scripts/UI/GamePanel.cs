using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace LastWizard
{
	public class GamePanelData : UIPanelData
	{
	}
	public partial class GamePanel : UIPanel
	{
		protected override void OnInit(IUIData uiData = null)
		{
			mData = uiData as GamePanelData ?? new GamePanelData();
			// please add init code here
			Global.Exp.RegisterWithInitValue(exp =>
			{
				ExpText.text = "EXP: " + exp;
            }).UnRegisterWhenGameObjectDestroyed(gameObject);//ÿ�ξ���ֵ���ʱ�ص�ִ��

			Global.Lv.RegisterWithInitValue(lv =>
			{
				LvText.text = "LV: " + lv;
			}).UnRegisterWhenGameObjectDestroyed(gameObject);//ÿ�εȼ����ʱ�ص�ִ��

			Global.Lv.Register(lv =>
			{
				Time.timeScale = 0;//����ʱ��Ϸ��ͣ
				BtnUpgrade.Show();//����ʱ��������ѡ��

			}).UnRegisterWhenGameObjectDestroyed(gameObject);//��ʼ��֮���ÿһ�εȼ����ʱִ��

			Global.Exp.RegisterWithInitValue(exp =>
			{
				if(exp >= 5)
                {
					Global.Exp.Value -= 5;
					Global.Lv.Value ++;
                }
			}).UnRegisterWhenGameObjectDestroyed(gameObject);

			Global.CurrentTime.RegisterWithInitValue(seconds =>
			{
				if(Time.frameCount % 30 == 0)
                {
					TimeText.text = "Time: " + seconds;
				}
			}).UnRegisterWhenGameObjectDestroyed(gameObject);

			
			BtnUpgrade.onClick.AddListener(() =>
			{
				Time.timeScale = 1.0f;
				Global.SampleAbilityDamage.Value++;
				BtnUpgrade.Hide();
			});

			ActionKit.OnUpdate.Register(() =>
			{
				Global.CurrentTime.Value += Time.deltaTime;
			}).UnRegisterWhenGameObjectDestroyed(gameObject);
		}
		
		protected override void OnOpen(IUIData uiData = null)
		{
		}
		
		protected override void OnShow()
		{
		}
		
		protected override void OnHide()
		{
		}
		
		protected override void OnClose()
		{
		}
	}
}
