using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class makeClone : MonoBehaviour
{
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    public Canvas parentCanvas;
    //클론
    private GameObject cloneObject;
    private RectTransform cloneRect;
    private CanvasGroup cloneCanvasGroup;
    int a = 1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public void OnBeginDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = false;
        // 복제본 생성
        cloneObject = Instantiate(this.gameObject, parentCanvas.transform);
        cloneObject.name = this.name;
        cloneRect = cloneObject.GetComponent<RectTransform>();
        cloneCanvasGroup = cloneObject.GetComponent<CanvasGroup>();

        // 복제본만 드래그 가능하게 설정
        cloneCanvasGroup.blocksRaycasts = true;

        // 원본은 그대로, clone만 드래그
        cloneRect.position = transform.position;
    }
}
