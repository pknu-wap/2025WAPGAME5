using UnityEngine;

public class Mouse : MonoBehaviour
{
    public Texture2D fistCursor; 
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

        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }

    public void UnlockCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        isCursorVisible = true;

        Cursor.SetCursor(fistCursor, Vector2.zero, CursorMode.Auto);
    }

    public bool IsCursorVisible()
    {
        return isCursorVisible;
    }
}
