using UnityEngine;

public static class GameHelper
{
    public static void SetHideAndLockCursor(bool value)
    {
        if (value)
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }

    public static float GetRotationAngleFromInput(float x, float y)
    {
        float rotationAngle = Mathf.Atan2(x, y) * Mathf.Rad2Deg;
        return rotationAngle;
    }
}
