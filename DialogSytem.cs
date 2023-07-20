using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DialogSytem : MonoBehaviour
{
    public Text dialog;
    public int line = 0;
    public float TimeToType = 3.0f;
    public bool endWord = false;
    public float textPercentage = 0f;
    public TextAsset textFile;

    void Start()
    {
        GetDialog(0);
    }

    void Update()
    {
        GetDialog(line);
        /*if(Input.GetMouseButtonDown(0))
        {
            line = line + 1;
            textPercentage = 0f;
            endWord = false;
        }*/
    }

    void GetDialog(int line)
    {
        string[] linesInFile;
        linesInFile = textFile.text.Split('\n');
        if(line < linesInFile.Length){
            callTyping(linesInFile[line]);
        }else
        {
            dialog.text = "";
        }
    }

    void callTyping(string TextToType)
    {
        int numberOfLettersToShow = (int)(TextToType.Length * textPercentage);
        dialog.text = TextToType.Substring(0, numberOfLettersToShow);
        textPercentage += Time.deltaTime / TimeToType;
        textPercentage = Mathf.Min(1.0f, textPercentage);
        if(textPercentage >=1)
        {
            endWord = true;
        }
    }
}
