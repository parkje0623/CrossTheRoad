using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainGenerator : MonoBehaviour
{
    [SerializeField] private List<GameObject> trains = new List<GameObject>();
    [SerializeField] private float minGeneratingTime;
    [SerializeField] private float maxGeneratingTime;
    
    private GameObject train;
    private Vector3 startLeftPos;
    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        startLeftPos = new Vector3(transform.position.x, 0.25f, 100);
        StartCoroutine(generateLog());
    }

    private IEnumerator generateLog()
    {
        while (player != null)
        {
            yield return new WaitForSeconds(UnityEngine.Random.Range(minGeneratingTime, maxGeneratingTime));
            train = Instantiate(trains[0], startLeftPos, Quaternion.identity);
        }
    }
}
