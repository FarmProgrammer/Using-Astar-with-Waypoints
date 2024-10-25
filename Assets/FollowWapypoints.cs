using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowWapypoints : MonoBehaviour
{
    Transform goal;
    float speed = 5;
    float accuracy = 5;
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
        g = wpManager.GetComponent<WPManager>().graph;
        currentNode = wps[0];
        Invoke("GoToRuin", 2);
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

    public void GoToRandom()
    {
        g.AStar(currentNode, wps[Random.Range(0, wps.Length)]);
        currentWP = 0;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (g.pathList.Count == 0 || currentWP == g.pathList.Count)
            return;

        if (Vector3.Distance(transform.position, g.pathList[currentWP].getID().transform.position) < accuracy)
        {
            currentNode = g.pathList[currentWP].getID();
            currentWP++;
        }

        if (currentWP < g.pathList.Count)
        {
            goal = g.pathList[currentWP].getID().transform;

            Vector3 lookAtGoal = new Vector3(goal.position.x, transform.position.y, goal.position.z);

            Vector3 dir = lookAtGoal - transform.position;

            transform.rotation = Quaternion.Slerp(transform.rotation,
                                                    Quaternion.LookRotation(dir),
                                                    rotspeed * Time.deltaTime);

            transform.Translate(0, 0, speed * Time.deltaTime);
        }
    }
}
