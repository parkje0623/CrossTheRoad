using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vehicle : MonoBehaviour
{
    [SerializeField] private float speed;

    // Update is called once per frame
    private void Update()
    {
        if (transform.rotation.eulerAngles.y == 90)
        {
            transform.position = Vector3.MoveTowards(transform.position, transform.position + new Vector3(0, 0, 1), speed * Time.deltaTime);
            RaycastHit hit;
            if (Physics.Raycast(transform.position, Vector3.forward, out hit, 5))
            {
                if (hit.collider.gameObject.tag == "Vehicle")
                {
                    speed = 3;
                }
            }

            if (transform.position.z > 20)
            {
                Destroy(gameObject);
            }
        }
        else if (transform.rotation.eulerAngles.y == 270)
        {
            transform.position = Vector3.MoveTowards(transform.position, transform.position + new Vector3(0, 0, -1), speed * Time.deltaTime);
            RaycastHit hit;
            if (Physics.Raycast(transform.position, Vector3.back, out hit, 5))
            {
                if (hit.collider.gameObject.tag == "Vehicle")
                {
                    speed = 3;
                }     
            }

            if (transform.position.z < -20)
            {
                Destroy(gameObject);
            }
        }
    }
}
