using UnityEngine;
using System;

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
