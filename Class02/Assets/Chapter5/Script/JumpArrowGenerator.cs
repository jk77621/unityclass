using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpArrowGenerator : MonoBehaviour
{
    public GameObject arrowPrefab;
    float span = 1.0f;
    float delta = 0;

    // Update is called once per frame
    void Update()
    {
        delta += Time.deltaTime; //���� ������ �ð� = �����Ӱ� ������ ������ �ð�
        if (delta > span) // 1���̻� ������ �Ʒ������� �����Ѵ�.
        {
            delta = 0;
            GameObject genObj = Instantiate(arrowPrefab); //���ӿ�����Ʈ�� �����Ѵ�.
            int px = Random.Range(-6, 7); //-6���� 6������ ���� �����ϰ� ����Ѵ�.

            genObj.transform.position = new Vector3(px, 7, 0);
        }
    }
}
