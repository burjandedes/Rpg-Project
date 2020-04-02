﻿using UnityEngine;
using RPG.Movement;
using RPG.Core;
using System;

namespace RPG.Combat
{
    public class Fighter : MonoBehaviour, IAction
    {
        [SerializeField] float timeBetweenAttacks = 1f;

        [SerializeField] Transform handTransform = null;
        [SerializeField] Weapon weapon = null;
        

        Health target;
        Animator animator;
        Mover mover;

        float timeSinceLastAttack = Mathf.Infinity;

        void Start()
        {
            animator = GetComponent<Animator>();
            mover = GetComponent<Mover>();

            SpawnWeapon();
        }

        private void Update()
        {
            timeSinceLastAttack += Time.deltaTime;

            if (!target) { return; }
            if (target.IsDead()) { return; }

            if (!GetIsInRange())
            {
                mover.MoveTo(target.transform.position, 1f);
            }
            else
            {
                mover.Cancel();
                AttackBehaviour();
            }
        }

        private void SpawnWeapon()
        {
            if (weapon == null) return;

            Animator animator = GetComponent<Animator>();
            weapon.Spawn(handTransform, animator);
        }

        private void AttackBehaviour()
        {
            transform.LookAt(target.transform);

            if (timeSinceLastAttack > timeBetweenAttacks)
            {
                TriggerAttack();
                timeSinceLastAttack = 0;
            }
        }

        private void TriggerAttack()
        {
            animator.ResetTrigger("stopAttack");

            // This will trigger the Hit() event.
            animator.SetTrigger("attack");
        }

        // Animation Event
        void Hit()
        {
            if (!target) { return; }

            target.TakeDamage(weapon.GetWeaponDamage());
        }

        public bool GetIsInRange()
        {
            return Vector3.Distance(transform.position, target.transform.position) < weapon.GetWeaponRange();
        }

        public void Attack(GameObject combatTarget)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            
            target = combatTarget.GetComponent<Health>();
        }

        public bool CanAttack(GameObject combatTarget)
        {
            if (!combatTarget) { return false;  }

            Health targetToTest = combatTarget.GetComponent<Health>();

            return targetToTest && !targetToTest.IsDead();
        }

        public void Cancel()
        {
            StopAttack();
            target = null;
            mover.Cancel();
        }

        private void StopAttack()
        {
            animator.ResetTrigger("attack");
            animator.SetTrigger("stopAttack");
        }

    }
}