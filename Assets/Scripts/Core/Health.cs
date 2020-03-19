using UnityEngine;

namespace RPG.Core
{

    public class Health : MonoBehaviour
    {

        [SerializeField] float health = 100;

        bool isDead = false;

        public bool IsDead()
        {
            return isDead;
        }

        public void TakeDamage(float damage)
        {
            health -= damage;
            
            if (health <= 0)
            {
                Die();
            }
        }

        private void Die()
        {
            if (isDead) return;

            isDead = true;
            GetComponent<Animator>().SetTrigger("die");
            GetComponent<ActionScheduler>().CancelCurrentAction();
        }
    }

}

