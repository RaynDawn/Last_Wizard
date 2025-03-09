using UnityEngine;
using QFramework;

namespace LastWizard
{
	public partial class EnemySpawner : ViewController
	{
		void Start()
		{
			// Code Here

		}

        public GameObject enemyPrefab1; 
        public GameObject enemyPrefab2;
        public GameObject enemyPrefab3;

        void Update()
        {
          
            if (Input.GetMouseButtonDown(0))
            {
                if(Global.Anger.Value < 10)
                {
                    return;
                }
                Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                mousePosition.z = 0; 
                Instantiate(enemyPrefab1, mousePosition, Quaternion.identity);
                Global.Anger.Value -=10;
            }

            if (Input.GetMouseButtonDown(1))
            {
               if (Global.Anger.Value < 10)
                {
                    return;
                }
                Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                mousePosition.z = 0; 
                Instantiate(enemyPrefab2, mousePosition, Quaternion.identity);
                Global.Anger.Value -= 10;
            }

            if(Input.GetMouseButtonDown(2))
                {
                Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                mousePosition.z = 0;
                Instantiate(enemyPrefab3, mousePosition, Quaternion.identity);
                Global.Anger.Value -= 20;
            }

            if(Input.GetKeyDown(KeyCode.C))
            {
                Global.Anger.Value += 10;
            }
        }
    }
}
