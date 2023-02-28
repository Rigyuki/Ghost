using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;

public class ChasePlayer : MonoBehaviour
{
    public Transform target;
    public List<NavMeshSurface> naveMeshSurfaces = new List<NavMeshSurface>();
    [Range(1, 10)] public int updatePerXFrames = 5;
    int temp = 0;
    NavMeshAgent agent;
    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }
    void Update()
    {
        if (temp % updatePerXFrames != 0)
        {
            ++temp;
            return;
        }
        temp = 1;
        foreach(NavMeshSurface surface in naveMeshSurfaces)
        {
            surface.BuildNavMesh();
        }
        agent.SetDestination(target.position);
    }
}
