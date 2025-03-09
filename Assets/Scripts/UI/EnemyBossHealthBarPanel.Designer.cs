using System;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace LastWizard
{
	// Generate Id:daa1c1ea-c1a1-418c-8aea-9d2779897489
	public partial class EnemyBossHealthBarPanel
	{
		public const string Name = "EnemyBossHealthBarPanel";
		
		[SerializeField]
		public UnityEngine.UI.Image BossHpValue;
		[SerializeField]
		public UnityEngine.UI.Text BossHpText;
		
		private EnemyBossHealthBarPanelData mPrivateData = null;
		
		protected override void ClearUIComponents()
		{
			BossHpValue = null;
			BossHpText = null;
			
			mData = null;
		}
		
		public EnemyBossHealthBarPanelData Data
		{
			get
			{
				return mData;
			}
		}
		
		EnemyBossHealthBarPanelData mData
		{
			get
			{
				return mPrivateData ?? (mPrivateData = new EnemyBossHealthBarPanelData());
			}
			set
			{
				mUIData = value;
				mPrivateData = value;
			}
		}
	}
}
