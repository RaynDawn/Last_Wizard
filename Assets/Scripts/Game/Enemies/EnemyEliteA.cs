using UnityEngine;
using QFramework;
using static UnityEngine.Rendering.DebugUI;

namespace LastWizard
{
	public partial class EnemyEliteA : ViewController, IEnemy
    
	{
        public float movementSpeed = 3;
        public float health = 3;
        public Color dissolveColor = Color.white;
        public int damage = 5;
        public float range = 5;

        public enum Elite_States
        {
            Following,
            Warning,
            Bombing,
        }

        public FSM<Elite_States> FSM = new FSM<Elite_States>();
        void Start()
		{
            // Code Here
            Global.EnemyCount.Value++;
           /* FSM.State(Elite_States.Following).OnFixedUpdate(() =>
            {
                Debug.Log("Following");
                if (Player.Default)
                {
                    var direction = (Player.Default.transform.position - transform.position).normalized;

                    SelfRigidbody2D.velocity = direction * movementSpeed;

                    Debug.Log("Following");

                    if ((Player.Default.transform.Position() - transform.Position()).magnitude <= 5)
                    {
                        FSM.ChangeState(Elite_States.Warning);
                    }
                }
                else
                {
                    SelfRigidbody2D.velocity = Vector2.zero;
                    Debug.Log("No Player");
                }
            });

            var warningTime = 0f;
            var mCurrentSeconds = 0f;
            FSM.State(Elite_States.Warning).OnFixedUpdate(() =>
            {
                if(Player.Default)
                {
                    var direction = (Player.Default.transform.position - transform.position).normalized;
                    SelfRigidbody2D.velocity = direction * movementSpeed * 1.5f;
                    
                    warningTime += Time.deltaTime;
                    mCurrentSeconds += Time.deltaTime;
                    if (mCurrentSeconds > 0.5f)
                    {
                        mCurrentSeconds = 0;
                        Sprite.color = Color.red;
                        ActionKit.Delay(0.3f, () =>
                        {
                            this.Sprite.color = Color.white;
                        }).Start(this);
                    }
                    if (warningTime >= 5 || (Player.Default.transform.Position() - transform.Position()).magnitude <= 2)
                    {
                        FSM.ChangeState(Elite_States.Bombing);
                    }
                }
                else
                {
                    SelfRigidbody2D.velocity = Vector2.zero;
                }
            });
            FSM.State(Elite_States.Bombing).OnEnter(() =>
            {
                Explode();
                AudioKit.PlaySound("bomb");

                CameraController.Shake();
                health = 0;
                this.DestroyGameObjGracefully();
            });*/

        }

        private void OnDestroy()
        {
            Global.EnemyCount.Value--;
        }

    

       

        void FixedUpdate()
        {
            //FSM.FixedUpdate();
            if (Player.Default)
            {
                var direction = (Player.Default.transform.position - transform.position).normalized;

                SelfRigidbody2D.velocity = direction * movementSpeed;

                Debug.Log("Following");

                if ((Player.Default.transform.Position() - transform.Position()).magnitude <= 5)
                {
                    var warningTime = 0f;
                    var mCurrentSeconds = 0f;
                    if (Player.Default)
                    {
                        
                        SelfRigidbody2D.velocity = direction * movementSpeed * 1.5f;

                        warningTime += Time.deltaTime;
                        mCurrentSeconds += Time.deltaTime;
                        if (mCurrentSeconds > 0.5f)
                        {
                            mCurrentSeconds = 0;
                            Sprite.color = Color.red;
                            ActionKit.Delay(0.3f, () =>
                            {
                                this.Sprite.color = Color.white;
                            }).Start(this);
                        }
                        if (warningTime >= 5 || (Player.Default.transform.Position() - transform.Position()).magnitude <= 2)
                        {
                           
                            health = 0;
                            
                        }
                    }
                }
            }
            else
            {
                SelfRigidbody2D.velocity = Vector2.zero;
                Debug.Log("No Player");
            }
        }
        void Update()
        {
           // FSM.Update();
            if (health <= 0)
            {
                if (Player.Default)
                {
                    Global.Anger.Value++;
                }
                Explode();
                AudioKit.PlaySound("bomb");

                CameraController.Shake();
                Global.GenerateDrop(gameObject);
                this.DestroyGameObjGracefully();

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

        void Explode()
        {
            Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, range);
            if (hits.Length > 0)
            {
                foreach (Collider2D hit in hits)
                {
                   
                    var hurtBox = hit.GetComponent<HurtBox>();
                    if (hurtBox)
                    {
                      
                        if (hurtBox.Owner.CompareTag("Enemy"))
                        {
                            hurtBox.Owner.GetComponent<IEnemy>().Hurt(damage);
                        }
                        else if (hurtBox.Owner.CompareTag("Hero"))
                        {
                            Global.Hp.Value -= damage;
                        }
                    }
                }
            }


        }
    }
}
