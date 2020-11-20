using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogGenerator : MonoBehaviour
{
    [SerializeField] private List<GameObject> Logs = new List<GameObject>();
    [SerializeField] private float minGeneratingTime;
    [SerializeField] private float maxGeneratingTime;
    [SerializeField] public static bool isRight;

    private GameObject player;
    private Vector3 startLeftPos;
    private Vector3 startRightPos;
    private GameObject log;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        startLeftPos = new Vector3(transform.position.x, -0.36f, 15);
        startRightPos = new Vector3(transform.position.x, -0.36f, -15);

        //Performs actions every custom time set
        StartCoroutine(generateLog());
    }

    private IEnumerator generateLog()
    {
        while (player != null)
        {
            yield return new WaitForSeconds(UnityEngine.Random.Range(minGeneratingTime, maxGeneratingTime));
            if (isRight)
            {
                log = Instantiate(Logs[UnityEngine.Random.Range(0, Logs.Count)], startRightPos, Quaternion.identity);
            }
            else
            {
                log = Instantiate(Logs[UnityEngine.Random.Range(0, Logs.Count)], startLeftPos, Quaternion.identity);
            }
        }
    }
}
