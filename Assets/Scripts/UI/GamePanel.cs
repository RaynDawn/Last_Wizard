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
			
			Global.Hp.RegisterWithInitValue(hp =>
			{
				HpText.text = "HP: " + Global.Hp.Value + "/" + Global.MaxHp.Value;
              
            }).UnRegisterWhenGameObjectDestroyed(gameObject);//每次生命值变更时回调执行

			Global.MaxHp.RegisterWithInitValue(maxhp =>
			{
				HpText.text = "HP: " + Global.Hp.Value + "/" + Global.MaxHp.Value;
			}).UnRegisterWhenGameObjectDestroyed(gameObject);//每次最大生命值变更时回调执行

			Global.Exp.RegisterWithInitValue(exp =>
			{
				ExpText.text = "EXP: " + exp + "/" + Global.LevelUpExp();
				
            }).UnRegisterWhenGameObjectDestroyed(gameObject);//每次经验值变更时回调执行

            Global.Anger.RegisterWithInitValue(anger =>
            {
                AngerText.text = "ANGER: " + anger + "/" + Global.MaxAnger.Value;
                var sizeDelta = AngerValue.rectTransform.sizeDelta;
                sizeDelta.y = 900 * anger / (float)Global.MaxAnger.Value;
                AngerValue.rectTransform.sizeDelta = sizeDelta;
            }).UnRegisterWhenGameObjectDestroyed(gameObject);//每次怒气值变更时回调执行

            Global.Lv.RegisterWithInitValue(lv =>
			{
				LvText.text = "LV: " + lv;
			}).UnRegisterWhenGameObjectDestroyed(gameObject);//每次等级变更时回调执行

			Global.Lv.Register(lv =>
			{
				Time.timeScale = 0;//升级时游戏暂停
				UpgradeRoot.Show();//升级时弹出升级选项

			}).UnRegisterWhenGameObjectDestroyed(gameObject);//初始化之后的每一次等级变更时执行

			Global.GuardNum.Register(num =>
			{
				Debug.Log(num);
				FindObjectOfType<GuardAbility>().GuardUpgrade(num);
			}).UnRegisterWhenGameObjectDestroyed(gameObject);//每次守卫数量变更时回调执行

			Global.Exp.RegisterWithInitValue(exp =>
			{
				if(exp >= Global.LevelUpExp())
                {
					Global.Exp.Value = 0;
					Global.Lv.Value++;
					Global.MaxHp.Value++;
					AudioKit.PlaySound("rise");
                }
			}).UnRegisterWhenGameObjectDestroyed(gameObject);//升级

			Global.CurrentTime.RegisterWithInitValue(secondsTemp =>
			{
				
				if(Time.frameCount % 30 == 0)
                {
					var secondsInt = Mathf.FloorToInt(secondsTemp);
					var seconds = secondsInt % 60;
					var minutes = secondsInt / 60;
					TimeText.text = "Time: " + $"{minutes:00}:{seconds:00}";
				}
				var player = Player.Default;
				if (player == null)
                {
					secondsTemp = 0;	
                }
			}).UnRegisterWhenGameObjectDestroyed(gameObject);

			

			Global.Coin.RegisterWithInitValue(coins =>//金币数量
			{
				CoinText.text = "COIN: " + coins;
			}).UnRegisterWhenGameObjectDestroyed(gameObject);
			
			BtnUpgrade.onClick.AddListener(() =>//升级攻击伤害
			{
				Time.timeScale = 1.0f;
				Global.SampleAbilityDamage.Value++;
				UpgradeRoot.Hide();
			});

			BtnUpgrade2.onClick.AddListener(() =>//升级攻击间隔
			{
				Time.timeScale = 1.0f;
				Global.SampleAbilityRate.Value *= 0.8f;
				UpgradeRoot.Hide();
			});

			Global.EnemyCount.RegisterWithInitValue(enemyCount =>//敌人数量
			{
				EnemyCountText.text = "ENEMY: " + enemyCount;
			}).UnRegisterWhenGameObjectDestroyed(gameObject);

			var enemyGenerator = FindAnyObjectByType<EnemyGenerator>();
			ActionKit.OnUpdate.Register(() =>
			{
				Global.CurrentTime.Value += Time.deltaTime;
				if(Global.CurrentTime.Value >= 180 && enemyGenerator.lastWave && !FindAnyObjectByType<Enemy>() && enemyGenerator.Wave == null && Global.EnemyCount.Value == 0)//当游戏持续时间超过一定时长 且 最后一波 且 没有敌人存在 且 波次结束 且 敌人数量为0
                {
					UIKit.OpenPanel<GamePassPanel>();
                }
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
