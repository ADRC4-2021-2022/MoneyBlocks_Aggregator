using System.Collections;
using System.Collections.Generic;
using UnityEngine;

///<summary>
///
///</summary>
public class Test : MonoBehaviour
{

    private void Start()
    {
        GetColors();     
    }


  
    public Texture2D map;

    
    public Color[] pixels;

    
    
    public void GetColors()
    {
        pixels = map.GetPixels();

       
    }



}

