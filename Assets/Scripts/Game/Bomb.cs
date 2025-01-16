using UnityEngine;
using QFramework;
using System.Collections.Generic;
using System.Collections;

namespace LastWizard
{
	public partial class Bomb : ViewController
	{
        private float mCurrentSeconds = 0;

        private void Start()
        {
            this.OnTriggerEnter2DEvent(Collider2D =>
            {
                if (Collider2D.gameObject.tag != "Enemy") return;

                var hurtBox = Collider2D.GetComponent<HurtBox>();
              
                if (hurtBox)
                {
                    if (hurtBox.Owner.CompareTag("Enemy"))
                    {
                        mCurrentSeconds = 99;
                    }
                }
            }).UnRegisterWhenGameObjectDestroyed(gameObject);
        }

        private void Update()
        {
            mCurrentSeconds += Time.deltaTime;
            if (mCurrentSeconds >= 3) //±¬Õ¨ÑÓÊ±Ê±¼ä
			{
				Explode();
				CameraController.Shake();
				AudioKit.PlaySound("bomb");
				this.DestroyGameObjGracefully();
			}
        }

		void Explode()
		{
            Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, Global.BombAbilityRange.Value);
            if (hits.Length > 0)
            {
                foreach (Collider2D hit in hits)
                {
                    //if (hit.gameObject.tag != "Enemy") return;
                    var hurtBox = hit.GetComponent<HurtBox>();
                    if (hurtBox)
                    {
                        if (hurtBox.Owner.CompareTag("Enemy"))
                        {
                            hurtBox.Owner.GetComponent<Enemy>().Hurt(Global.BombAbilityDamage.Value);
                        }
                    }
                }
            }

			
		}
    }
}
