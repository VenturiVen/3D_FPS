using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class tempUIStates : MonoBehaviour
{
    // https://youtu.be/7O0QK1D5LTE?si=RHSJma5dOOz0uM4E

    private Transform trans;
    private Vector3 offset = new Vector3(0, 180, 0); // flip to correct side

    // text objects
    public GameObject textmeshpro;

    // Ttext components
    TextMeshProUGUI textmeshpro_text;

    // get string
    public Envy_FSM fsm;

    // Start is called before the first frame update
    void Start()
    {
        trans = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Transform>();

        textmeshpro_text = textmeshpro.GetComponent<TextMeshProUGUI>();

        ChangeText("Hello");
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(trans);
        transform.Rotate(offset);

        ChangeText(fsm.CurrentStateAsString);
    }

    public void ChangeText(string text)
    {
        textmeshpro_text.text = text;   
    }
}
