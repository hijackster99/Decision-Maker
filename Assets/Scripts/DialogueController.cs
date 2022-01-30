using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueController : MonoBehaviour
{

    [SerializeField] TextAsset dialogueFile;
    [SerializeField] Text dialogueBox;
    [SerializeField] Text nameBox;
    [SerializeField] int charDelay;

    [SerializeField] DecisionHandler decisionHandler;
    [SerializeField] CutsceneManager cutsceneManager;
    [SerializeField] IconManager iconManager;

    private string dialogue = "";
    private int currentDelay = 0;
    private int currentIndex = 0;
    private int appendText = 0;

    bool pause = false;
    bool wait = false;

    // Start is called before the first frame update
    void Start()
    {
        dialogue = dialogueFile.text;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown)
        {
            if (pause)
            {
                pause = false;
            }
            else if(!wait) FillToBreak();
        }
    }
    
    private void FixedUpdate()
    {
        if (!pause && !wait)
        {
            if (currentDelay <= 0)
            {
                if (currentIndex < dialogue.Length)
                {
                    if (dialogue[currentIndex] == '\\')
                    {
                        ++currentIndex;
                        if (dialogue[currentIndex] == 'p')
                        {
                            pause = true;
                        }
                        else if (dialogue[currentIndex] == 'r')
                        {
                            dialogueBox.text = "";
                        }
                        else if (dialogue[currentIndex] == 'w')
                        {
                            wait = true;
                        }
                        else if (dialogue[currentIndex] == '\\')
                        {
                            AddText('\\');
                        }
                        else if (dialogue[currentIndex] == 's')
                        {
                            SetNameBox();
                        }
                        else if (dialogue[currentIndex] == 'd')
                        {
                            decisionHandler.MakeDecision(dialogue.Substring(currentIndex + 2, dialogue.IndexOf(')', currentIndex) - currentIndex - 2), false);
                            currentIndex = dialogue.IndexOf(')', currentIndex);
                        }
                        else if (dialogue[currentIndex] == 'o')
                        {
                            dialogue = dialogue.Insert(currentIndex + 1, decisionHandler.output);
                        }
                        else if (dialogue[currentIndex] == 'C')
                        {
                            string color = dialogue.Substring(currentIndex + 2, dialogue.IndexOf(')', currentIndex) - currentIndex - 2);
                            AddText("<color=" + color + "></color>");
                            appendText += 8;
                            currentIndex = dialogue.IndexOf(')', currentIndex);
                        }
                        else if (dialogue[currentIndex] == 'c')
                        {
                            appendText -= 8;
                        }
                        else if (dialogue[currentIndex] == 'B')
                        {
                            AddText("<b></b>");
                            appendText += 4;
                        }
                        else if (dialogue[currentIndex] == 'I')
                        {
                            AddText("<i></i>");
                            appendText += 4;
                        }
                        else if (dialogue[currentIndex] == 'b')
                        {
                            appendText -= 4;
                        }
                        else if (dialogue[currentIndex] == 'i')
                        {
                            appendText -= 4;
                        }
                        else if (dialogue[currentIndex] == 'n')
                        {
                            cutsceneManager.NextScene();
                        }
                        else if (dialogue[currentIndex] == 'a')
                        {
                            string responseComp = dialogue.Substring(currentIndex + 2, dialogue.IndexOf(')', currentIndex) - currentIndex - 2);
                            currentIndex = dialogue.IndexOf(')', currentIndex);
                            string[] responses = responseComp.Split('|');
                            if (decisionHandler.outputNum == 0) dialogue = dialogue.Insert(currentIndex + 1, responses[0]);
                            else dialogue = dialogue.Insert(currentIndex + 1, responses[1]);
                        }
                        else if (dialogue[currentIndex] == 'D')
                        {
                            decisionHandler.MakeDecision(dialogue.Substring(currentIndex + 2, dialogue.IndexOf(')', currentIndex) - currentIndex - 2), true);
                            currentIndex = dialogue.IndexOf(')', currentIndex);
                        }
                    }
                    else
                    {
                        if (dialogue[currentIndex] != '\n')
                        {
                            AddText(dialogue[currentIndex]);
                        }
                    }

                    currentDelay = charDelay;
                    ++currentIndex;
                }
                else
                {
                    Debug.Log("Good Count: " + decisionHandler.goodCount);
                    Debug.Log("Bad Count: " + decisionHandler.badCount);
                    Debug.Log("Major Bad: " + decisionHandler.majorBad);
                }
            }
            else
            {
                --currentDelay;
            }
        }
    }
    
    private void FillToBreak()
    {
        while (currentIndex < dialogue.Length)
        {
            if (dialogue[currentIndex] == '\\')
            {
                ++currentIndex;
                if (dialogue[currentIndex] == 'p')
                {
                    --currentIndex;
                    return;
                }
                else if (dialogue[currentIndex] == 'r')
                {
                    dialogueBox.text = "";
                }
                else if (dialogue[currentIndex] == 'w')
                {
                    wait = true;
                }
                else if(dialogue[currentIndex] == '\\')
                {
                    AddText(dialogue[currentIndex]);
                }
                else if (dialogue[currentIndex] == 's')
                {
                    SetNameBox();
                }
                else if (dialogue[currentIndex] == 'd')
                {
                    decisionHandler.MakeDecision(dialogue.Substring(currentIndex + 2, dialogue.IndexOf(')', currentIndex) - currentIndex - 2), false);
                    currentIndex = dialogue.IndexOf(')', currentIndex);
                }
                else if (dialogue[currentIndex] == 'o')
                {
                    dialogue = dialogue.Insert(currentIndex + 1, decisionHandler.output);
                }
                else if (dialogue[currentIndex] == 'C')
                {
                    string color = dialogue.Substring(currentIndex + 2, dialogue.IndexOf(')', currentIndex) - currentIndex - 2);
                    AddText("<color=" + color + "></color>");
                    appendText += 8;
                    currentIndex = dialogue.IndexOf(')', currentIndex);
                }
                else if (dialogue[currentIndex] == 'c')
                {
                    appendText -= 8;
                }
                else if (dialogue[currentIndex] == 'B')
                {
                    AddText("<b></b>");
                    appendText += 4;
                }
                else if (dialogue[currentIndex] == 'I')
                {
                    AddText("<i></i>");
                    appendText += 4;
                }
                else if (dialogue[currentIndex] == 'b')
                {
                    appendText -= 4;
                }
                else if (dialogue[currentIndex] == 'i')
                {
                    appendText -= 4;
                }
            }
            else
            {
                AddText(dialogue[currentIndex]);
            }
            ++currentIndex;
        }

    }
    
    private void SetNameBox() 
    {
        nameBox.text = "";
        nameBox.text += dialogue.Substring(currentIndex + 2, dialogue.IndexOf(')', currentIndex) - currentIndex - 2);
        iconManager.LoadIcon(nameBox.text);
        currentIndex = dialogue.IndexOf(')', currentIndex);
    }
    
    public void MoveOn()
    {
        wait = false;
    }

    private void AddText(string text)
    {
        if (appendText > 0)
        {
            dialogueBox.text = dialogueBox.text.Insert(dialogueBox.text.Length - appendText, text);
        }
        else
        {
            dialogueBox.text += text;
        }
    }

    private void AddText(char text)
    {
        if (appendText > 0)
        {
            dialogueBox.text = dialogueBox.text.Insert(dialogueBox.text.Length - appendText, new string(text, 1));
        }
        else
        {
            dialogueBox.text += text;
        }
    }

}
