using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManageShapes : MonoBehaviour
{
    private Mesh _mesh;
    public GameObject blueSphere;
    public GameObject[] limbs; // Assign all the limbs (don't have to assign small limbs like fingers & toes)
    public GameObject[] baseLimbs; // Assign base limbs (just legs, arms and head)
    private Vector3[] _vertices;
    private void Awake()
    {
        _mesh = new Mesh();
        _mesh = GetComponent<MeshFilter>().mesh;
        GetComponent<SkinnedMeshRenderer>().BakeMesh(_mesh);

        InitializeStickMan();
        CreateStickMan();
    }

    private void InitializeStickMan()
    {
        _vertices = new Vector3[_mesh.vertices.Length];
        _vertices = _mesh.vertices;
    }

    private void CreateStickMan()
    {
        var a = 0;
        var spawnedPositions = new List<Vector3>();

        for(int i = 0; i< _vertices.Length; i++)
        {
            a++;
            if (a / 10 == 1) // Only instantiating sphere on 10% of the vertices to gain performance in runtime.
            {
                a = 0;
                if (!spawnedPositions.Contains(_vertices[i])) // I'm blocking any chance of overlapping to gain performance in runtime.
                {
                    GameObject spawnedSphere = Instantiate(blueSphere, _vertices[i], Quaternion.identity);

                    spawnedSphere.transform.parent = FindClosestTransform(spawnedSphere.transform.position, limbs);

                    spawnedPositions.Add(_vertices[i]);
                }
            }
        }

        GameObject.Find("Alpha_Surface").SetActive(false);
        GameObject.Find("Alpha_Joints").SetActive(false);
    }
    private Transform FindClosestTransform(Vector3 spawnedSpherePosition, GameObject[] array)
    {
        // Distance between the first limb and spawned object is assigned to shortestDistance variable. Also, transform of the first limb is assigned to transformHolder.
        // If there's a limb closer to the first limb, the algorithm will find it and replaces this variable with new ones.

        var shortestDistance = Vector3.Distance(array[0].transform.position, spawnedSpherePosition);
        var transformHolder = array[0].transform;

        for (int i = 0; i < array.Length; i++)
        {
            var currentDistance = Vector3.Distance(array[i].transform.position, spawnedSpherePosition);
            if (currentDistance < shortestDistance)
            {
                shortestDistance = currentDistance;
                transformHolder = array[i].transform;
            }
        }
        return transformHolder;
    }
}