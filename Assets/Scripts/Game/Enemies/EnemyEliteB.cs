using UnityEngine;
using QFramework;
using System;

namespace LastWizard
{
	public partial class EnemyEliteB : ViewController,IEnemy
	{
        public float movementSpeed = 3;
        public float health = 3;
        public Color dissolveColor = Color.white;
        public float minAttackRange = 5;
        private float mCurrentSeconds = 0f;

        //public RectTransform healthBar;
        void Start()
        {
            // Code Here
            Global.EnemyCount.Value++;


        }

        private void OnDestroy()
        {
            Global.EnemyCount.Value--;
        }
        private void FixedUpdate()
        {
            mCurrentSeconds += Time.deltaTime;
            if (Player.Default)
            {
                var direction = (Player.Default.transform.position - transform.position).normalized;

                SelfRigidbody2D.velocity = direction * movementSpeed;

                if (mCurrentSeconds >= 1)
                {
                    mCurrentSeconds = 0;
                    Fire();
                }

                if ((Player.Default.transform.Position() - transform.Position()).magnitude <= minAttackRange)
                {
                    SelfRigidbody2D.velocity = Vector2.zero;
                   
                }
            }
            else 
            {
                SelfRigidbody2D.velocity = Vector2.zero;
            }
        }

        private void Fire()
        {
            
           
         
                Projectile.Instantiate().Show().Position(this.Position()).Self(self =>
                {
                    self.transform.SetParent(null);

                    var rigibody2D = self.GetComponent<Rigidbody2D>();

                    var direction = (Player.Default.transform.position - transform.position).normalized;

                    rigibody2D.AddForce(direction * 10, ForceMode2D.Impulse);
                });
                
                AudioKit.PlaySound("fire");
             
           

        }

        private void Update()
        {
            if (health <= 0)
            {
                this.DestroyGameObjGracefully();
                FXController.Play(Sprite, dissolveColor);
                Global.GenerateDrop(gameObject);
                if (Player.Default)
                {
                    Global.Anger.Value++;
                }
            }
        }
        private bool IgnoreHurt = false;



        public void Hurt(float value)
        {
            if (IgnoreHurt) return;
            TextController.PlayFloatingText(transform.position, value.ToString());
            Sprite.color = Color.red;
            AudioKit.PlaySound("hurt");
            ActionKit.Delay(0.3f, () =>
            {
                this.Sprite.color = Color.white;
                this.health -= value;
                IgnoreHurt = false;

                //UpdateHealthBar();
            }).Start(this);


        }
    }
}
