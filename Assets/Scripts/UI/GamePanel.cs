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
              
            }).UnRegisterWhenGameObjectDestroyed(gameObject);//ÿ������ֵ���ʱ�ص�ִ��

			Global.MaxHp.RegisterWithInitValue(maxhp =>
			{
				HpText.text = "HP: " + Global.Hp.Value + "/" + Global.MaxHp.Value;
			}).UnRegisterWhenGameObjectDestroyed(gameObject);//ÿ���������ֵ���ʱ�ص�ִ��

			Global.Exp.RegisterWithInitValue(exp =>
			{
				ExpText.text = "EXP: " + exp + "/" + Global.LevelUpExp();
				
            }).UnRegisterWhenGameObjectDestroyed(gameObject);//ÿ�ξ���ֵ���ʱ�ص�ִ��

            Global.Anger.RegisterWithInitValue(anger =>
            {
                AngerText.text = "ANGER: " + anger + "/" + Global.MaxAnger.Value;
                var sizeDelta = AngerValue.rectTransform.sizeDelta;
                sizeDelta.y = 900 * anger / (float)Global.MaxAnger.Value;
                AngerValue.rectTransform.sizeDelta = sizeDelta;
            }).UnRegisterWhenGameObjectDestroyed(gameObject);//ÿ��ŭ��ֵ���ʱ�ص�ִ��

            Global.Lv.RegisterWithInitValue(lv =>
			{
				LvText.text = "LV: " + lv;
			}).UnRegisterWhenGameObjectDestroyed(gameObject);//ÿ�εȼ����ʱ�ص�ִ��

			Global.Lv.Register(lv =>
			{
				Time.timeScale = 0;//����ʱ��Ϸ��ͣ
				UpgradeRoot.Show();//����ʱ��������ѡ��

			}).UnRegisterWhenGameObjectDestroyed(gameObject);//��ʼ��֮���ÿһ�εȼ����ʱִ��

			Global.GuardNum.Register(num =>
			{
				Debug.Log(num);
				FindObjectOfType<GuardAbility>().GuardUpgrade(num);
			}).UnRegisterWhenGameObjectDestroyed(gameObject);//ÿ�������������ʱ�ص�ִ��

			Global.Exp.RegisterWithInitValue(exp =>
			{
				if(exp >= Global.LevelUpExp())
                {
					Global.Exp.Value = 0;
					Global.Lv.Value++;
					Global.MaxHp.Value++;
					AudioKit.PlaySound("rise");
                }
			}).UnRegisterWhenGameObjectDestroyed(gameObject);//����

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

			

			Global.Coin.RegisterWithInitValue(coins =>//�������
			{
				CoinText.text = "COIN: " + coins;
			}).UnRegisterWhenGameObjectDestroyed(gameObject);
			
			BtnUpgrade.onClick.AddListener(() =>//���������˺�
			{
				Time.timeScale = 1.0f;
				Global.SampleAbilityDamage.Value++;
				UpgradeRoot.Hide();
			});

			BtnUpgrade2.onClick.AddListener(() =>//�����������
			{
				Time.timeScale = 1.0f;
				Global.SampleAbilityRate.Value *= 0.8f;
				UpgradeRoot.Hide();
			});

			Global.EnemyCount.RegisterWithInitValue(enemyCount =>//��������
			{
				EnemyCountText.text = "ENEMY: " + enemyCount;
			}).UnRegisterWhenGameObjectDestroyed(gameObject);

			var enemyGenerator = FindAnyObjectByType<EnemyGenerator>();
			ActionKit.OnUpdate.Register(() =>
			{
				Global.CurrentTime.Value += Time.deltaTime;
				if(Global.CurrentTime.Value >= 180 && enemyGenerator.lastWave && !FindAnyObjectByType<Enemy>() && enemyGenerator.Wave == null && Global.EnemyCount.Value == 0)//����Ϸ����ʱ�䳬��һ��ʱ�� �� ���һ�� �� û�е��˴��� �� ���ν��� �� ��������Ϊ0
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
