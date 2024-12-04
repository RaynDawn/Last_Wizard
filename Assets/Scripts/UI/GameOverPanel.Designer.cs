using System;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace LastWizard
{
	// Generate Id:28ebaf23-6fd6-4262-b385-a9f55ba88923
	public partial class GameOverPanel
	{
		public const string Name = "GameOverPanel";
		
		
		private GameOverPanelData mPrivateData = null;
		
		protected override void ClearUIComponents()
		{
			
			mData = null;
		}
		
		public GameOverPanelData Data
		{
			get
			{
				return mData;
			}
		}
		
		GameOverPanelData mData
		{
			get
			{
				return mPrivateData ?? (mPrivateData = new GameOverPanelData());
			}
			set
			{
				mUIData = value;
				mPrivateData = value;
			}
		}
	}
}
