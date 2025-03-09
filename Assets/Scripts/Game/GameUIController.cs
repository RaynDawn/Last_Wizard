using UnityEngine;
using QFramework;

namespace LastWizard
{
	public partial class GameUIController : ViewController
	{
		void Start()
		{
			// Code Here
			UIKit.OpenPanel<GamePanel>();
		}
        private void Update()
        {
           /* if(EnemyBoss.Default)
            {
                UIKit.OpenPanel<EnemyBossHealthBarPanel>();
              
            }
            else
            {
                UIKit.ClosePanel<EnemyBossHealthBarPanel>();
            }*/


        }
        private void OnDestroy()
        {
            UIKit.ClosePanel<GamePanel>();
        }
    }
}
