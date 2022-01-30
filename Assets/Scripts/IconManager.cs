using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IconManager : MonoBehaviour
{

    [SerializeField] private List<string> keyList = new List<string>();
    [SerializeField] private List<Sprite> valueList = new List<Sprite>();
    [SerializeField] private Image bgImage;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadIcon(string name)
    {
        if(keyList.IndexOf(name) != -1)
        {
            bgImage.sprite = valueList[keyList.IndexOf(name)];
        }
    }
}
