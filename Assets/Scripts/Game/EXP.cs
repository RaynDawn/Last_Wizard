using UnityEngine;
using QFramework;

namespace LastWizard
{
	public partial class EXP : ViewController
	{
		void Start()
		{
			// Code Here
		}

        private void OnTriggerEnter2D(Collider2D other)
        {
            if(other.GetComponent<CollectableArea>())
            {
                Global.Exp.Value++;
                AudioKit.PlaySound("exp");
                this.DestroyGameObjGracefully();
            }
        }
    }
}
