using System;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace LastWizard
{
	// Generate Id:4f6efe69-8a58-4198-9a96-af0fb837a909
	public partial class GamePassPanel
	{
		public const string Name = "GamePassPanel";
		
		
		private GamePassPanelData mPrivateData = null;
		
		protected override void ClearUIComponents()
		{
			
			mData = null;
		}
		
		public GamePassPanelData Data
		{
			get
			{
				return mData;
			}
		}
		
		GamePassPanelData mData
		{
			get
			{
				return mPrivateData ?? (mPrivateData = new GamePassPanelData());
			}
			set
			{
				mUIData = value;
				mPrivateData = value;
			}
		}
	}
}
