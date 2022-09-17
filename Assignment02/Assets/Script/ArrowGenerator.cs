using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowGenerator : MonoBehaviour
{
    public GameObject button;
    public List<float> songNodeTiming;
    public List<GameObject> nodeObjList;
    float delta = 0;
    bool isStart = false;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        isStart = button.GetComponent<ButtonController>().isStart;

        if (!isStart) return;

        delta += Time.deltaTime;

        int rndNum1 = Random.Range(0, songNodeTiming.Count);
        float span = songNodeTiming[rndNum1];
        if (delta > span)
        {
            delta = 0;

            int rndNum2 = Random.Range(0, nodeObjList.Count);
            GameObject genObj = Instantiate(nodeObjList[rndNum2]);
            switch (rndNum2)
            {
                case 0:
                    genObj.transform.position = new Vector3(0, 7, 0);
                    break;
                case 1:
                    genObj.transform.position = new Vector3(-10, 0, 0);
                    break;
                case 2:
                    genObj.transform.position = new Vector3(0, -7, 0);
                    break;
                case 3:
                    genObj.transform.position = new Vector3(10, 0, 0);
                    break;
            }
        }
    }
}
