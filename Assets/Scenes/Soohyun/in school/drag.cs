using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Drag : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    public Canvas parentCanvas;
    //클론
    private GameObject cloneObject;
    private RectTransform cloneRect;
    private CanvasGroup cloneCanvasGroup;
    int a = 0;
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
            this.name = this.name + ++a;
            cloneRect = cloneObject.GetComponent<RectTransform>();
            cloneCanvasGroup = cloneObject.GetComponent<CanvasGroup>();

            // 복제본만 드래그 가능하게 설정
            cloneCanvasGroup.blocksRaycasts = true;

            // 원본은 그대로, clone만 드래그
            //cloneRect.position = transform.position;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / parentCanvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = true;
    }
}