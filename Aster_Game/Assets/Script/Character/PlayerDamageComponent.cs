using UnityEngine;
using AG.PlayerComponent.Interfaces;

namespace AG.PlayerComponent
{
    public class PlayerDamageComponent : MonoBehaviour, IDamageable
    {
        private int maxHealth;
        private int currentHealth;
        private bool isDead;
        private bool isWinner;

        public void ApplyDamage(int damage)
        {
            currentHealth = Mathf.Clamp(currentHealth - damage, 0, maxHealth);

            if(currentHealth <= 0)
            {
                Die();
            }
        }

        public void Die()
        {
            isDead = true;
        }

        public bool Dead()
        {
            return isDead;
        }

        public bool Win()
        {
            // TODO : should make Game mode and manage this logic
            return isWinner;
        }
    }
}