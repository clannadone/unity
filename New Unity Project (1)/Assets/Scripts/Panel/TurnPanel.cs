using UnityEngine;
using System.Collections;

public class TurnPanel : BasePanel {

    private CanvasGroup canvasGroup;

    void Start()
    {
        if (canvasGroup == null) canvasGroup = GetComponent<CanvasGroup>();
    }


    public override void OnEnter()
    {
        if (canvasGroup == null) canvasGroup = GetComponent<CanvasGroup>();
        canvasGroup.alpha = 1;
        //canvasGroup.blocksRaycasts = false;
    }

    public override void OnExit()
    {
        canvasGroup.alpha = 0;
        //canvasGroup.blocksRaycasts = false;
    }

    public void OnClosePanel()
    {
        UIManager.Instance.PopPanel();
    }

}
