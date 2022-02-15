using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using UnityEngine;


/// <summary>
/// Singleton class to manage block patterns in the project
/// </summary>
public class PatternManager
{
    //The pattern manager is a singleton class. This means there is only one instance of the PatternManager class in the entire project and it can be refered to anywhere withing the project

    /// <summary>
    /// Singleton object of the PatternManager class. Refer to this to access the data inside the object.
    /// </summary>
    public static PatternManager Instance { get; } = new PatternManager();

    private static List<Pattern> _patterns;
    public static Dictionary<string, Pattern> _patternsByName;

    /// <summary>
    /// returns a read only list of the patterns defined in the project
    /// </summary>
    public static ReadOnlyCollection<Pattern> Patterns => new ReadOnlyCollection<Pattern>(_patterns);


    /// <summary>
    /// returns a read only dictionary of the patterns defined in the project organised by name
    /// </summary>
    public static ReadOnlyDictionary<string, Pattern> PatternsByName => new ReadOnlyDictionary<string, Pattern>(_patternsByName);

    /// <summary>
    /// private constructor. All initial patterns will be defined in here
    /// </summary>
    private PatternManager()
    {
        _patterns = new List<Pattern>();
        _patternsByName = new Dictionary<string, Pattern>();

        //Define UShape
        AddPattern(
            new List<Vector3Int>() //indices
                {
                    new Vector3Int(0, 0, 0),
                    new Vector3Int(1, 0, 0),
                    new Vector3Int(2, 0, 0),
                    new Vector3Int(0, 1, 0),
                    new Vector3Int(0, 2, 0),
                    new Vector3Int(0, 3, 0),
                    new Vector3Int(2, 1, 0),
                    new Vector3Int(2, 2, 0),
                    new Vector3Int(2, 3, 0),

                },
            new List<Vector3Int>() //Anchorpoints
                {
                   new Vector3Int(1, 0, 0),
                   new Vector3Int(0, 1, 0),
                   new Vector3Int(2, 1, 0),
                   
                },
            new List<Vector3Int>() //Connections
                {
                    new Vector3Int(1, 1, 0),
                    new Vector3Int(1, 2, 0),
                    new Vector3Int(1, 3, 0)
                    
                },
                "UShape"
                );

        //Define all patterns

        //Define H shape 
        AddPattern(
            new List<Vector3Int>() //indices
                {
                    new Vector3Int(0, 0, 0),
                    new Vector3Int(1, 0, 0),
                    new Vector3Int(2, 0, 0),
                    new Vector3Int(3, 0, 0),
                    new Vector3Int(4, 0, 0),
                    new Vector3Int(5, 0, 0),
                    new Vector3Int(6, 0, 0),
                    new Vector3Int(4, 1, 0),
                    new Vector3Int(4, 2, 0),
                    new Vector3Int(0, 2, 0),
                    new Vector3Int(1, 2, 0),
                    new Vector3Int(2, 2, 0),
                    new Vector3Int(3, 2, 0),
                    new Vector3Int(4, 2, 0),
                    new Vector3Int(5, 2, 0),
                    new Vector3Int(6, 2, 0),


                },
            new List<Vector3Int>() //Anchorpoints
                {
                    new Vector3Int(1, 0, 0),
                    new Vector3Int(2, 0, 0),
                    new Vector3Int(4, 0, 0),
                    new Vector3Int(5, 0, 0),
                    new Vector3Int(4, 1, 0),
                    new Vector3Int(1, 2, 0),
                    new Vector3Int(2, 2, 0),
                    new Vector3Int(4, 2, 0),
                    new Vector3Int(5, 2, 0),

                },
            new List<Vector3Int>() //Connections
                {
                    new Vector3Int(0, 2, 0),
                    new Vector3Int(1, 2, 0),
                    new Vector3Int(2, 2, 0),
                    new Vector3Int(4, 2, 0),
                    new Vector3Int(5, 2, 0),
                    new Vector3Int(6, 2, 0)

                },
                "HShape"
                );


        //Define HShape 
        AddPattern(
            new List<Vector3Int>() //indices
                {
                    new Vector3Int(0, 0, 0),
                    new Vector3Int(2, 0, 0),
                    new Vector3Int(0, 1, 0),
                    new Vector3Int(2, 1, 0),
                    new Vector3Int(0, 2, 0),
                    new Vector3Int(2, 2, 0),
                    new Vector3Int(0, 3, 0),
                    new Vector3Int(1, 3, 0),
                    new Vector3Int(2, 3, 0),
                    new Vector3Int(0, 4, 0),
                    new Vector3Int(2, 4, 0),
                    new Vector3Int(0, 5, 0),
                    new Vector3Int(2, 5, 0),
                    new Vector3Int(0, 6, 0),
                    new Vector3Int(2, 6, 0)




                },
            new List<Vector3Int>() //Anchorpoints
                {
                    new Vector3Int(1, 0, 0),
                    new Vector3Int(1, 1, 0),
                    new Vector3Int(1, 2, 0),
                    new Vector3Int(1, 4, 0),
                    new Vector3Int(1, 5, 0),
                    new Vector3Int(1, 6, 0)

                },
            new List<Vector3Int>() //Connections
                {
                    new Vector3Int(0, 2, 0),
                    new Vector3Int(1, 2, 0),
                    new Vector3Int(2, 2, 0),
                    new Vector3Int(1, 3, 0),
                    new Vector3Int(0, 4, 0),
                    new Vector3Int(2, 4, 0)

                },
                "HShape"
                );


        //Define O shape 
        AddPattern(
            new List<Vector3Int>() //indices
                {
                    new Vector3Int(0, 0, 0),
                    new Vector3Int(1, 0, 0),
                    new Vector3Int(2, 0, 0),
                    new Vector3Int(3, 0, 0),
                    new Vector3Int(0, 1, 0),
                    new Vector3Int(3, 1, 0),
                    new Vector3Int(0, 2, 0),
                    new Vector3Int(1, 2, 0),
                    new Vector3Int(2, 2, 0),
                    new Vector3Int(3, 2, 0)
                },
            new List<Vector3Int>() //Anchorpoints
                {
                    new Vector3Int(1, 0, 0),
                    new Vector3Int(0, 1, 0),
                    new Vector3Int(1, 2, 0),
                    new Vector3Int(3, 1, 0),
                    new Vector3Int(2, 0, 0),
                    new Vector3Int(2, 2, 0)
                },
            new List<Vector3Int>() //Connections
                {
                    new Vector3Int(1, 1, 0),
                    new Vector3Int(2, 1, 0)
                },
                "OShape"
                );

        //Define S shape 
        AddPattern(
            new List<Vector3Int>() //indices
                {
                    new Vector3Int(0, 0, 0),
                    new Vector3Int(1, 0, 0),
                    new Vector3Int(2, 0, 0),
                    new Vector3Int(3, 0, 0),
                    new Vector3Int(0, 1, 0),
                    new Vector3Int(0, 2, 0),
                    new Vector3Int(1, 2, 0),
                    new Vector3Int(2, 2, 0),
                    new Vector3Int(3, 2, 0),
                    new Vector3Int(3, 3, 0),
                    new Vector3Int(3, 4, 0),
                    new Vector3Int(2, 4, 0),
                    new Vector3Int(1, 4, 0),
                    new Vector3Int(0, 4, 0)

                },
            new List<Vector3Int>() //Anchorpoints
                {
                    new Vector3Int(1, 0, 0),
                    new Vector3Int(0, 1, 0),
                    new Vector3Int(1, 2, 0),
                    new Vector3Int(3, 3, 0)

                },
            new List<Vector3Int>() //Connections
                {
                    new Vector3Int(1, 1, 0),
                    new Vector3Int(2, 1, 0),
                    new Vector3Int(2, 2, 0),
                    new Vector3Int(0, 3, 0),
                    new Vector3Int(1, 3, 0),
                    new Vector3Int(2, 3, 0)
                },
                "SShape"
                );
        //Define Hook shape 
        AddPattern(
            new List<Vector3Int>() //indices
                {
                    new Vector3Int(0, 0, 0),
                    new Vector3Int(0, 1, 0),
                    new Vector3Int(0, 2, 0),
                    new Vector3Int(0, 3, 0),
                    new Vector3Int(0, 4, 0),
                    new Vector3Int(0, 5, 0),
                    new Vector3Int(1, 0, 0),                 
                    new Vector3Int(1, 2, 0),
                    new Vector3Int(1, 3, 0),
                    new Vector3Int(3, 0, 0),               
                    new Vector3Int(3, 2, 0),
                    new Vector3Int(3, 3, 0),
                    new Vector3Int(3, 4, 0),
                    new Vector3Int(3, 5, 0)

                },
            new List<Vector3Int>() //Anchorpoints
                {
                    new Vector3Int(0, 1, 0),                  
                    new Vector3Int(1, 0, 0),
                    new Vector3Int(1, 2, 0),
                    new Vector3Int(1, 3, 0),
                    new Vector3Int(0, 4, 0),
                    new Vector3Int(2, 4, 0)

                },
            new List<Vector3Int>() //Connections
                {
                    new Vector3Int(1, 1, 0),
                    new Vector3Int(2, 1, 0),
                    new Vector3Int(1, 4, 0),
                    new Vector3Int(1, 5, 0),
                    new Vector3Int(3, 4, 0)
                    
                },

                "HookShape"
               
                );


        //Define 6shape 
        AddPattern(
            new List<Vector3Int>() //indices
                {
                    new Vector3Int(0, 0, 0),
                    new Vector3Int(1, 0, 0),
                    new Vector3Int(2, 0, 0),
                    new Vector3Int(3, 0, 0),
                    new Vector3Int(0, 1, 0),
                    new Vector3Int(3, 1, 0),
                    new Vector3Int(0, 2, 0),
                    new Vector3Int(1, 2, 0),
                    new Vector3Int(2, 2, 0),
                    new Vector3Int(3, 2, 0),
                    new Vector3Int(0, 3, 0),
                    new Vector3Int(0, 4, 0),
                    new Vector3Int(1, 4, 0),
                    new Vector3Int(2, 4, 0),
                    new Vector3Int(3, 4, 0)


                },
            new List<Vector3Int>() //Anchorpoints
                {
                    new Vector3Int(0, 1, 0),
                    new Vector3Int(3, 1, 0),
                    new Vector3Int(0, 3, 0)

                },
            new List<Vector3Int>() //Connections
                {
                    new Vector3Int(1, 1, 0),
                    new Vector3Int(2, 1, 0),
                    new Vector3Int(1, 3, 0),
                    new Vector3Int(2, 3, 0),
                    new Vector3Int(3, 3, 0),
                    new Vector3Int(1, 5, 0),
                    new Vector3Int(2, -1, 0)

                },

                "6Shape"

                );
        //Define  LongHookshape 
        AddPattern(
            new List<Vector3Int>() //indices
                {
                    new Vector3Int(0, 0, 0),
                    new Vector3Int(1, 0, 0),
                    new Vector3Int(2, 0, 0),                   
                    new Vector3Int(3, 0, 0),
                    new Vector3Int(4, 0, 0),
                    new Vector3Int(5, 0, 0),
                    new Vector3Int(6, 0, 0),
                    new Vector3Int(7, 0, 0),
                    new Vector3Int(8, 0, 0),
                    new Vector3Int(9, 0, 0),
                    new Vector3Int(14, 0, 0),
                    new Vector3Int(15, 0, 0),
                    new Vector3Int(0, 1, 0),
                    new Vector3Int(2, 1, 0),
                    new Vector3Int(3, 1, 0),
                    new Vector3Int(4, 1, 0),
                    new Vector3Int(5, 1, 0),
                    new Vector3Int(6, 1, 0),
                    new Vector3Int(7, 1, 0),
                    new Vector3Int(8, 1, 0),
                    new Vector3Int(9, 1, 0),
                    new Vector3Int(10, 1, 0),
                    new Vector3Int(11, 1, 0),
                    new Vector3Int(12, 1, 0),
                    new Vector3Int(13, 1, 0),
                    new Vector3Int(14, 1, 0),
                    new Vector3Int(15, 1, 0),
                   new Vector3Int(0, 2, 0),
                    new Vector3Int(2, 2, 0),
                    new Vector3Int(3, 2, 0),
                    new Vector3Int(4, 2, 0),
                    new Vector3Int(5, 2, 0),
                    new Vector3Int(6, 2, 0),
                    new Vector3Int(7, 2, 0),
                    new Vector3Int(8, 2, 0),
                    new Vector3Int(9, 2, 0),
                    new Vector3Int(10, 2, 0),
                    new Vector3Int(11, 2, 0),
                    new Vector3Int(12, 2, 0),
                    new Vector3Int(13, 2, 0),
                    new Vector3Int(14, 2, 0),
                    new Vector3Int(15, 2, 0)


                },
            new List<Vector3Int>() //Anchorpoints
                {
                    new Vector3Int(9, 0, 0),
                    new Vector3Int(1, 0, 0),
                    new Vector3Int(0, 1, 0),
                    new Vector3Int(2, 1, 0),
                    new Vector3Int(10, 1, 0),
                    new Vector3Int(0, 2, 0),
                    new Vector3Int(2, 2, 0)
                    


                },
            new List<Vector3Int>() //Connections
                {
                    
                    new Vector3Int(10, 0, 0),
                    new Vector3Int(11, 0, 0),
                    new Vector3Int(12, 0, 0),
                    new Vector3Int(13, 0, 0),
                    new Vector3Int(1, 1, 0),
                    new Vector3Int(1, 2, 0)

                },
                "LongHookShape"
              

        );
        //Define LongHshape 
        AddPattern(
            new List<Vector3Int>() //indices
                {
                    new Vector3Int(0, 0, 0),
                    new Vector3Int(1, 0, 0),
                    new Vector3Int(2, 0, 0),
                    new Vector3Int(3, 0, 0),
                    new Vector3Int(4, 0, 0),
                    new Vector3Int(5, 0, 0),
                    new Vector3Int(6, 0, 0),
                    new Vector3Int(7, 0, 0),
                    new Vector3Int(8, 0, 0),
                    new Vector3Int(9, 0, 0),
                    new Vector3Int(10, 0, 0),
                    new Vector3Int(3, 1, 0),
                    new Vector3Int(7, 1, 0),
                    new Vector3Int(0, 2, 0),
                    new Vector3Int(1, 2, 0),
                    new Vector3Int(2, 2, 0),
                    new Vector3Int(3, 2, 0),
                    new Vector3Int(4, 2, 0),
                    new Vector3Int(5, 2, 0),
                    new Vector3Int(6, 2, 0),
                    new Vector3Int(7, 2, 0),
                    new Vector3Int(8, 2, 0),
                    new Vector3Int(9, 2, 0),
                    new Vector3Int(10, 2, 0),

                },
            new List<Vector3Int>() //Anchorpoints
                {
                    new Vector3Int(8, 0, 0),
                    new Vector3Int(9, 0, 0),
                    new Vector3Int(7, 1, 0),
                    new Vector3Int(2, 2, 0),
                    new Vector3Int(8, 2, 0),
                    new Vector3Int(9, 2, 0)

                },
            new List<Vector3Int>() //Connections
                {
                    new Vector3Int(0, 1, 0),
                    new Vector3Int(1, 1, 0),
                    new Vector3Int(2, 1, 0),
                    new Vector3Int(4, 1, 0),
                    new Vector3Int(5, 1, 0),
                    new Vector3Int(6, 1, 0),
                    new Vector3Int(8, 1, 0),
                    new Vector3Int(9, 1, 0),
                    new Vector3Int(10, 1, 0)


                },

                "LongHShape"
            );


    }
    /// <summary>
    /// Use this method rather than adding directly to the _patterns field. This method will check if the pattern is valid and can be added to the list. Invalid input will be refused.
    /// </summary>
    /// <param name="indices">List of indices that define the patter. The indices should always relate to Vector3In(0,0,0) as anchor point</param>
    /// <param name="type">The PatternType of this pattern to add. Each type can only exist once</param>
    /// <returns></returns>
    public bool AddPattern(List<Vector3Int> indices, List<Vector3Int> anchorPoints, List<Vector3Int> connections, string name)
    {

        //only add valid patterns
        if (indices == null) return false;
        if (indices[0] != Vector3Int.zero) return false;
        if (_patterns.Count(p => p.Name == name) > 0) return false;

        foreach (var anchor in anchorPoints)
        {
            GeneratePatterns(indices, anchor, connections, name);
        }

        return true;
    }

    public void GeneratePatterns(List<Vector3Int> indices, Vector3Int anchorPoint, List<Vector3Int> connections, string name)
    {
        List<Vector3Int> newIndices = new List<Vector3Int>();
        List<Vector3Int> newConnections = new List<Vector3Int>();
        string newName = name + anchorPoint.ToString();
        foreach (var index in indices)
        {
            newIndices.Add(index - anchorPoint);
        }
        foreach (var connection in connections)
        {
            newConnections.Add(connection - anchorPoint);
        }

        GameObject goPrefab = null;
        //Load the prefab out of resources
        //Create a copy
        //Move the mesh child object of the prefab according to the anchorpoint
        //==> position = position - anchorpoint * voxelsize

        _patterns.Add(new Pattern(newIndices, newConnections, _patterns.Count, newName, goPrefab));
        _patternsByName.Add(newName, _patterns.Last());
    }

    /// <summary>
    /// Return the pattern linked to its index
    /// </summary>
    /// <param name="index">The index to look for</param>
    /// <returns>The pattern linked to the type. Will return null if the type is never defined</returns>
    public static Pattern GetPatternByIndex(int index) => Patterns[(int)index];

    public static Pattern GetPatternByName(string name) => PatternsByName[name];
}
/// <summary>
/// The pattern that defines a block. Object of this class should only be made in the PatternManager
/// </summary>
public class Pattern
{
    public GameObject GOPrefab { get; }
    /// <summary>
    /// The patterns are saved as ReadOnlyCollections rather than list so that once defined, the pattern can never be changed
    /// </summary>
    public ReadOnlyCollection<Vector3Int> Indices { get; }

    //List of indices of connection voxels where new block can be added to
    public ReadOnlyCollection<Vector3Int> Connections { get; }
    public int Index { get; }

    public string Name { get; }

    /// <summary>
    /// Pattern constructor. The indices will be stored in a ReadOnlyCollection
    /// </summary>
    ///<param name = "indices" > List of indices that define the patter.The indices should always relate to Vector3In(0,0,0) as anchor point</param>
    /// <param name="type">The PatternType of this pattern to add. Each type can only exist once</param>
    public Pattern(List<Vector3Int> indices, List<Vector3Int> connections, int index, string name, GameObject goPrefab)
    {
        Indices = new ReadOnlyCollection<Vector3Int>(indices);
        Connections = new ReadOnlyCollection<Vector3Int>(connections);
        Index = index;
        Name = name;
        GOPrefab = goPrefab;

    }
}

