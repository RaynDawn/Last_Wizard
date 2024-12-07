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
            }).UnRegisterWhenGameObjectDestroyed(gameObject);//每次经验值变更时回调执行

			Global.Lv.RegisterWithInitValue(lv =>
			{
				LvText.text = "LV: " + lv;
			}).UnRegisterWhenGameObjectDestroyed(gameObject);//每次等级变更时回调执行

			Global.Lv.Register(lv =>
			{
				Time.timeScale = 0;//升级时游戏暂停
				BtnUpgrade.Show();//升级时弹出升级选项

			}).UnRegisterWhenGameObjectDestroyed(gameObject);//初始化之后的每一次等级变更时执行

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
