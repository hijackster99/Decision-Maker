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
    public int outputNum { get; private set; } = 0;

    public int goodCount { get; private set; } = 0;
    public int badCount { get; private set; } = 0;

    public bool majorBad { get; private set; }  = false;

    private string left;
    private string right;
    private bool major = false;

    private int intro = -4;

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

    public void MakeDecision(string decision, bool major)
    {
        this.major = major;
        string[] fields = decision.Split('|');
        left = fields[0];
        right = fields[1];
        leftField.text = ProcessString(fields[0]);
        rightField.text = ProcessString(fields[1]);
        choiceMenu.SetActive(true);
    }

    public void MakeChoice(int choice)
    {
        if(intro == -4 || intro == -2 || intro == -1)
        {
            if (choice == 1)
            {
                output = left;
                outputNum = 0;
            }
            else
            {
                output = right;
                outputNum = 1;
            }
            controller.MoveOn();
            choiceMenu.SetActive(false);
            ++intro;
        }
        else if(intro == -3)
        {
            if (choice == 1)
            {
                ++goodCount;
                output = left;
                outputNum = 0;
                controller.MoveOn();
                choiceMenu.SetActive(false);
                ++intro;
            }
        }
        else
        {
            if (major)
            {
                if (choice == 1)
                {
                    output = left;
                    outputNum = 0;
                }
                else
                {
                    majorBad = true;
                    output = right;
                    outputNum = 1;
                }
                major = false;
            }
            else
            {
                if (choice == 1)
                {
                    ++goodCount;
                    output = left;
                    outputNum = 0;
                }
                else
                {
                    ++badCount;
                    output = right;
                    outputNum = 1;
                }
            }
            controller.MoveOn();
            choiceMenu.SetActive(false);
        }
    }

    private string ProcessString(string str)
    {
        string result = "";
        int currentIndex = 0;
        int appendText = 0;
        while (currentIndex < str.Length)
        {
            if (str[currentIndex] == '\\')
            {
                ++currentIndex;
                if (str[currentIndex] == 'C')
                {
                    string color = str.Substring(currentIndex + 2, str.IndexOf(')', currentIndex) - currentIndex - 2);
                    result = AddText("<color=" + color + "></color>", result, appendText);
                    appendText += 8;
                    currentIndex = str.IndexOf(')', currentIndex);
                }
                else if (str[currentIndex] == 'c')
                {
                    appendText -= 8;
                }
                else if (str[currentIndex] == 'B')
                {
                    result = AddText("<b></b>", result, appendText);
                    appendText += 4;
                }
                else if (str[currentIndex] == 'I')
                {
                    result = AddText("<i></i>", result, appendText);
                    appendText += 4;
                }
                else if (str[currentIndex] == 'b')
                {
                    appendText -= 4;
                }
                else if (str[currentIndex] == 'i')
                {
                    appendText -= 4;
                }
            }
            else
            {
                if(str[currentIndex] != '\n') result = AddText(str[currentIndex], result, appendText);
            }
            ++currentIndex;
        }
        return result;
    }

    private string AddText(char text, string result, int appendText)
    {
        if (appendText > 0)
        {
            result = result.Insert(result.Length - appendText, new string(text, 1));
        }
        else
        {
            result += text;
        }
        return result;
    }
    private string AddText(string text, string result, int appendText)
    {
        if (appendText > 0)
        {
            result = result.Insert(result.Length - appendText, text);
        }
        else
        {
            result += text;
        }
        return result;
    }
}
