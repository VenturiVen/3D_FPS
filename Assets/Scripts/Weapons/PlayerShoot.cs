using UnityEngine;
using System;

// This script was made with the help of this YouTube video:
// https://www.youtube.com/watch?v=kXbQMhwj5Uc

public class PlayerShoot : MonoBehaviour
{
    public static Action shootInput;
    public static Action reloadInput;

    [SerializeField] private KeyCode reloadKey;
    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            shootInput.Invoke();
        }

        if (Input.GetKeyDown(reloadKey))
        {
            reloadInput.Invoke();
        }
    }
}
