using UnityEngine;
using QFramework;

namespace LastWizard
{
	public partial class HurtBox : ViewController
	{
		public GameObject Owner;
		void Start()
		{
			// Code Here
			if (Owner == null)
			{
				Owner = transform.parent.gameObject;
			}
		}
	}
}
