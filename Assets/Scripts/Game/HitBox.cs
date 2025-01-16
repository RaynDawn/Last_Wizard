using UnityEngine;
using QFramework;

namespace LastWizard
{
	public partial class HitBox : ViewController
	{
		public GameObject Owner;
		void Start()
		{
			// Code Here
			if( Owner == null )
            {
				Owner = transform.parent.gameObject;
            }
		}
	}
}
