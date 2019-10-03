using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RunTimeBuild : MonoBehaviour
{
    public NavMeshSurface surface;

    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        surface.BuildNavMesh();
        player.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
