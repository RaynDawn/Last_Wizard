using System;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace LastWizard
{
	// Generate Id:4b62d594-0889-4840-92f7-53f195153c19
	public partial class GamePanel
	{
		public const string Name = "GamePanel";
		
		[SerializeField]
		public UnityEngine.UI.Button BtnItem1;
		[SerializeField]
		public UnityEngine.UI.Button BtnItem2;
		[SerializeField]
		public UnityEngine.UI.Button BtnItem3;
		[SerializeField]
		public UnityEngine.UI.Button BtnItem4;
		[SerializeField]
		public UnityEngine.UI.Button BtnItem5;
		[SerializeField]
		public UnityEngine.UI.Button BtnMon1;
		[SerializeField]
		public UnityEngine.UI.Button BtnMon2;
		[SerializeField]
		public UnityEngine.UI.Button BtnMon3;
		[SerializeField]
		public UnityEngine.UI.Text ExpText;
		[SerializeField]
		public UnityEngine.UI.Text HpText;
		[SerializeField]
		public UnityEngine.UI.Text LvText;
		[SerializeField]
		public UnityEngine.UI.Text CoinText;
		[SerializeField]
		public UnityEngine.UI.Text TimeText;
		[SerializeField]
		public UnityEngine.UI.Text EnemyCountText;
		[SerializeField]
		public RectTransform UpgradeRoot;
		[SerializeField]
		public UnityEngine.UI.Button BtnUpgrade;
		[SerializeField]
		public UnityEngine.UI.Button BtnUpgrade2;
		[SerializeField]
		public RectTransform AngerUIRoot;
		[SerializeField]
		public UnityEngine.UI.Image AngerValue;
		[SerializeField]
		public UnityEngine.UI.Text AngerText;
		
		private GamePanelData mPrivateData = null;
		
		protected override void ClearUIComponents()
		{
			BtnItem1 = null;
			BtnItem2 = null;
			BtnItem3 = null;
			BtnItem4 = null;
			BtnItem5 = null;
			BtnMon1 = null;
			BtnMon2 = null;
			BtnMon3 = null;
			ExpText = null;
			HpText = null;
			LvText = null;
			CoinText = null;
			TimeText = null;
			EnemyCountText = null;
			UpgradeRoot = null;
			BtnUpgrade = null;
			BtnUpgrade2 = null;
			AngerUIRoot = null;
			AngerValue = null;
			AngerText = null;
			
			mData = null;
		}
		
		public GamePanelData Data
		{
			get
			{
				return mData;
			}
		}
		
		GamePanelData mData
		{
			get
			{
				return mPrivateData ?? (mPrivateData = new GamePanelData());
			}
			set
			{
				mUIData = value;
				mPrivateData = value;
			}
		}
	}
}
