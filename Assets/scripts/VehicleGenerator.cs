using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleGenerator : MonoBehaviour
{
    [SerializeField] private List<GameObject> vehicles = new List<GameObject>();
    [SerializeField] private float minGeneratingTime;
    [SerializeField] private float maxGeneratingTime;
    [SerializeField] private bool isRight;

    private GameObject player;
    private Vector3 startLeftPos;
    private Vector3 startRightPos;
    private GameObject vehicle;

    // Start is called before the first frame update
    private void Start()
    {
        player = GameObject.Find("Player");
        startLeftPos = new Vector3(transform.position.x, 0.65f, 15);
        startRightPos = new Vector3(transform.position.x, 0.65f, -15);

        //Performs actions every custom time set
        StartCoroutine(generateVehicle());
    }

    private IEnumerator generateVehicle()
    {
        while(player != null)
        {
            //Every 2 seconds, performs an action
            yield return new WaitForSeconds(UnityEngine.Random.Range(minGeneratingTime, maxGeneratingTime));
            if (isRight)
            {
                vehicle = Instantiate(vehicles[UnityEngine.Random.Range(0, vehicles.Count)], startRightPos, Quaternion.identity);
                vehicle.transform.Rotate(0, 90, 0);
            }
            else
            {
                vehicle = Instantiate(vehicles[UnityEngine.Random.Range(0, vehicles.Count)], startLeftPos, Quaternion.identity);
                vehicle.transform.Rotate(0, 270, 0);
            }
        }
    }
}
