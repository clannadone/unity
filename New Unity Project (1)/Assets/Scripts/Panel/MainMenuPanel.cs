using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class MainMenuPanel : BasePanel {
    GameManager gameManager;
    public Image turn;
    [SerializeField]
    private ScrollRect scrollControl;

    public Sprite Red;
    public Sprite Black;
    private CanvasGroup canvasGroup;
    public Transform content;
    [SerializeField]
    public Text Message;
    public void PrintMessage(bool redTurn,string s,bool IsError=false)
    {
        Text temp = GameObject.Instantiate(Message, content);
        temp.gameObject.SetActive(true);

        temp.color = IsError ? Color.yellow: redTurn ? Color.red :Color.white;
        temp.text = s;
        StartCoroutine("ScrollToBottom");
    }
    IEnumerator ScrollToBottom()
    {
        yield return new WaitForEndOfFrame();
        scrollControl.verticalNormalizedPosition = 0f;
    }
    void Start()
    {
       
        gameManager = FindObjectOfType<GameManager>();
        canvasGroup = GetComponent<CanvasGroup>();
        gameManager.myDelegate += PrintMessage;
    }

    public override void OnPause()
    {
        canvasGroup.blocksRaycasts = false;//当弹出新的面板的时候，让主菜单面板 不再和鼠标交互
    }
    public override void OnResume()
    {
        canvasGroup.blocksRaycasts = true;
    }

    public void OnPushPanel(string panelTypeString)
    {
        UIPanelType panelType = (UIPanelType) System.Enum.Parse(typeof(UIPanelType), panelTypeString);
        UIManager.Instance.PushPanel(panelType);
    }
    private void Update()
    {
        turn.sprite = gameManager.redTurn ? Red : Black;
    }
}
