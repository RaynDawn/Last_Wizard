using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace LastWizard
{
	public class GameStartPanelData : UIPanelData
	{
	}
	public partial class GameStartPanel : UIPanel
	{
		protected override void OnInit(IUIData uiData = null)
		{
			mData = uiData as GameStartPanelData ?? new GameStartPanelData();
			// please add init code here
			BtnShowUpgradePanel.onClick.AddListener(() =>
			{
				CoinUpgradePanel.Show();
			});

			BtnClose.onClick.AddListener(() =>
            {
				CoinUpgradePanel.Hide();
            });
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
