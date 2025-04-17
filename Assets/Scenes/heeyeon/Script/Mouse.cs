using UnityEngine;

public class Mouse : MonoBehaviour
{
    private bool isCursorVisible = false;

    void Start()
    {
        LockCursor();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isCursorVisible)
                LockCursor();
            else
                UnlockCursor();
        }
    }

    public void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        isCursorVisible = false;
    }

    public void UnlockCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        isCursorVisible = true;
    }

    public bool IsCursorVisible()
    {
        return isCursorVisible;
    }
}
