using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 篝火管理器
/// </summary>
public class Campfire : MonoBehaviour
{
    [SerializeField] new Light light;
    float time=20f;//燃烧时间
    float currentTime=20f;//剩余燃烧时间
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
