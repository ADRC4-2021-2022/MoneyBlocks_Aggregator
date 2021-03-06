using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Aggregator : MonoBehaviour
{
    private int _seed = 102;

    private List<Connection> _connections = new List<Connection>();
    private VoxelGrid _grid;
    private List<Voxel> _nonDeadVoxels;
    List<Voxel> _targets = new List<Voxel>();
    public Texture2D[] _sourceImage;
    private float _voxelSize = 0.3f;
    private int _voxelOffset = 1;
    private int _triesPerIteration = 10000;
    private int _iterations = 1000;
    private int _tryCounter = 0;
    private int _iterationCounter = 0;

    public List<Voxel> NonDeadVoxels
    {
        get
        {        
                if (_nonDeadVoxels == null)
                {
                    _nonDeadVoxels = _grid.GetVoxels().Where(v => v.Status != VoxelState.Dead).ToList();
                }          
            return _nonDeadVoxels;
        }
    }

   
    private PatternCreator _patternCreator
    {
        get
        {
            return GameObject.Find("PatternCreator").GetComponent<PatternCreator>();
        }
    }

    public void SetVoxelGridVoid()
    {
        GameObject[] voxels = GameObject.FindGameObjectsWithTag("Voxel");
        //Material voidMaterial = Resources.Load<Material>("Pix2PixMaterials/Void");
        foreach (GameObject item in voxels)
        {
            //item.GetComponent<MeshRenderer>().material = voidMaterial;
            item.SetActive(false);
        }
    }

    public void DestroyTagVoidVoxel()
    {
        GameObject[] voxels = GameObject.FindGameObjectsWithTag("VoidVoxel");
        foreach (GameObject item in voxels)
        {
            Destroy(item);
        }
    }

    public void DestroyTagVoxel()
    {
        GameObject[] voxels = GameObject.FindGameObjectsWithTag("Voxel");
        foreach (GameObject item in voxels)
        {
            Destroy(item);
        }
    }

    public void ReadAllImage()
    {    
        for (int i = 0; i < _sourceImage.Length; i++)
        {
            if (i==3)
            {
                height = 4;
                Util.IndexPerFunction.Remove(FunctionColour.Black);
                Util.IndexPerFunction.Add(FunctionColour.Black, height);
            }
            _grid = new VoxelGrid(_sourceImage[i], 3, height, location, voxelScale);
            _grid.SetStatesFromImageReduced(_sourceImage[i]);
            location.y += height * voxelScale;
            DestroyTagVoidVoxel();
        }    
    }


    public static int height = 14;
    float voxelScale = 0.3f;
    Vector3 location;
    int floorIndex = 0;
    public void GeniusGenerate()
    {           
        IsTopFloor();
        _grid = new VoxelGrid(_sourceImage[floorIndex], 3, height, location, voxelScale);
        _grid.SetStatesFromImageReduced(_sourceImage[0]);
        location.y += height * voxelScale;
        floorIndex += 1;
        ButtonGenerate();
    }

    public void IsTopFloor()
    {
        if (floorIndex == 3)
        {
            height = 4;
            Util.IndexPerFunction.Remove(FunctionColour.Black);
            Util.IndexPerFunction.Add(FunctionColour.Black, height);
        }
    }

    public void test111()
    {
        Debug.Log("111");
    }


    public void ButtonGenerate()
    {
        _patternCreator.CreatePatterns();
        AddFirstBlock();
        for (int i = 0; i < 5000; i++)
        {
            GenerationStep();
        }
        
        DestroyTagVoidVoxel();
        DestroyTagVoxel();
        //var remainingVoxels = _grid.GetVoxels().Count(v => v.Status == VoxelState.Available);
        //Debug.Log(remainingVoxels);
    }

    void Start()
    {

        location.x = location.y = location.z = 0;

        Random.InitState(_seed);
        // generate using the first seed
        // count how many voxels are still VoxelStat.Availble
        //var remainingVoxels = _grid.GetVoxels().Count(v => v.Status == VoxelState.Available);
        // store the seed and result in a list or dictionary
        // repeat for a diffrent seed.
        // find the seed that had the best result
        // use that seed to generate the final result

    }

    /// <summary>
    /// Set the status of all voxels dead inside or outside of the mesh
    /// </summary>
    /// <param name="checkInside">if true, the voxels outside the mesh will be dead.
    /// If false, the voxels inside of the mesh will be dead.</param>
    private void KillVoxelsInOutBounds(bool checkInside)
    {
        for (int i = 0; i < _sourceImage.Length; i++)
        {
            foreach (Voxel voxel in _grid.GetVoxels())
            {
                bool isInside = BoundingMesh.IsInsideCentre(voxel);
                if (checkInside && !isInside)
                    voxel.Status = VoxelState.Dead;
                if (!checkInside && isInside)
                    voxel.Status = VoxelState.Dead;
            }
            Debug.Log($"Number of available voxels: {_grid.GetVoxels().Count(v => v.Status == VoxelState.Available)} of {_grid.GetVoxels().Count()} voxels");
        }
    }

    public void AddFirstBlock()
    {
        //Select a random voxel with Y index = 0
        int rndIndex = Random.Range(0, NonDeadVoxels.Count);
        Vector3Int randomVoxel = NonDeadVoxels[rndIndex].Index;
        //Create a new connection with the voxel index
        Connection connectionZero = new Connection(randomVoxel, _grid);
        TryConnection(connectionZero);
    }

    //Add the next block to the aggregation. Run this in a coroutine to automate the generation
    //Google coroutine
    public void GenerationStep()
    {
        //Find all available connections
        List<Connection> availableConnections = _grid.GetAvailableConnections();
        if (availableConnections.Count <= 0)
        {
            Debug.Log($"no available connections");
        }
        else
        {
            //Select a random available connection
            int rndConnectionIndex = Random.Range(0, availableConnections.Count);
            Connection selectedConnection = availableConnections[rndConnectionIndex];
            TryConnection(selectedConnection);
        }
    }

    public bool TryConnection(Connection connection)
    {
        List<Pattern> possiblePatterns = connection.PossiblePatterns;
        //Debug.Log(possiblePatterns.Count);
        //If we have found a pattern, this boolean will be true
        bool patternSet = false;
        int patternTries = 0;

        //Try all patterns until one is found
        while (possiblePatterns.Count > 0 && !patternSet && patternTries < PatternManager.Patterns.Count)
        {
            List<Vector3Int> possibleDirections = new List<Vector3Int>(Util.Directions);
            int directionTries = 0;

            //Get a random pattern out of the possiblePatterns
            int patternIndex = Random.Range(0, possiblePatterns.Count);
            Pattern selectedPattern = possiblePatterns[patternIndex];

            //try all directions untill one is found
            while (possibleDirections.Count > 0 && !patternSet && directionTries < Util.Directions.Count)
            {
                int rndDirectionIndex = Random.Range(0, possibleDirections.Count);
                Vector3Int randomDirection = possibleDirections[rndDirectionIndex];
                //Set the random pattern as current grid pattern
                _grid.SetPatternIndex(selectedPattern.Index);

                Quaternion xRotation = Quaternion.Euler(new Vector3(90, 0, 0));

                //Try to place a block based on the pattern with the anchorpoint on the connection point
                _grid.AddBlock(connection.Index, Quaternion.Euler(randomDirection * 90)/**xRotation*/);

                //See if the block can be added
                if (!_grid.TryAddCurrentBlocksToGrid())
                {
                    //Remove the added block from the grid
                    _grid.PurgeUnplacedBlocks();

                    //remove the random direction that we have tried
                    possibleDirections.Remove(randomDirection);

                    //If the direction we have tried was the last one, remove the random pattern
                    if (possibleDirections.Count == 0)
                    {
                        possiblePatterns.Remove(selectedPattern);
                    }

                }
                else
                {
                    //A pattern is succesfully added to the grid
                    //Clear all the possible connections, make sure the while loop stops and stop the function
                    connection.PossiblePatterns = new List<Pattern>();
                    patternSet = true;
                    return true;
                }
                directionTries++;
            }
            patternTries++;
        }
        return false;
    }

    /// <summary>
    /// Set the voxels outside of the mesh to not alive
    /// </summary>
    public void VoxeliseMesh()
    {
        KillVoxelsInOutBounds(true);
    }


}
