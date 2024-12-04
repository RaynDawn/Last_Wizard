using System;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace LastWizard
{
	// Generate Id:56c7761d-5623-426a-ab66-fa9d917c77c8
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
