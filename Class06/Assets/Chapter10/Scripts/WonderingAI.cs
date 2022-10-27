using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 1. ��� ������ ������
 * 
 * 2. �����θ� �̵��� ��
 * 
 * 3. �� �տ� Ư�� ��ü�� �ִ��� üũ�� ����
 * 
 * 4. ���� Ư�� ��ü�� �ִµ�, �װ� ���̶�� �����Ѵ�.
 * 
 * 5. ���� ��� ���� �Ÿ���ŭ ��������� �ٸ� �������� ��ȯ�Ѵ�.
 */

public class WonderingAI : MonoBehaviour
{
    public float speed = 3.0f;
    public float obstarcleRange = 5.0f;

    [SerializeField] GameObject fireballPrefab;
    GameObject _fireball;

    bool _isAlive;

    // Start is called before the first frame update
    void Start()
    {
        _isAlive = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (_isAlive)
        {
            //��� �ִ� ���
            transform.Translate(0, 0, speed * Time.deltaTime);

            Ray ray = new Ray(transform.position, transform.forward);
            RaycastHit hit;

            if (Physics.SphereCast(ray, 0.75f, out hit))
            {
                GameObject hitObject = hit.transform.gameObject;

                if (hitObject.GetComponent<PlayerCharacter>())
                {
                    if(_fireball == null)
                    {
                        _fireball = Instantiate(fireballPrefab) as GameObject;
                        _fireball.transform.position = transform.TransformPoint(Vector3.forward * 1.5f);
                        _fireball.transform.rotation = transform.rotation;
                    }
                }
                else if (hit.distance < obstarcleRange)
                {
                    float angle = Random.Range(-110, 110);
                    transform.Rotate(0, angle, 0);
                }
            }
        }
    }

    public void SetAlive(bool alive)
    {
        _isAlive = alive;
    }
}
