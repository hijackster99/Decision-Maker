using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueController : MonoBehaviour
{

    [SerializeField] TextAsset dialogueFile;
    [SerializeField] Text textField;
    [SerializeField] int charDelay;

    private string dialogue = "";
    private int currentDelay = 0;
    private int currentIndex = 0;

    bool pause = false;

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
            Debug.Log("Press");
            if (pause)
            {
                textField.text = "";
                pause = false;
            }
            else FillToBreak();
        }
    }

    private void FixedUpdate()
    {
        //Debug.Log(pause);
        if (!pause)
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
                        else if(dialogue[currentIndex] == '\\')
                        {
                            textField.text += '\\';
                        }
                    }
                    else
                    {
                        textField.text += dialogue[currentIndex];
                    }

                    currentDelay = charDelay;
                    ++currentIndex;
                }
                else
                {
                    currentIndex = 0;
                    textField.text = "";
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
                else if(dialogue[currentIndex] == '\\')
                {
                    textField.text += dialogue[currentIndex];
                    ++currentIndex;
                }
            }
            else
            {
                textField.text += dialogue[currentIndex];
                ++currentIndex;
            }
        }

    }
}
