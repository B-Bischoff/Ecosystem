using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshManager : MonoBehaviour
{
    public NavMeshSurface surface;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            BakeNavMesh();
        }
    }

    public void BakeNavMesh()
    {
        surface.BuildNavMesh();
        Debug.Log("NavMesh Surface Rebaked !");
    }
}
