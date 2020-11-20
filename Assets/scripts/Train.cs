using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Train : MonoBehaviour
{
    [SerializeField] private float speed;

    private Vector3 startPos;
    private GameObject player;

    void Start()
    {
        startPos = transform.position;
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, transform.position + new Vector3(0, 0, -1), speed * Time.deltaTime);
        if (transform.position.z < -30)
        {
            Destroy(gameObject);
        }
    }
}
