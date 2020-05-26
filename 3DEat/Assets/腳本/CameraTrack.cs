using UnityEngine;
using UnityEngine.UI;

public class CameraTrack : MonoBehaviour
{
    #region 欄位與屬性
    /// <summary>
    /// 玩家變形工具
    /// </summary>
    private Transform player;


    [Header("追蹤速度"), Range(0.1f, 50.5f)]
    public float speed = 1.5f;
    #endregion




    #region 方法

    /// <summary>
    /// 追蹤玩家
    /// </summary>
    private void Track() 
    {
        //攝影機與小明Y軸距離 7
        //攝影機與小明Z軸距離 -8

        Vector3 posTrack = player.position ;
        posTrack.x += 3f;
        posTrack.y += 6f;
        posTrack.z += -8f;

        //攝影機座標 = 變形.座標
        Vector3 posCam = transform.position;
        // 攝影機座標 = 三維向量.插值(A點,B點,百分比)
        posCam = Vector3.Lerp(posCam, posTrack, 0.5f * Time.deltaTime * speed);
        //變形.座標= 攝影機座標
        transform.position = posCam;

    }


    #endregion
    #region 事件
    private void Start()
    {
        // 小明物件 = 遊戲物件.尋找("物件名稱").變形
        player = GameObject.Find("小明").transform;
    }

    private void Update()
    {
        //Mathf.Lerp(A,B,比例) 取介於AB之間某比例的值
        //Mathf.Lerp(0,10,0.5f) = 5
        // Vector2.Lerp(Vector2,Vector2,float)
    }


    // 延遲更新 會在update執行後再執行
    // 建議：需要追蹤座標要寫在此事件內
    private void LateUpdate()
    {
        Track();
    }


    #endregion







}
