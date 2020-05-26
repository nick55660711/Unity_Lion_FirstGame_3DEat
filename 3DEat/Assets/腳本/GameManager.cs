using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    #region field&prop
    [Header("道具")]
    public GameObject[] porps;

    /// <summary>
    /// 需要道具總數
    /// </summary>
    private int countTotal;
    /// <summary>
    /// 取得道具數量
    /// </summary>
    private int countGet;


    [Header("文字介面：取得道具數量")]
    public Text textGet;
    [Header("文字介面：時間")]
    public Text textTime;



    [Header("結束畫面")]
    public CanvasGroup final;
    [Header("文字介面：結束畫面標題")]
    public Text textTital;
    private float gameTime = 45;
    
    
    
    
    #endregion





    #region method
    /// <summary>
    /// 生成道具
    /// </summary>
    /// <param name="prop">想生成的道具</param>
    /// <param name="count">想要生成的數量 + 隨機值(+-5)</param> 
    /// <returns>傳回生成幾顆</returns>
    private int CreateProp(GameObject prop, int count) 
    {
        int total = count + Random.Range(-5, 5);
        for (int i = 0; i < total; i++)
        {
            // 座標 = (隨機,1.5,隨機)
            Vector3 pos = new Vector3(Random.Range(-9, 9), 2f, Random.Range(-9, 9));
            // 生成 (物件,座標,角度)
            Instantiate(prop, pos, Quaternion.identity);
        }

        //傳回道具數量
        return total;


    }

    /// <summary>
    /// 時間倒數
    /// </summary>

    private void CountTime()
    {
        // 遊戲時間 遞減 一禎的時間
        gameTime -= Time.deltaTime;
        //更新倒數時間介面  float.ToString("f小數點位數") 轉為指定小數點位數的字串
        textTime.text = "倒數時間：" + gameTime.ToString("f1");
    }





    #endregion




    #region event


    private void Start()
    {
        countTotal = CreateProp(porps[0], 20); // 道具總數 = 生成道具(道具1號,指定數量)
        textGet.text = "道具數量：0/" + countTotal;

        countTotal = CreateProp(porps[1], 10); //  生成道具(道具2號,指定數量)

    }
    #endregion
    private void Update()
    {
        CountTime();
    }


}
