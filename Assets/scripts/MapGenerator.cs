using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    [SerializeField] private int maxTerrains;
    [SerializeField] private List<GameObject> terrains = new List<GameObject>();
    [SerializeField] private List<GameObject> trees = new List<GameObject>();
    [SerializeField] private List<GameObject> stars = new List<GameObject>();

    private Vector3 currentPosition = new Vector3(0, 0, 0);
    private Vector3 treePosition;
    private List<GameObject> currentTerrains = new List<GameObject>();
    private GameObject terrain;
    private List<GameObject> currentTrees = new List<GameObject>();
    private List<int> trackTreeAmount = new List<int>();
    private List<float> trackTreePos = new List<float>();
    private GameObject star;
    private GameObject tree;
    private float randomTreePos;
    private int randomTreeAmount;
    private int xPosition = 0;
    private GameObject player;
    public static GameObject restartButton;
    public static GameObject inputName;
    public static bool isHighScore;
    //private GameObject quitButton;
    public static int keepHighScore;

    private void Start()
    {
        player = GameObject.Find("Player");
        restartButton = GameObject.Find("Restart");
        inputName = GameObject.Find("InputField");
        
        //quitButton = GameObject.Find("Quit");
        restartButton.SetActive(false);
        inputName.SetActive(false);
        //quitButton.SetActive(false);
        for (int i = 0; i < maxTerrains; i++)
        {
            createNewRoads();
        }
        isHighScore = false;

    }

    private void Update()
    {
        if (!KillPlayer.alive)
        {
            restartButton.SetActive(true);
            if (Player.trackScore >= PlayerPrefs.GetInt("HighestScore"))
            {
                inputName.SetActive(true);
                keepHighScore = Player.trackScore;
                isHighScore = true;
            }
            //quitButton.SetActive(true);
        }

        player = GameObject.Find("Player");
        if (player != null)
        {
            if (currentPosition.x - player.transform.position.x < 17)
            {
                createNewRoads();
            }
            else if (currentPosition.x - player.transform.position.x < 15)
            {
                deleteRoad();
            }
        }
    }

    private void createNewRoads()
    {
        if (currentTerrains.Count < 4)
        {
            terrain = Instantiate(terrains[0], currentPosition, Quaternion.identity);
        }
        else
        {
            terrain = Instantiate(terrains[UnityEngine.Random.Range(0, terrains.Count)], currentPosition, Quaternion.identity);
        }
        terrain.name = terrain.name.Replace("(Clone)", "");
        currentTerrains.Add(terrain);

        randomTreeAmount = UnityEngine.Random.Range(1, 10);
        if (terrain.name == "sidewalk")
        {
            for (int j = 0; j < randomTreeAmount; j++)
            {
                do
                {
                    randomTreePos = UnityEngine.Random.Range(-19, 19);
                } while (trackTreePos.Contains(randomTreePos));
                trackTreePos.Add(randomTreePos);

                treePosition = new Vector3(xPosition, 0.5f, randomTreePos);
                if (treePosition.z != player.transform.position.z)
                {   
                    tree = Instantiate(trees[UnityEngine.Random.Range(0, trees.Count)], treePosition, Quaternion.identity);
                }
                currentTrees.Add(tree);
            }
            trackTreeAmount.Add(trackTreePos.Count);
            trackTreePos.Clear();
        }

        if (UnityEngine.Random.Range(0, 10) == 7)
        {
            if (terrain.name == "Road")
            {
                randomTreePos = UnityEngine.Random.Range(3, -5);
                treePosition = new Vector3(xPosition, 1f, randomTreePos);
                star = Instantiate(stars[0], treePosition, Quaternion.identity);
                star.transform.rotation = Quaternion.AngleAxis(-90, Vector3.right);
            }
        }

        currentPosition.x++;
        xPosition++;
    }

    private void deleteRoad()
    {
        if (currentTerrains[0].name == "sidewalk")
        {
            for (int i = 0; i < trackTreeAmount[0]; i++)
            {
                Destroy(currentTrees[0]);
                currentTrees.RemoveAt(0);
            }
            trackTreeAmount.RemoveAt(0);
        }

        Destroy(currentTerrains[0]);
        currentTerrains.RemoveAt(0);
    }
}