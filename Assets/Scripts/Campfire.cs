using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ���������
/// </summary>
public class Campfire : MonoBehaviour
{
    [SerializeField] new Light light;
    float time=20f;//ȼ��ʱ��
    float currentTime=20f;//ʣ��ȼ��ʱ��
    private void Update()
    {
        if (currentTime <= 0)
        {
            currentTime = 0;
            light.transform.parent.gameObject.SetActive(false);
        }
        else
        {
            currentTime -= Time.deltaTime;
            light.intensity = Mathf.Clamp(currentTime / time, 0, 1) * 3f;
        }
        
    }
    public void AddWood()
    {
        currentTime += 10f;
        light.transform.parent.gameObject.SetActive(true);
    }
}
