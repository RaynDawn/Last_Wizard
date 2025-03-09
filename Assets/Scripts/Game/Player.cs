using UnityEngine;
using QFramework;
using System.Xml.Linq;

namespace LastWizard
{
	public partial class Player : ViewController
	{
	    public float movementSpeed = 5;
        public RectTransform healthBar;
        public RectTransform ExpBar;
        public static Player Default;
        public int lastHp = Global.MaxHp.Value;

        [SerializeField] bool isPlayerControl = false;

        private void Awake()
        {
            Player.Default = this;
        }

        private void OnDestroy()
        {
            Default = null;
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            
        }
        void Start()
		{
			// Code Here
			
			
			HurtBox.OnTriggerEnter2DEvent(Collider2D => 
			{
				if (Collider2D.gameObject.tag != "Enemy") return;

				var hurtBox = Collider2D.GetComponent<HurtBox>();
				if (hurtBox)
				{
					if (hurtBox.Owner.CompareTag("Enemy"))
					{
						Hurt(1);
                    }
				}
			
			}).UnRegisterWhenGameObjectDestroyed(gameObject);

        

            Global.Exp.RegisterWithInitValue(exp =>
            {

                var sizeDelta = ExpValue.rectTransform.sizeDelta;
                sizeDelta.x = 300 * exp / (float)Global.LevelUpExp();
                ExpValue.rectTransform.sizeDelta = sizeDelta;
            }).UnRegisterWhenGameObjectDestroyed(gameObject);

            Global.Lv.RegisterWithInitValue(lv =>
            {
                LvText.text = "LV: " + lv;
            }).UnRegisterWhenGameObjectDestroyed(gameObject);

            Global.Hp.RegisterWithInitValue(hp =>
            {

                var sizeDelta = HpValue.rectTransform.sizeDelta;
                sizeDelta.x = 300 * hp / (float)Global.MaxHp.Value;
                HpValue.rectTransform.sizeDelta = sizeDelta;
                var hpDelta = lastHp - hp;
                if (hpDelta > 0)
                {
                    Global.Coin.Value += hpDelta;
                }
                if (hp <= 0)
                {
                    AudioKit.PlaySound("die");
                    Die();
                    
                }

            }).UnRegisterWhenGameObjectDestroyed(gameObject);

            Global.MaxHp.RegisterWithInitValue(maxhp =>
            {
                var sizeDelta = HpValue.rectTransform.sizeDelta;
                sizeDelta.x = 300 * Global.Hp.Value / (float)maxhp;
                HpValue.rectTransform.sizeDelta = sizeDelta;
            }).UnRegisterWhenGameObjectDestroyed(gameObject);

        }

		void Update()
		 {
            if (isPlayerControl)
            {
				Movement();

            }
            

        }

		void Movement()
		{
            var horizontal = Input.GetAxis("Horizontal");
            var vertical = Input.GetAxis("Vertical");

            var direction = new Vector2(horizontal, vertical).normalized;

            SelfRigidbody2D.velocity = direction * movementSpeed;
        }
        private bool IgnoreHurt = false;
        public void Hurt(float value)
        {
            if (IgnoreHurt) return;
            
            Sprite.color = Color.red;
          
            ActionKit.Delay(0.3f, () =>
            {
                this.Sprite.color = Color.white;
                Global.Hp.Value -= (int)value;
                AudioKit.PlaySound("pain");
                IgnoreHurt = false;
                
            }).Start(this);


        }

        public void Die()
        {
            this.DestroyGameObjGracefully();
            UIKit.OpenPanel<GameOverPanel>();
        }
    }

 }

