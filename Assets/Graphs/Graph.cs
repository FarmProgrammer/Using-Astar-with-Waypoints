using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Graph
{
    List<Edge> edges = new List<Edge>();
    List<Node> nodes = new List<Node>();
    List<Node> pathList = new List<Node>();

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
}
