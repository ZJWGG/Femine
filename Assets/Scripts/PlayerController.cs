using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : ObjectBase
{
    public static PlayerController Instance;
    [SerializeField] float hungry = 100f;
    [SerializeField] float turnSpeed=10;
    [SerializeField] float moveSpeed=1;
    [SerializeField] float hungrySpeed=2;
    [SerializeField] float hpSpeed=2;
    CharacterController characterController;
    Transform GetTransform;
    bool isAttacking;
    public float Hungry { 
        get => hungry;
        set 
        {
            hungry = value;
            if (hungry <= 0)
            {
                hungry = 0;
                Hp -= Time.deltaTime * hpSpeed;
                Debug.Log(Hp);
            }
            UI.Instance.HungryUpdate();
        }
    }

    private void Awake()
    {
        Instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        GetTransform = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        OnHungryUpdate();
        if (isAttacking)
        {
            GetTransform.localRotation = Quaternion.Slerp(GetTransform.localRotation, targetDirQuaternion, Time.deltaTime * turnSpeed);
        }
        else
        {
            Move();
            Attack();
        }
     
    }
    private void OnHungryUpdate()
    {
        Hungry -= Time.deltaTime * hungrySpeed;
    }
    protected override void OnHpUpdate()
    {
        UI.Instance.HPUpdate();
    }

    Quaternion targetDirQuaternion;


    #region 移动和转向
    public void Move()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        if (h == 0 && v == 0)
        {
            AnimatorStringToHash.Instance.IsNotWalking();
        }
        else
        {
            AnimatorStringToHash.Instance.IsWalking();
            targetDirQuaternion = Quaternion.LookRotation(new Vector3(h, 0, v));
            GetTransform.localRotation = Quaternion.Slerp(GetTransform.localRotation, targetDirQuaternion, Time.deltaTime * turnSpeed);
            characterController.SimpleMove(new Vector3(h, 0, v) * moveSpeed) ;
        }

    }
    #endregion
    private void Attack()
    {
        if (Input.GetMouseButton(0))
        {
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hitInfo, 100f, LayerMask.GetMask("Ground")))
            {
                //碰到地面
                isAttacking = true;
                AnimatorStringToHash.Instance.IsAttacking();
                targetDirQuaternion = Quaternion.LookRotation(hitInfo.point-GetTransform.position);
            }
        }
    }
    #region 动画事件
    private void StartHit()
    {
        PlayAudio(0);
    }
    private void StopHit()
    {
        isAttacking = false;
    }
    #endregion
}
