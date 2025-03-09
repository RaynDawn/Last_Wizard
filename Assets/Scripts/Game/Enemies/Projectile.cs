using UnityEngine;
using QFramework;

namespace LastWizard
{
	public partial class Projectile : ViewController
	{
		void Start()
		{
            // Code Here
            this.OnTriggerEnter2DEvent(Collider2D =>
            {
                

                var hurtBox = Collider2D.GetComponent<HurtBox>();

                if (hurtBox)
                {
                    if (hurtBox.Owner.CompareTag("Hero"))
                    {
                        Global.Hp.Value --;
                        this.DestroyGameObjGracefully();
                        Debug.Log("Hit");
                    }
                }
            }).UnRegisterWhenGameObjectDestroyed(gameObject);
        }
	}
}
