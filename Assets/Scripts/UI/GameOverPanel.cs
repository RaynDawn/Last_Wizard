using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using QFramework;

namespace LastWizard
{
	public class GameOverPanelData : UIPanelData
	{
	}
	public partial class GameOverPanel : UIPanel
	{
		protected override void OnInit(IUIData uiData = null)
		{
			mData = uiData as GameOverPanelData ?? new GameOverPanelData();
			// please add init code here
			ActionKit.OnUpdate.Register(() =>
			{
				if(Input.GetKeyUp(KeyCode.Escape))
                {
					SceneManager.LoadScene("SampleScene");
					this.CloseSelf();
                }
			}
			).UnRegisterWhenGameObjectDestroyed(gameObject);
		}

        private void Update()
        {
            
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
