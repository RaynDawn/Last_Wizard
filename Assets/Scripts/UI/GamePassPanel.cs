using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using QFramework;

namespace LastWizard
{
	public class GamePassPanelData : UIPanelData
	{
	}
	public partial class GamePassPanel : UIPanel
	{
		protected override void OnInit(IUIData uiData = null)
		{
			mData = uiData as GamePassPanelData ?? new GamePassPanelData();
			// please add init code here
			
			ActionKit.OnUpdate.Register(() =>
			{
				if (Input.GetKeyUp(KeyCode.Escape))
				{
					SceneManager.LoadScene("SampleScene");
					this.CloseSelf();
					Global.ResetData();
				}
			}
			).UnRegisterWhenGameObjectDestroyed(gameObject);
		}
		
		protected override void OnOpen(IUIData uiData = null)
		{
			Time.timeScale = 0;
		}
		
		protected override void OnShow()
		{
		}
		
		protected override void OnHide()
		{
		}
		
		protected override void OnClose()
		{
			Time.timeScale = 1;
		}
	}
}
