using UnityEngine;
using RPG.Movement;
using RPG.Core;

namespace RPG.Combat
{
    public class Fighter : MonoBehaviour, IAction
    {
        [SerializeField] float weaponRange = 2f;
        [SerializeField] float weaponDamage = 5f;
        [SerializeField] float timeBetweenAttacks = 1f;

        Health target;
        Animator animator;
        Mover mover;

        float timeSinceLastAttack = 0;

        void Start()
        {
            animator = GetComponent<Animator>();
            mover = GetComponent<Mover>();
        }

        private void Update()
        {
            timeSinceLastAttack += Time.deltaTime;

            if (!target) return;
            if (target.IsDead()) return;

            if (!GetIsInRange())
            {
                mover.MoveTo(target.transform.position);
            }
            else
            {
                mover.Cancel();
                AttackBehaviour();
            }
        }

        private void AttackBehaviour()
        {
            transform.LookAt(target.transform);

            if (timeSinceLastAttack > timeBetweenAttacks)
            {
                // This will trigger the Hit() event.
                animator.SetTrigger("attack");
                timeSinceLastAttack = 0;
            }
        }

        // Animation Event
        void Hit()
        {
            target.TakeDamage(weaponDamage);
        }

        public bool GetIsInRange()
        {
            return Vector3.Distance(transform.position, target.transform.position) < weaponRange;
        }

        public void Attack(Health combatTarget)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            target = combatTarget;
        }

        public void Cancel()
        {
            animator.SetTrigger("stopAttack");
            target = null;
        }

        public bool CanAttack(Health target)
        {
            return target && !target.IsDead();
        }

    }
}