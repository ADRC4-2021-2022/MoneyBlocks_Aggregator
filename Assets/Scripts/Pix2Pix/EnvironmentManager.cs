using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using QuickGraph;
using QuickGraph.Algorithms;

public class EnvironmentManager : MonoBehaviour
{
    #region Private fields

    VoxelGrid _voxelGrid;
    TextureHSVColor _voxelGrid2;
    //08 Create image variable
    Texture2D _sourceImage;
    Texture2D _sourceImage123;
    Texture2D _sourceImageCopy;
    //44 Create list to stores target voxels
    List<Voxel> _targets = new List<Voxel>();
    GameObject originVoxel;
    #endregion

    #region Unity methods

    public void Start()
    {
        
    }

    public void Update()
    {
        
    }

    #endregion

    #region Public methods


    public void LoadImage()
    {

        //01 Create a basic Pix2PixVoxelGrid
        //_voxelGrid = new Pix2PixVoxelGrid(new Vector3Int(50, 5, 25), transform.position, 1f);

        //09 Read Image from resources
        _sourceImage = Resources.Load<Texture2D>("Data/1234");
      //  _sourceImageCopy = duplicateTexture(_sourceImage);






        //SourceField
        //10 Create grid from image
        _voxelGrid = new VoxelGrid(_sourceImage, 3, 5, transform.position, 0.1f);

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
        _targets = new List<Voxel>();
    }

   public void ChangeNeibourColour()
   {
        
       


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

   

    //83 Create public method to start coroutine
    /// <summary>
    /// Start the animation of the Shortest path algorithm
    /// </summary>
    

    #endregion

    #region Private methods

    
    

    

    /// <summary>
    /// Get the voxels that are part of a Path
    /// </summary>
    /// <returns>All the path <see cref="GraphVoxel"/></returns>
    private IEnumerable<Voxel> GetPathVoxels()
    {
        foreach (Voxel voxel in _voxelGrid.Voxels)
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