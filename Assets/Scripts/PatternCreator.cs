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
    
    public Vector3Int Dimensions;
    private Vector3 Origin;

    List<GameObject> _componentPrefabs;

    


    #endregion


    // Start is called before the first frame update
    void Start()
    {
        //Load all the prefabs out of the resources  .get and make them to gameobject
        GameObject[] prefabs = (GameObject[])Resources.LoadAll(@"Prefabs");

        //Load all the component prefabs in the list
        _componentPrefabs = prefabs.Where(g => g.tag == "Component").ToList();

        //Loop over all your prefabs and run AddPattern()

        /*foreach (var item in List)
          {

          }*/
    }

    private void AddPattern(GameObject component)
    {
        

        //Declare all the patern variables
        string name = component.name;
        List<Vector3Int> indices = new List<Vector3Int>();
        List<Vector3Int> anchorpoints = new List<Vector3Int>();
        List<Vector3Int> connections = new List<Vector3Int>();

        //Create a voxelgrid using the given voxelsize with the dimensions of the component
        ////Get origin of your component
        ////Get dimensions of your component/ voxelsize
        ////Create you voxelgrid using the Voxelsize, Origin and dimensions
        //VoxelGrid grid = new VoxelGrid()
        //add all the voxelsindices with the centre inside the component collider to the indices list

        //Get all the children of the component gameobject with the tag anchorpoints in a list
        ////Loop over all the anchorpoints,
        //////Take the anchorpoint position and divide by your voxelsize, rounded to a Vector3Int
        //////Add the new vector3Int to the list of anchorpoint
        GameObject.FindGameObjectsWithTag("AnchorPoint");

        List<GameObject> Anchorpoints = new List<GameObject>();

        foreach (var item in anchorpoints)
        {
            
            this.GetComponent<Transform>().position = new Vector3Int();
           new Vector3Int().Add(anchorpoints);

        }

        //Get all the children of the component gameobject with the tag connections in a list
        ////Loop over all the connections,
        //////Take the connection position and divide by your voxelsize, rounded to a Vector3Int
        //////Add the new vector3Int to the list of connection
        GameObject.FindGameObjectsWithTag("Connection");

        List<GameObject> Connection = new List<GameObject>();

        foreach (var item in anchorpoints)
        {

            this.GetComponent<Transform>().position = new Vector3Int();
            new Vector3Int().Add(connections);

        }

        //Add the new paterns for the component
        PatternManager.Instance.AddPattern(indices, anchorpoints, connections, name);
    }

    public void voxelgrid(Vector3Int dimentions, Vector3 origin, float voxelSize, GameObject Prefabs)
    {
        
        Dimensions = dimentions;
        Origin = origin;
        _voxelSize = voxelSize;


       /*VoxelGrid grid = new VoxelGrid[Dimensions.x, Dimensions.y, Dimensions.z]
        

        for (int x = 0; x < Dimensions.x; x++)
        {
            for (int y = 0; y < Dimensions.y; y++)
            {
                for (int z = 0; z < Dimensions.z; z++)
                {
                    // 13 Use Voxel constructors
                    Voxels[x, y, z] = new Voxel( new Vector3Int(x, y, z),this,Prefabs,0.95f);
                }
            }
        }*/
    }




}
