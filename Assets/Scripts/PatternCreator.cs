using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
//using System;


public class PatternCreator : MonoBehaviour
{
    public GameObject[] newPrefabs;
    public Toggle active00;
    public Toggle active01;
    public Toggle active02;
    public Toggle active03;
    public Toggle active04;

    //Please create your regions
    #region Private fields
    [SerializeField]
    private readonly float _voxelSize = 0.3f;

    //private Vector3Int _dimensions;
    //private Vector3 _origin;

    //private VoxelGrid _grid;

    //List<GameObject> _componentPrefabs;




    #endregion


    public void IsActive()
    {
        if (active00.isOn)
        {
            newPrefabs[0].SetActive(true);
        }

        if (active00.isOn == false)
        {
            newPrefabs[0].SetActive(false);
        }

        if (active01.isOn)
        {
            newPrefabs[1].SetActive(true);
        }

        if (active01.isOn == false)
        {
            newPrefabs[1].SetActive(false);
        }

        if (active02.isOn)
        {
            newPrefabs[2].SetActive(true);
        }

        if (active02.isOn == false)
        {
            newPrefabs[2].SetActive(false);
        }

        if (active03.isOn)
        {
            newPrefabs[3].SetActive(true);
        }

        if (active03.isOn == false)
        {
            newPrefabs[3].SetActive(false);
        }

        if (active04.isOn)
        {
            newPrefabs[4].SetActive(true);
        }

        if (active04.isOn == false)
        {
            newPrefabs[4].SetActive(false);
        }

    }

    


    public void NewCreatePatterns()
    {
        int count = 0;
        for (int i = 0; i < newPrefabs.Count(); i++)
        {
            
                var goPrefab = newPrefabs[i];
                GameObject goComponent = GameObject.Instantiate(goPrefab);
                AddPattern(goComponent, count++);
                GameObject.Destroy(goComponent);
            
            
        }
    }

    public void CreatePatterns()
    {
        //Load all the prefabs out of the resources  .get and make them to gameobject
        //GameObject[] prefabs = (GameObject[])Resources.LoadAll(@"Prefabs");

        //Load all the component prefabs in the list
        //_componentPrefabs = prefabs.Where(g => g.tag == "Component").ToList();

        GameObject[] goPrefabs = Resources.LoadAll<GameObject>("Prefabs/Parts");

        //var parent = GameObject.Find("Prefabs").transform;
        //for (int i = 0; i < parent.childCount; i++)
        //{
        //    var goComponent = parent.GetChild(i).gameObject;
        //    //ameObject goComponent = GameObject.Instantiate(goPrefab);
        //    AddPattern(goComponent);
        //    GameObject.Destroy(goComponent);
        //}


        //Loop over all your prefabs and run AddPattern()


        //int count = 0;
        //GameObject goComponent = GameObject.Instantiate(goPrefabs[6]);
        //AddPattern(goComponent, count++);
        //GameObject.Destroy(goComponent);


        int count = 0;
        for (int i = 0; i < goPrefabs.Count(); i++)
        {
            var goPrefab = goPrefabs[i];
            GameObject goComponent = GameObject.Instantiate(goPrefab);
            AddPattern(goComponent, count++);
            GameObject.Destroy(goComponent);
        }




    }

    private void AddPattern(GameObject goComponent, int count)
    {
        //Declare all the patern variables
        string name = goComponent.name;
        List<Vector3Int> indices = new List<Vector3Int>();
        List<Vector3Int> anchorpoints = new List<Vector3Int>();
        List<Vector3Int> connections = new List<Vector3Int>();

        //Create a voxelgrid using the given voxelsize with the dimensions and origin of the component
        GameObject goPartMesh = Util.GetChildrenWithTag(goComponent.transform, "ComponentMesh").First();
        //GameObject goPartMesh = Util.GetChildrenWithTag(goComponent.transform, transform.localPosition, "ComponentMesh").First();
        if (goPartMesh == null)
        {
            Debug.Log($"Mesh gameobject {name} collider not found");
            return;
        }
        MeshCollider partCollider = goPartMesh.GetComponent<MeshCollider>();

        var displacement = new Vector3(0, count * 10, 0);
        goComponent.transform.Translate(displacement);

        ////Get dimensions of your component + connections/ voxelsize
        Bounds partBounds = new Bounds();
        partBounds.Encapsulate(partCollider.bounds);

        GameObject[] goConnections = Util.GetChildrenWithTag(goComponent.transform, "Connection").ToArray();
        foreach (var connection in goConnections)
        {
            partBounds.Encapsulate(connection.GetComponent<Collider>().bounds);
        }

        Vector3Int gridDimensions = (partBounds.size / _voxelSize).ToVector3IntCeil();

        ////Create you voxelgrid using the Voxelsize, Origin and dimensions
        //if(_grid != null)
        //{
        //    foreach (var vox in _grid.GetVoxels())
        //    {
        //        vox.DestroyGameObject();
        //    }
        //}
        //_grid = new VoxelGrid(gridDimensions, _voxelSize, partBounds.min);
        //var grid = new VoxelGrid(gridDimensions, _voxelSize, partBounds.min);
        Vector3[,,] vecGrid = new Vector3[gridDimensions.x, gridDimensions.y, gridDimensions.z];
        for (int x = 0; x < gridDimensions.x; x++)
        {
            for (int y = 0; y < gridDimensions.y; y++)
            {
                for (int z = 0; z < gridDimensions.z; z++)
                {
                    vecGrid[x, y, z] = displacement + partBounds.min + new Vector3(x, y, z) * _voxelSize + Vector3.one * 0.5f * _voxelSize;
                }
            }
        }

        //add all the voxelsindices with the centre inside the component collider to the indices list
        for (int x = 0; x < gridDimensions.x; x++)
        {
            for (int y = 0; y < gridDimensions.y; y++)
            {
                for (int z = 0; z < gridDimensions.z; z++)
                {
                    var pos = vecGrid[x, y, z];
                    var index = new Vector3Int(x, y, z);
                    if (Util.PointInsideCollider(pos, partCollider))
                    {
                        indices.Add(index);
                    }
                }
            }
        }

        //foreach (Voxel vox in vecGrid.GetVoxels())
        //{
        //    if (Util.PointInsideCollider(vox.Centre, partCollider))
        //    {
        //        indices.Add(vox.Index);
        //    }
        //}

        //Get all the children of the component gameobject with the tag anchorpoints in a list
        ////Loop over all the anchorpoints,
        //////Take the anchorpoint position and divide by your voxelsize, rounded to a Vector3Int
        //////Add the new vector3Int to the list of anchorpoint
        GameObject[] goAnchorpoints = Util.GetChildrenWithTag(goComponent.transform, "AnchorPoint").ToArray();

        foreach (GameObject goAnchorpoint in goAnchorpoints)
        {
            for (int x = 0; x < gridDimensions.x; x++)
            {
                for (int y = 0; y < gridDimensions.y; y++)
                {
                    for (int z = 0; z < gridDimensions.z; z++)
                    {
                        var pos = vecGrid[x, y, z];
                        var index = new Vector3Int(x, y, z);
                        if (Vector3.Distance(goAnchorpoint.transform.position, pos) < _voxelSize / 2)
                        {
                            anchorpoints.Add(index);
                        }
                    }
                }
            }

            //foreach (var vox in grid.GetVoxels())
            //{
            //    //Check if the centrepoint of the voxel is in almost the same position as the transform position of the anchorpoint gameobject
            //    if (Vector3.Distance(goAnchorpoint.transform.position, vox.Centre) < _voxelSize / 2)
            //    {
            //        anchorpoints.Add(vox.Index);
            //    }
            //}
        }

        //Get all the children of the component gameobject with the tag connections in a list
        ////Loop over all the connections,
        //////Take the connection position and divide by your voxelsize, rounded to a Vector3Int
        //////Add the new vector3Int to the list of connection


        foreach (GameObject goConnection in goConnections)
        {
            for (int x = 0; x < gridDimensions.x; x++)
            {
                for (int y = 0; y < gridDimensions.y; y++)
                {
                    for (int z = 0; z < gridDimensions.z; z++)
                    {
                        var pos = vecGrid[x, y, z];
                        var index = new Vector3Int(x, y, z);
                        if (Vector3.Distance(goConnection.transform.position, pos) < _voxelSize / 2)
                        {
                            connections.Add(index);
                        }
                    }
                }
            }

            //foreach (var vox in grid.GetVoxels())
            //{
            //    //Check if the centrepoint of the voxel is in almost the same position as the transform position of the anchorpoint gameobject
            //    if (Vector3.Distance(goConnection.transform.position, vox.Centre) < _voxelSize / 2)
            //    {
            //        connections.Add(vox.Index);
            //    }
            //}
        }

        Debug.Log($"{name} {indices.Count} {anchorpoints.Count} {connections.Count}");
        //Add the new paterns for the component
        PatternManager.AddPattern(indices, anchorpoints, connections, $"{name}-{Random.Range(0, 1000)}", goComponent, _voxelSize);
        //PatternManager.AddPattern(indices, anchorpoints, connections, name, goComponent, _voxelSize);

    }
}
