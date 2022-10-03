using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackController : MonoBehaviour
{
    public GameObject target;
    public List<GameObject> arrBack = new List<GameObject>();
    public List<GameObject> arrMiddle = new List<GameObject>();
    public float nBackSpeed = 0.01f;
    public float nMiddleSpeed = 0.01f;
    float nBackWidth;
    float nMiddleWidth;

    // Start is called before the first frame update
    void Start()
    {
        nBackWidth = arrBack[0].GetComponent<Renderer>().bounds.size.x;
        nMiddleWidth = arrMiddle[0].GetComponent<Renderer>().bounds.size.x;

        Debug.Log(nBackWidth);
        Debug.Log(nMiddleWidth);
    }

    // Update is called once per frame
    void Update()
    {
        GameObject targetBack;
        for (int i = 0; i < arrBack.Count; ++i)
        {
            targetBack = arrBack[i];
            targetBack.transform.Translate(-nBackSpeed, 0, 0);

            if (targetBack.transform.position.x < target.transform.position.x - nBackWidth)
            {
                targetBack.transform.position = new Vector3(targetBack.transform.position.x + (nBackWidth * arrBack.Count), targetBack.transform.position.y, 0);
            }
        }

        GameObject targetMiddle;
        for (int i = 0; i < arrMiddle.Count; ++i)
        {
            targetMiddle = arrMiddle[i];
            targetMiddle.transform.Translate(-nMiddleSpeed, 0, 0);

            if (targetMiddle.transform.position.x < target.transform.position.x - (nMiddleWidth * 2))
            {
                targetMiddle.transform.position = new Vector3(targetMiddle.transform.position.x + (nMiddleWidth * arrMiddle.Count), targetMiddle.transform.position.y, 0);
            }
        }
    }
}
