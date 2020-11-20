using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Log : MonoBehaviour
{
    [SerializeField] private float speed;
    public bool isLog;

    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (startPos.z == 15)
        {
            transform.position = Vector3.MoveTowards(transform.position, transform.position + new Vector3(0, 0, -1), speed * Time.deltaTime);
            RaycastHit hit;
            if (Physics.Raycast(transform.position, Vector3.back, out hit, 5))
            {
                if (hit.collider.transform.tag == "Log")
                {
                    speed = 4;
                }
            }

            if (transform.position.z < -20)
            {
                Destroy(gameObject);
            }
        }
        else if (startPos.z == -15)
        {
            transform.position = Vector3.MoveTowards(transform.position, transform.position + new Vector3(0, 0, 1), speed * Time.deltaTime);
            RaycastHit hit;
            if (Physics.Raycast(transform.position, Vector3.forward, out hit, 5))
            {
                if (hit.collider.transform.tag == "Log")
                {
                    speed = 4;
                }
            }
            
            if (transform.position.z > 20)
            {
                Destroy(gameObject);
            }
        }
    }
}
