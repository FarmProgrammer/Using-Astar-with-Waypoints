using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Graph
{
    List<Edge> edges = new List<Edge>();
    List<Node> nodes = new List<Node>();
    public List<Node> pathList = new List<Node>();

    public Graph()
    {

    }

    public void AddNode(GameObject id)
    {
        Node node = new Node(id);
        nodes.Add(node);
    }

    public void AddEdge(GameObject fromNode, GameObject toNode)
    {
        Node from = FindNode(fromNode);
        Node to = FindNode(toNode);
        if(from != null && to != null)
        {
            Edge edge = new Edge(from, to);
            edges.Add(edge);
            from.edgeList.Add(edge);
        }
    }

    Node FindNode(GameObject id)
    {
        foreach (Node node in nodes)
        {
            if (node.getID() == id)
            {
                return node;
            }
        }
        return null;
    }

    public bool AStar(GameObject startID, GameObject endID)
    {
        if(startID == endID)
        {
            pathList.Clear();
            return false;
        }
        Node start = FindNode(startID);
        Node end = FindNode(endID);

        if(start ==null || end == null)
        {
            return false;
        }

        List<Node> open = new List<Node>();
        List<Node> closed = new List<Node>();
        float tentativeGScore = 0;
        bool tentativeIsbetter;

        start.g = 0;
        start.h = distance(start, end);
        start.f = start.h;

        open.Add(start);
        while (open.Count > 0)
        {
            int i = lowestF(open);
            Node thisNode = open[i];
            if(thisNode.getID() == endID)
            {
                ReconstructPath(start, end);
                return true;
            }

            open.RemoveAt(i);
            closed.Add(thisNode);
            Node neighbour;
            foreach(Edge e in thisNode.edgeList)
            {
                neighbour = e.endNode;
                if (closed.IndexOf(neighbour) > -1)
                    continue;

                tentativeGScore = thisNode.g + distance(thisNode, neighbour);

                if (open.IndexOf(neighbour) == -1)
                {
                    open.Add(neighbour);
                    tentativeIsbetter = true;
                }
                else if (tentativeGScore < neighbour.g)
                {
                    tentativeIsbetter = true;
                }
                else
                    tentativeIsbetter = false;
                neighbour.cameFrom = thisNode;
                neighbour.g = tentativeGScore;
                neighbour.h = distance(thisNode, end);
                neighbour.f = neighbour.g + neighbour.h;
            }
        }
        return false;
    }

    public void ReconstructPath(Node startId, Node endId)
    {
        pathList.Clear();
        pathList.Add(endId);

        var p = endId.cameFrom;

        while(p!=startId && p != null)
        {
            pathList.Insert(0, p);
            p = p.cameFrom;
        }

    }

    float distance(Node a, Node b)
    {
        return (Vector3.SqrMagnitude(a.getID().transform.position - b.getID().transform.position));
    }

    int lowestF(List<Node> l)
    {
        float lowestF = 0;
        int count = 0;
        int iteratorCount = 0;

        lowestF = l[0].f;

        for (int i = 1; i < l.Count; i++)
        {
            if (l[i].f < lowestF)
            {
                lowestF = l[i].f;
                iteratorCount = count;
            }
            count++;
        }

        return iteratorCount;
    }
}
