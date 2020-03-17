using RPG.Combat;
using RPG.Movement;
using UnityEngine;

namespace RPG.Control
{
    public class PlayerController : MonoBehaviour
    {

        Fighter fighter;

        public void Start()
        {
            fighter = GetComponent<Fighter>();
        }

        private void Update()
        {
            if (InteractWithCombat()) return;
            if (InteractWithMovement()) return;
        }

        private bool InteractWithCombat()
        {
            RaycastHit[] hits = Physics.RaycastAll(GetMouseRay());

            foreach (RaycastHit hit in hits)
            {
                Health target = hit.transform.GetComponent<Health>();

                if (!fighter.CanAttack(target)) continue;

                if (Input.GetMouseButtonDown(0))
                {
                    fighter.Attack(target);
                }

                return true;
            }

            return false;
        }

        private bool InteractWithMovement()
        {
            RaycastHit hit;
            bool hasHit = Physics.Raycast(GetMouseRay(), out hit);

            if (hasHit)
            {

                if (Input.GetMouseButton(0))
                {
                    GetComponent<Mover>().StartMoveAction(hit.point);
                }

                return true;
            }

            return false;
        }

        private static Ray GetMouseRay()
        {
            return Camera.main.ScreenPointToRay(Input.mousePosition);
        }
    }
}