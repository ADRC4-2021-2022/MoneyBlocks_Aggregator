using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Aggregator : MonoBehaviour
{
    private List<Connection> _connections = new List<Connection>();
    private VoxelGrid _grid;
    //public Coroutine coroutine;
    private List<Voxel> _nonDeadVoxels;

    private float _voxelSize = 0.09f;
    private int _voxelOffset = 1;

    private int _triesPerIteration = 10000;
    private int _iterations = 1000;

    private int _tryCounter = 0;
    private int _iterationCounter = 0;

    private PatternCreator _patternCreator
    {
        get
        {
            return GameObject.Find("PatternCreator").GetComponent<PatternCreator>();
        }
    }


    void Start()
    {
        //Random.InitState(66);
        _patternCreator.CreatePatterns();

       // Invoke("StopRun", 10f);
        //_grid = new VoxelGrid(20, 20, 20, 0.095f, Vector3.zero);
        _grid = BoundingMesh.GetVoxelGrid(_voxelOffset, _voxelSize);
        KillVoxelsInOutBounds(true);

        //Find the GameObject

        //GameObject obj = GameObject.Find("UShap");

        //Get script attached to it
        //attached in unity
        //Call the function
        //which function
        AddFirstBlock();
        for (int i = 0; i < 1000; i++)
        {
            GenerationStep();
        }

    }
    public void Update()
    {
        //int track = 0;

        //while (track < 1000)
        //{
        //    track++;
        //    GenerationStep();

        //}
    }

    /// <summary>
    /// Set the status of all voxels dead inside or outside of the mesh
    /// </summary>
    /// <param name="checkInside">if true, the voxels outside the mesh will be dead.
    /// If false, the voxels inside of the mesh will be dead.</param>
    private void KillVoxelsInOutBounds(bool checkInside)
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
        _nonDeadVoxels = _grid.GetVoxels().Where(v => v.Status != VoxelState.Dead).ToList();
    }

    public void AddFirstBlock()
    {

        //Select a random voxel with Y index = 0
        int rndIndex = Random.Range(0, _nonDeadVoxels.Count);

        Vector3Int randomVoxel = _nonDeadVoxels[rndIndex].Index;
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
            //return;
            
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

                //Try to place a block based on the pattern with the anchorpoint on the connection point
                _grid.AddBlock(connection.Index, Quaternion.Euler(randomDirection * 90));

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




        //obj.transform.rotation = new Quaternion. ;
        ///
        /// 
        ////If(Adding block failed)
        //////Remove rotation from possible rotations
        //////if(possibleRotation.count <=0)
        ////////Remove pattenr out of connection.PossiblePatterns
        ////////if(connection.PossiblePatterns.count<=0)
        //////////Remove connection from aggregator connection list
        //////////Return false

        return false;
    }

    public void AddNextBlock()
    {
        //select random available connection out of the grid
        //TryConnection
    }

    //This is a coroutine, watch a tutorial on coroutines to start this function


    public void StartStopGenerating()
    {
        //if there is no block added to the grid
        ////AddFirstBlock()
        //else
        ////StartGeneration() start or stop the coroutine
    }

    /*public void CreateVoxelGrid()
    {
        _grid = BoundingMesh.GetVoxelGrid(_voxelOffset, _voxelSize);
    }*/


    /// <summary>
    /// Set the voxels outside of the mesh to not alive
    /// </summary>
    public void VoxeliseMesh()
    {
        KillVoxelsInOutBounds(true);
    }


}
