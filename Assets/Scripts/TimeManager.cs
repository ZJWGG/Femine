using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public static TimeManager Instance;
    [SerializeField] Light sunLight;
    public float dayTime;//白天的时间
    public float dayToNightTime;//白天到夜晚的时间
    public float nightTime;//夜晚的时间
    public float nightToDayTime;//夜晚到白天的时间
    float lightValue = 1f;
    int dayNums;
    bool isDay;

    public bool IsDay { 
        get => isDay; 
        set
        {
            isDay = value;
            if (isDay)
            {
                dayNums++;
                UI.Instance.dayNumsText.text = "Day" + dayNums;
                UI.Instance.timeStateImage.sprite = UI.Instance.dayStateSprites[0];
            }
            else
            {
                UI.Instance.timeStateImage.sprite = UI.Instance.dayStateSprites[1];
            }
        }
     }

    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        IsDay = true;
        //计算时间
        StartCoroutine(UpdateTime());
    }
    private IEnumerator UpdateTime()
    {
        while (true)
        {
            yield return null;
            //如果当前是白天
            if (IsDay)
            {
                lightValue -= 1 / dayToNightTime * Time.deltaTime;
                SetLightValue(lightValue);
                if (lightValue < 0)
                {
                    IsDay = false;
                    yield return new WaitForSeconds(nightTime);//等待夜晚过去
                }
            }
            //当前是夜晚
            else
            {
                lightValue += 1 / nightToDayTime * Time.deltaTime;
                SetLightValue(lightValue);
                if (lightValue >=1)
                {
                    IsDay = true;
                    yield return new WaitForSeconds(dayTime);//等待白天过去
                }
            }
        }
    }
    private void SetLightValue(float value)
    {
        RenderSettings.ambientIntensity = value;
        sunLight.intensity = value;
    }
}
