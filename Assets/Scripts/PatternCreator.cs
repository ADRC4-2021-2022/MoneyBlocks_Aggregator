using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;



public class PatternCreator : MonoBehaviour
{
    //Please create your regions
    #region Private fields
    [SerializeField]
    private float _voxelSize = 0.09f;


    List<GameObject> _componentPrefabs;

    #endregion


    // Start is called before the first frame update
    void Start()
    {
        //Load all the prefabs out of the resources
        GameObject[] prefabs = (GameObject[])Resources.LoadAll(@"Prefabs");

        //Load all the component prefabs in the list
        _componentPrefabs = prefabs.Where(g => g.tag == "Component").ToList();

        //Loop over all your prefabs and run AddPattern()
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
        ////VoxelGrid grid = new VoxelGrid()
        //add all the voxelsindices with the centre inside the component collider to the indices list

        //Get all the children of the component gameobject with the tag anchorpoints in a list
        ////Loop over all the anchorpoints,
        //////Take the anchorpoint position and divide by your voxelsize, rounded to a Vector3Int
        //////Add the new vector3Int to the list of anchorpoint

        //Get all the children of the component gameobject with the tag connections in a list
        ////Loop over all the connections,
        //////Take the connection position and divide by your voxelsize, rounded to a Vector3Int
        //////Add the new vector3Int to the list of connection

        //Add the new paterns for the component
        PatternManager.Instance.AddPattern(indices, anchorpoints, connections, name);
    }
}
