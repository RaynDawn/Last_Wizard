using UnityEngine;
using QFramework;

namespace LastWizard
{
	public partial class CameraController : ViewController
	{
		private Vector2 mTargetPosition = Vector2.zero;
        private Vector3 mCurrentCameraPos = Vector3.zero;
        private bool isShake = false;
        private float mShakeFrame = 0;
        private float mShakeAmplitude = 2;

        static CameraController Default;
        private void Awake()
        {
            CameraController.Default = this;
        }

        private void OnDestroy()
        {
            Default = null;
        }
        void Start()
		{
            Application.targetFrameRate = 60;
		}

        private void Update()
        {
            if(Player.Default != null)
            {
                mTargetPosition = Player.Default.transform.position;
                mCurrentCameraPos.x = (1.0f - Mathf.Exp(-Time.deltaTime * 20)).Lerp(transform.position.x, mTargetPosition.x);
                mCurrentCameraPos.y = (1.0f - Mathf.Exp(-Time.deltaTime * 20)).Lerp(transform.position.y, mTargetPosition.y);
                mCurrentCameraPos.z = transform.position.z;
                if (isShake)
                {
                    mShakeFrame--;
                    var ShakeAmplitude = Mathf.Lerp(mShakeAmplitude, 0.0f,mShakeFrame/30.0f);
                    transform.position = new Vector3(mCurrentCameraPos.x + Random.Range(-ShakeAmplitude, ShakeAmplitude),
                        mCurrentCameraPos.y + Random.Range(-ShakeAmplitude, ShakeAmplitude),
                        mCurrentCameraPos.z);
                   
                    if (mShakeFrame <= 0)
                    {
                        isShake = false;
                    }
                }
                else
                {
                    transform.PositionX((1.0f - Mathf.Exp(-Time.deltaTime * 20)).Lerp(transform.position.x, mTargetPosition.x));//平滑，“Mathf.Exp(1.0f - -Time.deltaTime * 20）”为平滑度
                    transform.PositionY((1.0f - Mathf.Exp(-Time.deltaTime * 20)).Lerp(transform.position.y, mTargetPosition.y));
                }
            }
        }

        public static void Shake()
        {
            CameraController.Default.isShake = true;
            Default.mShakeFrame = 30;
            Default.mShakeAmplitude = 2;
        }
    }
}
