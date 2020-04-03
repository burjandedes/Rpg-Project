using RPG.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Combat
{

    [CreateAssetMenu(fileName ="Weapon", menuName ="Weapons/Make New Weapon", order = 0)]
    public class Weapon : ScriptableObject
    {
        [SerializeField] GameObject equippedPrefab = null;
        [SerializeField] AnimatorOverrideController animatorOverride = null;
        [SerializeField] Projectile projectile = null;

        [SerializeField] float weaponRange = 2f;
        [SerializeField] float weaponDamage = 10f;
        [SerializeField] bool isRightHanded = true;

        public void Spawn(Transform rightHand, Transform leftHand, Animator animator)
        {
            if (equippedPrefab != null)
            {
                Instantiate(equippedPrefab, GetTransform(rightHand, leftHand));
            }

            if (animatorOverride != null)
            {
                animator.runtimeAnimatorController = animatorOverride;
            }
        }

        private Transform GetTransform(Transform rightHand, Transform leftHand)
        {
            return isRightHanded ? rightHand : leftHand;
        }

        public bool HasProjectile()
        {
            return projectile != null;
        }

        public void LaunchProjectile(Transform rightHand, Transform leftHand, Health target)
        {
            Projectile projectileInstance = Instantiate(
                projectile, 
                GetTransform(rightHand, leftHand).position, 
                Quaternion.identity);

            projectileInstance.SetTarget(target, weaponDamage);
        }

        public float GetWeaponRange()
        {
            return weaponRange;
        }

        public float GetWeaponDamage()
        {
            return weaponDamage;
        }
    }
}