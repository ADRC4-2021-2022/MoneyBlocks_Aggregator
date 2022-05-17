using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System;


public class PatternCreator : MonoBehaviour
{
    //Please create your regions
    #region Private fields
    [SerializeField]
    private float _voxelSize = 0.09f;

    private Vector3Int _dimensions;
    private Vector3 _origin;

    private VoxelGrid _grid;

    List<GameObject> _componentPrefabs;




    #endregion


    
    public void CreatePatterns()
    {
        //Load all the prefabs out of the resources  .get and make them to gameobject
        //GameObject[] prefabs = (GameObject[])Resources.LoadAll(@"Prefabs");

        //Load all the component prefabs in the list
        //_componentPrefabs = prefabs.Where(g => g.tag == "Component").ToList();

        GameObject[] goPrefabs = Resources.LoadAll<GameObject>("Prefabs/Parts");


        //Comment the following two lines after adding all the colliders and uncomment the next block of code
        /*GameObject goComponent0 = GameObject.Instantiate(goPrefabs[0]);
        AddPattern(goComponent0);
        GameObject.Destroy(goComponent0);

        GameObject goComponent1 = GameObject.Instantiate(goPrefabs[1]);
        AddPattern(goComponent1);
        GameObject.Destroy(goComponent1);

        GameObject goComponent2 = GameObject.Instantiate(goPrefabs[2]);
        AddPattern(goComponent2);
        GameObject.Destroy(goComponent2);

        GameObject goComponent3 = GameObject.Instantiate(goPrefabs[3]);
        AddPattern(goComponent3);
        GameObject.Destroy(goComponent3);
        */







        //Loop over all your prefabs and run AddPattern()
        
        foreach (GameObject goPrefab in goPrefabs)
        {
            GameObject goComponent = GameObject.Instantiate(goPrefab);
            AddPattern(goComponent);
            GameObject.Destroy(goComponent);
        }
    }


    /*private void Awake()
    {
        CreatePatterns();
    }*/

    private void AddPattern(GameObject goComponent)
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

        

        ////Get dimensions of your component + connections/ voxelsize
        Bounds partBounds = new Bounds();
        partBounds.Encapsulate(partCollider.bounds);

        GameObject[] goConnections = Util.GetChildrenWithTag(goComponent.transform, "Connection").ToArray();
        foreach (var connection in goConnections)
        {
            partBounds.Encapsulate(connection.GetComponent<Collider>().bounds);
        }

        Vector3Int gridDimensions = (partBounds.size / _voxelSize).ToVector3IntRound();

        ////Create you voxelgrid using the Voxelsize, Origin and dimensions
        if(_grid != null)
        {
            foreach (var vox in _grid.GetVoxels())
            {
                vox.DestroyGameObject();
            }
        }
        _grid = new VoxelGrid(gridDimensions, _voxelSize, partBounds.min);

        //add all the voxelsindices with the centre inside the component collider to the indices list
        foreach (Voxel vox in _grid.GetVoxels())
        {
            if (Util.PointInsideCollider(vox.Centre, partCollider))
            {
                indices.Add(vox.Index);
            }
        }

        //Get all the children of the component gameobject with the tag anchorpoints in a list
        ////Loop over all the anchorpoints,
        //////Take the anchorpoint position and divide by your voxelsize, rounded to a Vector3Int
        //////Add the new vector3Int to the list of anchorpoint
        GameObject[] goAnchorpoints = Util.GetChildrenWithTag(goComponent.transform, "AnchorPoint").ToArray();

        foreach (GameObject goAnchorpoint in goAnchorpoints)
        {
            foreach (var vox in _grid.GetVoxels())
            {
                //Check if the centrepoint of the voxel is in almost the same position as the transform position of the anchorpoint gameobject
                if (Vector3.Distance(goAnchorpoint.transform.position, vox.Centre) < _voxelSize / 2)
                {
                    anchorpoints.Add(vox.Index);
                }
            }
        }

        //Get all the children of the component gameobject with the tag connections in a list
        ////Loop over all the connections,
        //////Take the connection position and divide by your voxelsize, rounded to a Vector3Int
        //////Add the new vector3Int to the list of connection
        

        foreach (GameObject goConnection in goConnections)
        {
            foreach (var vox in _grid.GetVoxels())
            {
                //Check if the centrepoint of the voxel is in almost the same position as the transform position of the anchorpoint gameobject
                if (Vector3.Distance(goConnection.transform.position, vox.Centre) < _voxelSize / 2)
                {
                    connections.Add(vox.Index);
                }
            }
        }

        //Add the new paterns for the component
        PatternManager.Instance.AddPattern(indices, anchorpoints, connections, name, goComponent,_voxelSize);
    }
}
