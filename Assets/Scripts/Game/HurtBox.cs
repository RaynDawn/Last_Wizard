using UnityEngine;
using QFramework;
using System;

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
