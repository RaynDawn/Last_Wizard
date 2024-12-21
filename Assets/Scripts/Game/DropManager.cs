using UnityEngine;
using QFramework;

namespace LastWizard
{
	public partial class DropManager : ViewController
	{
		public static DropManager Default;

        private void Awake()
        {
            Default = this;
        }

        private void OnDestroy()
        {
            Default = null;
        }
        void Start()
		{
			// Code Here
		}
	}
}
