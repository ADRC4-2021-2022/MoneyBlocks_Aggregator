using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using QuickGraph;
using QuickGraph.Algorithms;

public class EnvironmentManager : MonoBehaviour
{
    #region Private fields

    Pix2PixVoxelGrid _voxelGrid;
    TextureHSVColor _voxelGrid2;
    //08 Create image variable
    Texture2D _sourceImage;
    Texture2D _sourceImage123;
    Texture2D _sourceImageCopy;
    //44 Create list to stores target voxels
    List<Pix2PixVoxel> _targets = new List<Pix2PixVoxel>();

    #endregion

    #region Unity methods

    public void Start()
    {
        
    }

    public void Update()
    {
        //46 Cast ray clicking mouse
        if (Input.GetMouseButtonDown(0))
        {
            SetClickedAsTarget();
        }
    }

    #endregion

    #region Public methods


    public void LoadImage()
    {

        //01 Create a basic Pix2PixVoxelGrid
        //_voxelGrid = new Pix2PixVoxelGrid(new Vector3Int(50, 5, 25), transform.position, 1f);

        //09 Read Image from resources
        _sourceImage = Resources.Load<Texture2D>("Data/1234");
        _sourceImageCopy = duplicateTexture(_sourceImage);






        //SourceField
        //10 Create grid from image
        _voxelGrid = new Pix2PixVoxelGrid(_sourceImage, 3, 1, transform.position, 0.3f);

        //30 Read image and set states of GraphVoxels
        //_voxelGrid.SetStatesFromImage(_sourceImage);


    }










    //37 Create public method to read image from button
    /// <summary>
    /// Read the image and set the states of the Voxels
    /// </summary>
    public void ReadImage()
    {
        _voxelGrid.SetStageFromImageReduced(_sourceImage);
        _targets = new List<Pix2PixVoxel>();
    }

    public void ReadImage123()
    {
        _voxelGrid.SetStageFromImageReduced(_sourceImage123);
        _targets = new List<Pix2PixVoxel>();
    }



    //38 Create public method to change the visibility of the void voxels
    /// <summary>
    /// Change the visibility of the void GraphVoxels
    /// </summary>
    public void VoidsVisibility()
    {
        /*
        //39 Iterate through every voxel
        foreach (Pix2PixVoxel voxel in _voxelGrid.Voxels)
        {
            //40 FIRST Check if voxel is active and not not an obstacle
            //59 SECOND Also check if voxel isn't target before toggling visibility
            //83 THIRD Also check if voxel isn't part of path
            if (voxel.IsActive && !voxel.IsObstacle && !voxel.IsTarget && !voxel.IsPath)
            {
                //42 Implement the Toggle Visibility method
                voxel.ToggleVisibility();
            }
        }
       */
        
        GameObject[] voidVoxels = GameObject.FindGameObjectsWithTag("VoidVoxel");
        foreach (GameObject item in voidVoxels)
        {
            item.SetActive(false);
        }
        
    }

    //60 Create the method to calculate the shortest path
    public void FindShortestPath()
    {
        //61 Create a list to store all faces of the graph in the grid
        List<Face> faces = new List<Face>();

        //62 Iterate through all the faces in the grid
        foreach (var face in _voxelGrid.GetFaces())
        {
            //63 Get the voxels associated with this face
            Pix2PixVoxel voxelA = (Pix2PixVoxel)face.Voxels[0];
            Pix2PixVoxel voxelB = (Pix2PixVoxel)face.Voxels[1];

            //64 Check if both voxels exist, are not obstacle and are active
            if (voxelA != null && !voxelA.IsObstacle && voxelA.IsActive &&
                voxelB != null && !voxelB.IsObstacle && voxelB.IsActive)
            {
                //65 Add face to list
                faces.Add(face);
            }
        }

        //66 Create the edges from the graph using the faces (the voxels are the vertices)
        var graphEdges = faces.Select(f => new TaggedEdge<Pix2PixVoxel, Face>(f.Voxels[0], f.Voxels[1], f));

        //67 Create the undirected graph from the edges
        var graph = graphEdges.ToUndirectedGraph<Pix2PixVoxel, TaggedEdge<Pix2PixVoxel, Face>>();

        //68 Iterate through all the targets, starting at index 1
        for (int i = 1; i < _targets.Count; i++)
        {
            //69 Define the start vertex of this path
            var start = _targets[i - 1];

            //70 Set next target as the end of the path
            var end = _targets[i];

            //71 Construct the Shortest Path graph, unweighted
            var shortest = graph.ShortestPathsDijkstra(e => 1.0, start);

            //72 Calculate the shortest path, if such one is possible
            if (shortest(end, out var endPath))
            {
                //73 Read the path as a list of GraphVoxels
                var endPathVoxels = new List<Pix2PixVoxel>(endPath.SelectMany(e => new[] { e.Source, e.Target }));

                //74 Set each GraphVoxel as path
                foreach (var pathVoxel in endPathVoxels)
                {
                    //79 Set as path
                    pathVoxel.SetAsPath();
                }
            }
            //80 Throw exception if path could not be found
            else
            {
                throw new System.Exception($"No Path could be found!");
            }

        }
    }

    //83 Create public method to start coroutine
    /// <summary>
    /// Start the animation of the Shortest path algorithm
    /// </summary>
    public void StartAnimation()
    {
        StartCoroutine(FindShortestPathAnimated());
    }

    #endregion

    #region Private methods

    //45 Create the method to set clicked voxel as target
    /// <summary>
    /// Cast a ray where the mouse pointer is, turning the selected voxel into a target
    /// </summary>
    private void SetClickedAsTarget()
    {
        //47 Cast ray from camera
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        //48 If ray hits something, continue
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            Transform objectHit = hit.transform;

            //49 FIRST Compare tag of clicked object with VoidVoxel tag
            //58 SECOND Compare tag to TargetVoxel with ||
            if (objectHit.CompareTag("VoidVoxel") || objectHit.CompareTag("TargetVoxel"))
            {
                //50 Retrieve the voxel from the trigger
                Pix2PixVoxel voxel = objectHit.GetComponent<VoxelTrigger>().Voxel;

                //55 Set voxel as target and test
                voxel.SetAsTarget();

                //56 If voxel has be set as target, add it to _targets list
                if (voxel.IsTarget)
                {
                    _targets.Add(voxel);
                }
                //57 Else, remove it from _targets list
                else
                {
                    _targets.Remove(voxel);
                }
            }
        }
    }

    //81 Copy method to Ienumarator
    /// <summary>
    /// IEnumerator to animate the creation of the the shortest path algorithm
    /// </summary>
    /// <returns>Every step of the path</returns>
    private IEnumerator FindShortestPathAnimated()
    {
        List<Face> faces = new List<Face>();
        foreach (var face in _voxelGrid.GetFaces())
        {
            Pix2PixVoxel voxelA = face.Voxels[0];
            Pix2PixVoxel voxelB = face.Voxels[1];

            if (voxelA != null && !voxelA.IsObstacle && voxelA.IsActive &&
                voxelB != null && !voxelB.IsObstacle && voxelB.IsActive)
            {
                faces.Add(face);
            }
        }

        var graphFaces = faces.Select(f => new TaggedEdge<Pix2PixVoxel, Face>(f.Voxels[0], f.Voxels[1], f));
        var graph = graphFaces.ToUndirectedGraph<Pix2PixVoxel, TaggedEdge<Pix2PixVoxel, Face>>();

        for (int i = 1; i < _targets.Count; i++)
        {
            var start = _targets[i - 1];

            var shortest = graph.ShortestPathsDijkstra(e => 1.0, start);

            var end = _targets[i];
            if (shortest(end, out var endPath))
            {
                var endPathVoxels = new HashSet<Pix2PixVoxel>(endPath.SelectMany(e => new[] { e.Source, e.Target }));
                foreach (var pathVoxel in endPathVoxels)
                {
                    pathVoxel.SetAsPath();

                    //82 Yield return after setting voxel as path
                    yield return new WaitForSeconds(0.1f);
                }
            }
            else
            {
                throw new System.Exception($"No Path could be found!");
            }

        }
    }

    /// <summary>
    /// Get the voxels that are part of a Path
    /// </summary>
    /// <returns>All the path <see cref="GraphVoxel"/></returns>
    private IEnumerable<Pix2PixVoxel> GetPathVoxels()
    {
        foreach (Pix2PixVoxel voxel in _voxelGrid.Voxels)
        {
            if (voxel.IsPath)
            {
                yield return voxel;
            }
        }
    }


    private Texture2D duplicateTexture(Texture2D source)
    {
        RenderTexture renderTex = RenderTexture.GetTemporary(
                    source.width,
                    source.height,
                    0,
                    RenderTextureFormat.Default,
                    RenderTextureReadWrite.Linear);

        Graphics.Blit(source, renderTex);
        RenderTexture previous = RenderTexture.active;
        RenderTexture.active = renderTex;
        Texture2D readableText = new Texture2D(source.width, source.height);
        readableText.ReadPixels(new Rect(0, 0, renderTex.width, renderTex.height), 0, 0);
        readableText.Apply();
        RenderTexture.active = previous;
        RenderTexture.ReleaseTemporary(renderTex);
        return readableText;
    }




    #endregion
}