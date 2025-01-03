using UnityEngine;
using QFramework;

namespace LastWizard
{
	public partial class CameraController : ViewController
	{
		private Vector2 mTargetPosition = Vector2.zero;
		void Start()
		{
			// Code Here
		}

        private void Update()
        {
            if(Player.Default != null)
            {
                mTargetPosition = Player.Default.transform.position;
                transform.PositionX(Mathf.Exp(1.0f - -Time.deltaTime * 20).Lerp(transform.position.x, mTargetPosition.x));//平滑，“Mathf.Exp(1.0f - -Time.deltaTime * 20）”为平滑度
                transform.PositionY(Mathf.Exp(1.0f - -Time.deltaTime * 20).Lerp(transform.position.y, mTargetPosition.y));
            }
        }
    }
}
