using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Drag : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    //public connect connectt;
    public GameObject came;
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    public Canvas parentCanvas;
    //클론
    private GameObject cloneObject;
    private RectTransform cloneRect;
    private CanvasGroup cloneCanvasGroup;
    bool canMake = true;
    void Awake()
    {
        if (parentCanvas == null)
        {
            parentCanvas = GetComponentInParent<Canvas>();
        }
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        Vector3 canvaspos=parentCanvas.transform.position;
        came.transform.position=new Vector3(canvaspos.x, canvaspos.y, -400);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (canMake)
        {
            canMake = false;
            canvasGroup.blocksRaycasts = false;
            // 복제본 생성
            cloneObject = Instantiate(this.gameObject, parentCanvas.transform);
            cloneObject.name = this.name+'1';
            gameObject.layer = LayerMask.NameToLayer("Parentable");
            cloneRect = cloneObject.GetComponent<RectTransform>();
            cloneCanvasGroup = cloneObject.GetComponent<CanvasGroup>();
            cloneCanvasGroup.blocksRaycasts = true;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / parentCanvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = true;

        // 레이어 필터 설정
        int layerMask = 1 << LayerMask.NameToLayer("Parentable");

        // 마우스 위치 → 월드 위치
        Vector3 mousePos = Input.mousePosition;
        float z = Camera.main.WorldToScreenPoint(transform.position).z;
        mousePos.z = z;
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(mousePos);
        mouseWorldPos.z = 0;
        //Debug.Log("마우스" + mouseWorldPos);
        RaycastHit2D hit = Physics2D.Raycast(mouseWorldPos, Vector2.zero, Mathf.Infinity, layerMask);

        if (transform.parent.gameObject.layer == LayerMask.NameToLayer("Parent"))
        {
            transform.parent.gameObject.layer = LayerMask.NameToLayer("Parentable");
            transform.SetParent(parentCanvas.transform);
        }
        if (hit.collider != null && hit.collider.gameObject != gameObject)
            Debug.Log(hit.collider);
            Debug.Log(gameObject+"나");

        //맞았으면 연결
        if (hit.collider != null && hit.collider.gameObject != gameObject)
        {
            Debug.Log(hit.collider);
            if (transform.parent.gameObject.layer == LayerMask.NameToLayer("UI"))
                transform.SetParent(hit.collider.transform);
            if (transform.parent.childCount>0)
            {
                hit.collider.gameObject.layer = LayerMask.NameToLayer("Parent");
                Debug.Log(hit.collider+"부모됨");
            }


            if (transform.parent.gameObject.layer == LayerMask.NameToLayer("Parent"))
            {
                gameObject.transform.position = hit.collider.gameObject.transform.position;
                transform.localPosition = Vector3.zero + new Vector3(0, -50, 0);
            }
            //Debug.Log(transform.position);
            if (gameObject.GetComponent<BoxCollider2D>() == null)
            {
                BoxCollider2D collider = gameObject.AddComponent<BoxCollider2D>();
                collider.size = new Vector2(100f, 50f);
            }
        }
    }
}