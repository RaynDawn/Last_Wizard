using UnityEngine;
using QFramework;
using System.Collections.Generic;

namespace LastWizard
{
	public partial class HealArea : ViewController
	{
		
        
        public int healAmount = 50;
        
        public float healRadius = 3.0f;

        
        private List<GameObject> Enemies = new List<GameObject>();

     
        

        void Start()
        {
            
            GameObject[] allObjects = GameObject.FindGameObjectsWithTag("Enemy"); 
            Enemies.AddRange(allObjects);
        }

        void Update()
        {
        
            if (Input.GetMouseButtonDown(3))
            {
                Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                

                
                var hit = Physics2D.CircleCast(mousePosition, healRadius, Vector2.zero, 0);

                if (hit.collider)
                {
                    
                    Collider2D[] colliders = Physics2D.OverlapCircleAll(mousePosition, healRadius);

                    foreach (Collider2D collider in colliders)
                    {
                        GameObject ally = collider.gameObject;

                      
                        if (ally.TryGetComponent<Health>(out Health healthComponent))
                        {
                            healthComponent.Heal(healAmount);
                        }
                    }
                }
            }
        }
    }

 
    public class Health : MonoBehaviour
    {
        public int currentHealth;
        public int maxHealth = 100;

        public void Heal(int amount)
        {
            currentHealth = Mathf.Min(currentHealth + amount, maxHealth);
            // 可以在这里添加UI更新或其他逻辑
            Debug.Log($"{gameObject.name} healed for {amount} HP. Current HP: {currentHealth}/{maxHealth}");
        }

        public void TakeDamage(int amount)
        {
            currentHealth = Mathf.Max(currentHealth - amount, 0);
            // 可以在这里添加UI更新或死亡逻辑
        }
    }
}
