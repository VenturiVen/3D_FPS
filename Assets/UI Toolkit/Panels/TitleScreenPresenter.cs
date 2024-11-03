using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class TitleScreenPresenter : MonoBehaviour
{
    private void Awake()
    {
        VisualElement root = GetComponent<UIDocument>().rootVisualElement;

        /*
        Q stands for query


        root.Q("Start");
        look for visual element named Start


        root.Q<Button>("Start");
        look for visual element of type Button named Start
        */

        root.Q<Button>("Start").clicked += () => ChangeScene("Main");
    }

    // to add a scene to the build
    // File > Build Settings
    // Drag scenes in to window

    private void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
