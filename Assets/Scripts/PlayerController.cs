using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : ObjectBase
{
    public static PlayerController Instance;
    [SerializeField] float hungry;
    [SerializeField] float turnSpeed=10;
    [SerializeField] float moveSpeed;
    [SerializeField] float hungrySpeed=2;
    [SerializeField] float hpSpeed=2;
    CharacterController characterController;
    public Transform GetTransform;
    bool isAttacking;
    bool isHurting;
    public bool isDraging;
    Animator animator;
    int ani_WalkHash;
    int ani_CutHash;
    int ani_HurtHash;
    [SerializeField] CheckCollider checkCollider;

    public float Hungry { 
        get => hungry;
        set 
        {
            hungry = value;
            if (hungry <= 0)
            {
                hungry = 0;
                Hp -= Time.deltaTime * hpSpeed;
            }
            UI.Instance.HungryUpdate();
        }
    }

    private void Awake()
    {
        Instance = this;
        checkCollider.Init(this, 30);
    }
    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        GetTransform = GetComponent<Transform>();
        animator = GetComponent<Animator>();
        ani_WalkHash = Animator.StringToHash("走路");
        ani_CutHash = Animator.StringToHash("攻击");
        ani_HurtHash = Animator.StringToHash("受伤");
    }

    // Update is called once per frame
    void Update()
    {
        OnHungryUpdate();
        if (!isAttacking)
        {
            Move();
            Attack();
        }
        else
        {
            GetTransform.localRotation = Quaternion.Slerp(GetTransform.localRotation, targetDirQuaternion, Time.deltaTime * turnSpeed);
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
    public override void Hurt(int damage)
    {
        base.Hurt(damage);
        animator.SetTrigger(ani_HurtHash);
        PlayAudio(2);
        isHurting = true;
    }

    Quaternion targetDirQuaternion;


    #region 移动和转向
    public void Move()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        if (h == 0 && v == 0)
        {
            animator.SetBool(ani_WalkHash, false);
        }
        else
        {
            animator.SetBool(ani_WalkHash, true);
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
            //当前在拖拽物品||当前鼠标正在交互UI
            if (isDraging||UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
            {
                return;
            }
        
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hitInfo, 100f, LayerMask.GetMask("Ground")))
            {
                //碰到地面
                isAttacking = true;
                animator.SetTrigger(ani_CutHash);
                targetDirQuaternion = Quaternion.LookRotation(hitInfo.point-GetTransform.position);
            }
        }
    }
    public override bool AddItem(ItemType itemType)
    {
        //检测背包能不能放下
        return UI_Bag.Instance.AddItem(itemType);
    }
    public bool UseItem(ItemType itemType)
    {
        switch (itemType)
        {
            case ItemType.Meat:
                Hp += 10;
                hungry += 20;
                return true;
            case ItemType.CookedMeat:
                Hp += 20;
                hungry += 40;
                return true;
            case ItemType.Wood:
                Hp -= 20;
                hungry += 20;
                return true;
        }
        return false;
    }
    #region 动画事件
    private void StartHit()
    {
        PlayAudio(0);
        checkCollider.StartHit();
    }
    private void StopHit()
    {
        isAttacking = false;
        checkCollider.StopHit();
    }
    private void HurtOver()
    {
        isHurting = false;
    }
    #endregion
}
