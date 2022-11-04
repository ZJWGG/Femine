using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    public static UI Instance;
    [SerializeField] Image hpImage;
    [SerializeField] Image hungryImage;
    private void Awake()
    {
        Instance = this;
    }
    public void HPUpdate()
    {
        hpImage.fillAmount = PlayerController.Instance.Hp/100;
    }
    public void HungryUpdate()
    {
        hungryImage.fillAmount = PlayerController.Instance.Hungry/100;
    }

}
