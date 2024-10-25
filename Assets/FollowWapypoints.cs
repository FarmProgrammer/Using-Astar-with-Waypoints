using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowWapypoints : MonoBehaviour
{
    Transform goal;
    float speed = 5;
    float accuracy = 1;
    float rotspeed = 2;

    public GameObject wpManager;
    GameObject[] wps;
    GameObject currentNode;
    int currentWP = 0;
    Graph g;

    // Start is called before the first frame update
    void Start()
    {
        wps = wpManager.GetComponent<WPManager>().waypoints;
        g = wpManager.GetComponent<Graph>();
        currentNode = wps[0];
    }

    public void GoToHeli()
    {
        g.AStar(currentNode, wps[0]);
        currentWP = 0;
    }

    public void GoToRuin()
    {
        g.AStar(currentNode, wps[12]);
        currentWP = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
