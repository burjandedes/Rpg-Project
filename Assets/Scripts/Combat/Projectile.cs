using RPG.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Combat
{

    public class Projectile : MonoBehaviour
    {

        [SerializeField] float speed = 1f;

        Health target = null;

        float damage = 0;

        // Update is called once per frame
        void Update()
        {
            if (target == null) return;

            transform.LookAt(GetAimLocation());
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }

        private Vector3 GetAimLocation()
        {
            CapsuleCollider targetCapsule = target.GetComponent<CapsuleCollider>();

            if (targetCapsule == null)
            {
                return target.transform.position;
            }

            return target.transform.position + Vector3.up * targetCapsule.height / 2;
        }

        public void SetTarget(Health target, float weaponDamage)
        {
            this.target = target;
            this.damage += weaponDamage;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<Health>() != target) return;

            target.TakeDamage(damage);

            Destroy(gameObject);
        }

    }

}