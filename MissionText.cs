using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MissionText : MonoBehaviour
{
    public Text msText;
    public GameObject wallI,wallII,wallIII;

    // Start is called before the first frame update
    /*void Start()
    {
        int test = 0;
        msText.text = "ภารกิจ\nตามหาขวาน "+test+"/1";
        //msText = text.Replace("\n", "@" + System.Environment.NewLine);
    }*/

    // Update is called once per frame
    public void SetMissionI(int ms, Color msc)
    {
        //Color newColor = Color.green;
        msText.text = "ภารกิจ\nตามหาขวาน " + ms + "/1";
        msText.color = msc;
        StartCoroutine(HideMissionI(5f));
        //wallI.SetActive(false);
    }
    public void SetMissionII(int ms, Color msc, bool msb)
    {
        gameObject.SetActive(true);
        msText.text = "ภารกิจ\nตามหาไฟฉาย " + ms + "/1";
        msText.color = msc;
        if (msb == true)
        {
            StartCoroutine(HideMissionI(5f));
            wallII.SetActive(false);
        }

    }
    public void SetMissionIII(int ms, Color msc, bool msb)
    {
        gameObject.SetActive(true);
        msText.text = "ภารกิจ\nตามหาปืน " + ms + "/1";
        msText.color = msc;
        if (msb == true)
        {
            StartCoroutine(HideMissionI(5f));
        }
    }
    public void SetMissionIV(int ms, Color msc, bool msb)
    {
        gameObject.SetActive(true);
        msText.text = "ภารกิจ\nตามหาเศษแผนที่ " + ms + "/4";
        msText.color = msc;
        if (msb == true)
        {
            StartCoroutine(HideMissionI(5f));
            wallIII.SetActive(false);
        }
    }
    IEnumerator HideMissionI(float secondUntildestroy)
    {
        yield return new WaitForSeconds(secondUntildestroy);
        gameObject.SetActive(false);
        wallI.SetActive(false);
        //SetMissionII(0,Color.red);
        //msText.text = " ";
    }
}
