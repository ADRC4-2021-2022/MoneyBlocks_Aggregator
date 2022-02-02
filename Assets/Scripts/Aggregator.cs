using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aggregator : MonoBehaviour
{
    private List<Connection> connections = new List<Connection>();
    private VoxelGrid _grid;
    void Start()
    {
        //Create a voxelgrid

    }

    public void AddFirstBlock()
    {
        //Select a random voxel with Y index = 0
        //Create a new connection with the voxel index
        //TryConnection(RandomVoxel);
    }

    public bool TryConnection(Connection connection)
    {
        //Select a random pattern out of the connections possiblePatterns

        List<Vector3Int> possibleRotations = new List<Vector3Int>(Util.Directions);
        ////Select a random rotation out of possibleRotations
        ////Try to add a block on the connection voxel with the selected pattern in the selected direction
        ///
        /// 
        ////If(Adding block failed)
        //////Remove rotation from possible rotations
        //////if(possibleRotation.count <=0)
        ////////Remove pattenr out of connection.PossiblePatterns
        ////////if(connection.PossiblePatterns.count<=0)
        //////////Remove connection from aggregator connection list
        //////////Return false

        return true;
    }

    public void AddNextBlock()
    {
        //select random available connection out of the grid
        //TryConnection
    }

    //This is a coroutine, watch a tutorial on coroutines to start this function
    public IEnumerator StartGeneration()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.5f);
        }
    }

    public void StartStopGenerating()
    {
        //if there is no block added to the grid
        ////AddFirstBlock()
        //else
        ////StartGeneration() start or stop the coroutine
    }
}
