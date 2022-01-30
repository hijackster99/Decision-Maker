using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CutsceneManager : MonoBehaviour
{

    [SerializeField] Image bgImage;
    [SerializeField] List<Sprite> scenes = new List<Sprite>();

    private int currentScene = 0;

    // Start is called before the first frame update
    void Start()
    {
        if (currentScene < scenes.Count) bgImage.sprite = scenes[currentScene];
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NextScene()
    {
        ++currentScene;
        if(currentScene < scenes.Count) bgImage.sprite = scenes[currentScene];
    }
}
