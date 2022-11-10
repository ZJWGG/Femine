using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public static TimeManager Instance;
    [SerializeField] Light sunLight;
    public float dayTime;//�����ʱ��
    public float dayToNightTime;//���쵽ҹ���ʱ��
    public float nightTime;//ҹ���ʱ��
    public float nightToDayTime;//ҹ�������ʱ��
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
        //����ʱ��
        StartCoroutine(UpdateTime());
    }
    private IEnumerator UpdateTime()
    {
        while (true)
        {
            yield return null;
            //�����ǰ�ǰ���
            if (IsDay)
            {
                lightValue -= 1 / dayToNightTime * Time.deltaTime;
                SetLightValue(lightValue);
                if (lightValue < 0)
                {
                    IsDay = false;
                    yield return new WaitForSeconds(nightTime);//�ȴ�ҹ���ȥ
                }
            }
            //��ǰ��ҹ��
            else
            {
                lightValue += 1 / nightToDayTime * Time.deltaTime;
                SetLightValue(lightValue);
                if (lightValue >=1)
                {
                    IsDay = true;
                    yield return new WaitForSeconds(dayTime);//�ȴ������ȥ
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
