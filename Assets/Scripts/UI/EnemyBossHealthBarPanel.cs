using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace LastWizard
{
	public class EnemyBossHealthBarPanelData : UIPanelData
	{
	}
	public partial class EnemyBossHealthBarPanel : UIPanel
	{
		protected override void OnInit(IUIData uiData = null)
		{
			mData = uiData as EnemyBossHealthBarPanelData ?? new EnemyBossHealthBarPanelData();
			// please add init code here
			Global.EnemyBossHealth.RegisterWithInitValue(HP =>
            {
				BossHpText.text = HP + "/" + Global.EnemyBossMaxHealth.Value;
                var sizeDelta = BossHpValue.rectTransform.sizeDelta;
                sizeDelta.x = 700 * HP / (float)Global.EnemyBossMaxHealth.Value;
                BossHpValue.rectTransform.sizeDelta = sizeDelta;
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
