using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeviceOperator : MonoBehaviour
{
    [SerializeField] private float radius = 1.5f;
    private Collider[] hitColliders;

    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            hitColliders = Physics.OverlapSphere(transform.position, radius);

            foreach (Collider hitCollider in hitColliders)
            {
                Vector3 direction = hitCollider.transform.position - transform.position;

                if (Vector3.Dot(transform.forward, direction) > 0.5f)
                {
                    hitCollider.SendMessage("Operate", SendMessageOptions.DontRequireReceiver);
                }
            }
        }
    }
}
