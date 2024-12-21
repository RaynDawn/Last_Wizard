using System;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace LastWizard
{
	// Generate Id:de948119-ee02-4e4f-b136-7eab974891e1
	public partial class GameStartPanel
	{
		public const string Name = "GameStartPanel";
		
		[SerializeField]
		public UnityEngine.UI.Button BtnShowUpgradePanel;
		[SerializeField]
		public RectTransform CoinUpgradePanel;
		[SerializeField]
		public UnityEngine.UI.Button BtnCoinUpgrade;
		[SerializeField]
		public UnityEngine.UI.Button BtnCoinUpgrade2;
		[SerializeField]
		public UnityEngine.UI.Button BtnClose;
		
		private GameStartPanelData mPrivateData = null;
		
		protected override void ClearUIComponents()
		{
			BtnShowUpgradePanel = null;
			CoinUpgradePanel = null;
			BtnCoinUpgrade = null;
			BtnCoinUpgrade2 = null;
			BtnClose = null;
			
			mData = null;
		}
		
		public GameStartPanelData Data
		{
			get
			{
				return mData;
			}
		}
		
		GameStartPanelData mData
		{
			get
			{
				return mPrivateData ?? (mPrivateData = new GameStartPanelData());
			}
			set
			{
				mUIData = value;
				mPrivateData = value;
			}
		}
	}
}
