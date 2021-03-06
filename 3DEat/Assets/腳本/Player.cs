﻿using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    #region 欄位與屬性
    [Header("移動速度"), Range(1, 1000)]
    public float speed = 10;
    [Header("跳躍高度"), Range(1, 5000)]
    public float height;
     GameManager GM;



    [Header("壽司音效")]
    public AudioClip soundFood;
    [Header("酒音效")]
    public AudioClip soundWine;






    /// <summary>
    /// 是否在地板上 
    /// </summary>
    private bool isGround 
    {
        get
        {
            if (transform.position.y < 0.055f) return true; //如果Y座標小於 0.051 傳回 true
            else return false;    //否則傳回 false
        }
    }

    private Animator ani; //動畫
    private Rigidbody rig; //剛體
    private AudioSource aud; //喇叭
    private Vector3 angle;

    /// <summary>
    /// 從0慢慢增加
    /// </summary>
    private float jump;





    #endregion



    #region 方法
    /// <summary>
    /// 移動：使用鍵盤
    /// </summary>
    private void Move()
    {
        
        #region 移動
        // 浮點數 前後值 = 輸入類別.取得軸向值("垂直") - 垂直 ws 上下
        float v = Input.GetAxisRaw("Vertical");
        // 水平 ad 左右
        float h = Input.GetAxisRaw("Horizontal");
        // 剛體 添加推力(x,y,z) 世界座標
        // rig.AddForce(0, 0, speed*v );
        // rig.AddForce(speed*h ,0 ,0 );
        // 剛體 添加推力(三維向量)

        // 前方 transform.forward - Z
        // 右方 transform.right - X
        // 上方 transform.up - Y
        rig.AddForce(transform.forward * speed * Mathf.Abs(v));
        rig.AddForce(transform.forward * speed * Mathf.Abs(h));

        // 動畫.設定布林值("跑步參數名稱",布林值) - 當 前後取絕對值 大於0時True
        ani.SetBool("跑步開關", Mathf.Abs(v)>0 || Mathf.Abs(h) > 0);

        // ani.SetBool("跑步開關", v == 1 || v == -1); 使用邏輯運算子
        #endregion
        #region 轉向
        /* if (v == 1)  angle = new Vector3(0, 0, 0); //前
        else if (v == -1) angle = new Vector3(0, 180, 0); //後
        else if (h == 1) angle = new Vector3(0, 90, 0); //右
        else if (h == -1) angle = new Vector3(0,-90 , 0); //左
        */



            angle = new Vector3(0, h * 90 - h * v * 45  + (Mathf.Abs(h)-1)*(v-1)*v*90 , 0);





        // 只要類別後面有 MonoBehaviour
        // 就可以直接使用關鍵字 transform 取得此物件的 Transform 元件 不用額外抓取
        transform.eulerAngles = angle;



            #endregion
    }

    /// <summary>
    /// 跳躍：判斷在地板上並按下空白鍵時跳躍
    /// </summary>
    private void Jump() 
    { 
        if (isGround && Input.GetButtonDown("Jump"))
        {
            //每次跳躍值都從 0 開始
            //剛體.推力 (0,跳躍高度,0)
            rig.AddForce(0, height, 0);
        }

        //如果不在地上
        if (!isGround)
        {
            //跳躍 遞增 時間.一禎時間
            jump += Time.deltaTime;
        }
        else
        { jump = 0; }

        // 動畫.設定浮點數("跳躍參數名稱",跳躍時間);
        ani.SetFloat("跳躍力道", jump); 
    }
    /// <summary>
    /// 碰到道具：碰到具有標籤[食物]的物件
    /// </summary>
    private void HitProp(GameObject prop) 
    {
        
        if (prop.tag == "食物")
        {
            aud.PlayOneShot(soundFood, 0.5f); // 喇叭.撥放一次音效(音效片段,音量)
                Destroy(prop);
        }
        
        if (prop.tag == "酒")
        {
            aud.PlayOneShot(soundWine, 2);
                Destroy(prop);
        }
                GM.itemGet(prop.tag);

    }


    #region 碰撞事件
    // 碰撞事件：當物件碰撞開始時執行一次 (沒有勾選 Is Trigger)

    private void OnCollisionEnter(Collision collision)
    {
        
    }

    // 碰撞事件：當物件碰撞分離時執行一次 (沒有勾選 Is Trigger)
    private void OnCollisionExit(Collision collision)
    {
        
    }

    // 碰撞事件：當物件碰撞中持續執行 (沒有勾選 Is Trigger)
    private void OnCollisionStay(Collision collision)
    {
        
    }
    #endregion





    // 碰撞事件：當物件碰撞開始時執行一次 (有勾選 Is Trigger)
    void OnTriggerEnter(Collider other)
        {
        //碰到道具(碰撞資訊.遊戲物件)
        HitProp(other.gameObject);
        }



    /*
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "食物")
        {
            countGet += 1;
            GM.itemGet(countGet);
            Destroy(other.gameObject);
        }
        if(other.tag == "酒")
        {
             GM.TimeSubstract();
                print("碰到的物件標籤為：" + other.name);
                Destroy(other.gameObject);

        }

    }
    */

    #endregion





    #region 事件


    void Start()
    {
        // GetComponent<泛型>() 泛型方法 - 泛型 所有類型 Rigitbody,Transform, Collider...
        // 剛體 = 取得元件<剛體>();
        rig = GetComponent<Rigidbody>();
        ani = GetComponent<Animator>();
        aud = GetComponent<AudioSource>();

        // 僅限於場景上只有一個具此類別的物件存在時使用
        //
        GM = FindObjectOfType<GameManager>(); 
    }

    void Update()
    {
        Move();
        
        Jump();
    }


    // 固定更新頻率事件：一秒50禎 , 使用物理必須在此事件內

    /*private void FixedUpdate()
    {

    }
    */


    #endregion





}
