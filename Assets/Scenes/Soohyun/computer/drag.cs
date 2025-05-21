using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Drag : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    //public connect connectt;
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
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (canMake)
        {
            canMake = false;
            canvasGroup.blocksRaycasts = false;
            // 복제본 생성
            cloneObject = Instantiate(this.gameObject, parentCanvas.transform);
            cloneObject.name = this.name;
            cloneRect = cloneObject.GetComponent<RectTransform>();
            cloneCanvasGroup = cloneObject.GetComponent<CanvasGroup>();
            cloneCanvasGroup.blocksRaycasts = true;
        }
        if (transform.parent.gameObject.layer == LayerMask.NameToLayer("Parent"))
        {
            transform.parent.gameObject.layer = LayerMask.NameToLayer("Parentable");
            transform.SetParent(parentCanvas.transform);
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
        Vector2 mousePos = GetMouseLocalPos();
        //mousePos.y = mousePos.y *0.441f;
        Debug.Log("마우스" + mousePos);
        Vector3 canvasWorldPos = parentCanvas.transform.position;
        mousePos = mousePos + new Vector2(canvasWorldPos.x, canvasWorldPos.y);
        //Debug.Log(mousePos);
        RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero, Mathf.Infinity, layerMask);
        //맞은게 맞는지 확인
        if (hit.collider != null)
        {
            //Debug.Log(hit.collider.gameObject.name);
        }
        else
        {
            //Debug.Log("아무것도 안 맞음");
        }
        //맞았으면 연결
        if (hit.collider != null && hit.collider.gameObject != gameObject)
        {
            hit.collider.gameObject.layer = LayerMask.NameToLayer("Parent");
            gameObject.transform.position = hit.collider.gameObject.transform.position;
            transform.SetParent(hit.collider.transform);
            gameObject.layer = LayerMask.NameToLayer("Parentable");
            transform.localPosition = Vector3.zero + new Vector3(0, -50, 0);
            Debug.Log(transform.position);
            if (gameObject.GetComponent<BoxCollider2D>() == null)
            {
                BoxCollider2D collider = gameObject.AddComponent<BoxCollider2D>();
                collider.size = new Vector2(100f, 50f)*2f;
            }

        }
    }
    Vector2 GetMouseLocalPos()
    {
        Vector2 localPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            parentCanvas.transform as RectTransform,
            Input.mousePosition,
            parentCanvas.worldCamera,
            out localPoint
        );
        return localPoint;
    }
}