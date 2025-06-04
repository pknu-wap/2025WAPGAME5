using UnityEditor.Tilemaps;
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
    BoxCollider2D col;

    void Awake()
    {
        col = GetComponent<BoxCollider2D>();
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
            cloneObject = Instantiate(gameObject, parentCanvas.transform);
            cloneObject.name = this.name+'1';
            gameObject.layer = LayerMask.NameToLayer("Parentable");
            cloneRect = cloneObject.GetComponent<RectTransform>();
            cloneCanvasGroup = cloneObject.GetComponent<CanvasGroup>();
            cloneCanvasGroup.blocksRaycasts = true;
        }
        transform.SetAsLastSibling();



        col.enabled = false;


    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / parentCanvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = true;

        
        int layerMask = LayerMask.GetMask("Parentable");
        int layerMask2 = LayerMask.GetMask("trash");

        
        Vector3 mousePos = Input.mousePosition;
        float z = Camera.main.WorldToScreenPoint(transform.position).z;
        mousePos.z = z;
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(mousePos);
        mouseWorldPos.z = 0;
        RaycastHit2D hit = Physics2D.Raycast(mouseWorldPos, Vector2.zero, Mathf.Infinity, layerMask);
        RaycastHit2D hit2 = Physics2D.Raycast(mouseWorldPos, Vector2.zero, Mathf.Infinity, layerMask2);

        if (transform.parent.gameObject.layer == LayerMask.NameToLayer("Parent"))
        {
            transform.parent.gameObject.layer = LayerMask.NameToLayer("Parentable");
            transform.SetParent(parentCanvas.transform);
        }

        //맞았으면 연결
        if (hit.collider != null && hit.collider.gameObject != gameObject&& !hit.collider.transform.IsChildOf(transform))
        {
            Debug.Log(hit.collider);
            transform.SetParent(hit.collider.transform);
            hit.collider.gameObject.layer = LayerMask.NameToLayer("Parent");


            if (transform.parent.gameObject.layer == LayerMask.NameToLayer("Parent"))
            {
            gameObject.transform.position = hit.collider.gameObject.transform.position;
            transform.localPosition = Vector3.zero + new Vector3(0, -50, 0);
            }
            if (gameObject.GetComponent<BoxCollider2D>() == null)
            {
                BoxCollider2D collider = gameObject.AddComponent<BoxCollider2D>();
                collider.size = new Vector2(100f, 50f);
            }
        }
        if (hit2.collider != null )
        {
            foreach (Transform child in transform.GetComponentsInChildren<Transform>())
            {
                if (child.CompareTag("forward") || child.CompareTag("left") || child.CompareTag("right"))
                {

                    gameObject.SetActive(false);
                }
            }

        }
        col.enabled = true;
    }
}