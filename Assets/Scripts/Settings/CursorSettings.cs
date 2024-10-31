using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CursorSettings : MonoBehaviour
{

    private void Start()
    {
        SetCursorInvisible();
        UnityEngine.Cursor.lockState = CursorLockMode.Locked;
    }

    public void SetCursorVisible()
    {
        UnityEngine.Cursor.visible = true;
    }

    public void SetCursorInvisible()
    {
        UnityEngine.Cursor.visible = false;
    }
}
