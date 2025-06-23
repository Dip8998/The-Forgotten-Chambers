using UnityEngine;

namespace ForgottonChambers.HealthSystem
{
    public class HealthReducer : MonoBehaviour, IHealth
    {
        [SerializeField] private int _maxHealth = 100;
        private int _currentHealth;

        private void Awake()
        {
            _currentHealth = _maxHealth;
        }

        public void TakeDamage(int health)
        {
            _currentHealth -= health;
            if(_currentHealth < 0 )
            {
                Die();
            }
        }

        private void Die()
        {
            Destroy(gameObject);
        }
    }
}
