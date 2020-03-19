using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Control
{

    public class PatrolPath : MonoBehaviour
    {

        const float waypointGizmoRadius = 0.1f;

        private void OnDrawGizmos()
        {
            int numberOfChildren = transform.childCount;

            for (int i = 0; i < numberOfChildren; ++i)
            {
                Transform child = transform.GetChild(i);
                Transform previousChild;

                if (i == 0)
                {
                    previousChild = transform.GetChild(numberOfChildren - 1);
                }
                else
                {
                    previousChild = transform.GetChild(i - 1);
                }

                Vector3 childPosition = child.position;
                Vector3 previousChildPosition = previousChild.position;

                Gizmos.DrawLine(childPosition, previousChildPosition);
                Gizmos.DrawSphere(childPosition, waypointGizmoRadius);
            }
        }

    }

}