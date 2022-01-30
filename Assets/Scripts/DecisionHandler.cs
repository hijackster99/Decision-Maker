using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DecisionHandler : MonoBehaviour
{

    [SerializeField] Text leftField;
    [SerializeField] Text rightField;
    [SerializeField] DialogueController controller;
    public string output { get; private set; }

    private GameObject choiceMenu;

    // Start is called before the first frame update
    void Start()
    {
        choiceMenu = GameObject.Find("ChoiceMenu");
        choiceMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MakeDecision(string decision)
    {
        string[] fields = decision.Split('|');
        leftField.text = fields[0];
        rightField.text = fields[1];

        choiceMenu.SetActive(true);
    }

    public void MakeChoice(int choice)
    {
        if (choice == 1) output = leftField.text;
        else output = rightField.text;
        controller.MoveOn();
        choiceMenu.SetActive(false);
    }
}
