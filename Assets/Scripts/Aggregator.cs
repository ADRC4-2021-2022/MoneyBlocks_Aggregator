using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aggregator : MonoBehaviour
{
    private List<Connection> connections = new List<Connection>();
    private VoxelGrid _grid;
    void Start()
    {
        _grid = new VoxelGrid(50,100,50,10f,Vector3.zero);//the voxel grid does not show up

        //Find the GameObject

        GameObject obj = GameObject.Find("UShap");

        //Get script attached to it
        //attached in unity
        //Call the function
        //which function
    }

    public void AddFirstBlock()
         
    {
        int x = 3;
        int z = 15;
        Vector3Int randomVoxel = new Vector3Int(x,0,z);//Select a random voxel with Y index = 0
        Connection _connectionZero;
        _connectionZero = new Connection(randomVoxel, _grid );//Create a new connection with the voxel index
        TryConnection(_connectionZero);//TryConnection(RandomVoxel)
       
    }

    public bool TryConnection(Connection connection)
    {
        new Vector3Int(2, 1, 0); //Select a random pattern out of the connections possiblePatterns

        List<Vector3Int> possibleRotations = new List<Vector3Int>(Util.Directions);

        new Vector3Int(0, -1, 0);////Select a random rotation out of possibleRotations
        GameObject obj = GameObject.Find("UShap"); ////Try to add a block on the connection voxel with the selected pattern in the selected direction
        obj.transform.position = new Vector3Int(3,0,15);
        obj.transform.rotation = new Quaternion. ;
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
