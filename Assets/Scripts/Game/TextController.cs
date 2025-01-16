using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace LastWizard
{
	public partial class TextController : ViewController
	{
		void Start()
		{
			// Code Here
		}

        public static TextController Default;

        private void Awake()
        {
            TextController.Default = this;
        }

        private void OnDestroy()
        {
            Default = null;
        }

        public static void PlayFloatingText(Vector2 position, string text)//伤害数值浮动显示
        {
            Default.FloatingText.InstantiateWithParent(Default.transform).PositionX(position.x).PositionY(position.y).Self(self =>
            {
                var positionY = position.y;
                var textTrans = self.transform.Find("DamageNumber");
                var textComp = textTrans.GetComponent<Text>();
                textComp.text = text;
                ActionKit.Delay(0.5f, () =>
                {
                    textTrans.DestroyGameObjGracefully();
                }).Start(textComp);
                ActionKit.Sequence().Lerp(0, 0.5f, 0.5f, t =>
                {
                    textComp.PositionY(positionY + t * 0.25f);
                    textComp.LocalScaleX(Mathf.Clamp01(t * 8));
                    textComp.LocalScaleY(Mathf.Clamp01(t * 8));
                }).Delay(0.5f).Lerp(1,0,0.3f, t =>
                {
                    textComp.ColorAlpha(t);
                }, () =>
                {
                    textTrans.DestroyGameObjGracefully();
                }).Start(textComp);
            }).Show();
        }
    }
}
