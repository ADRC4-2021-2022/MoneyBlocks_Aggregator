using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
///<summary>
///
///</summary>
public class StartPanel : BasePanel
{
    private static string name = "StartPanel";
    private static string path = "Panel/StartPanel";
    public static readonly UIType uiType = new UIType(path, name);

    public StartPanel(): base(uiType)
    {

    }

    public override void OnStart()
    {
        base.OnStart();
        UIFunction.GetInstance().GetOrAddSingleComponentInChild<Button>(activeObj, "BTNBack").onClick.AddListener(Back);
        UIFunction.GetInstance().GetOrAddSingleComponentInChild<Button>(activeObj, "BTNStart").onClick.AddListener(Start);


    }

    private void Back()
    {
        GameRoot.GetInstance().UIManager_Root.Pop(false);
    }

    private void Start()
    {
        Scene2 scene2 = new Scene2();
        GameRoot.GetInstance().SceneControl_Root.LoadScene(scene2.sceneName, scene2);
    }

    public override void OnDestroy()
    {
        base.OnDestroy();  
    }

    public override void OnDisable()
    {
        base.OnDisable();
    }

    public override void OnEnable()
    {
        base.OnEnable();
    }

}

