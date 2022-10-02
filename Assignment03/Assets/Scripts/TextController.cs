using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextController : MonoBehaviour
{
    public TMP_Text title1;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(BlinkText());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator BlinkText()
    {
        while (true)
        {
            title1.text = "";

            yield return new WaitForSeconds(.5f);

            title1.text = "PRESS ENTER";

            yield return new WaitForSeconds(.5f);
        }
    }
}
