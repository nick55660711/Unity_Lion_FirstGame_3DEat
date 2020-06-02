using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; // 場景管理API


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
    private float gameTime ;
    
    
    
    
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
        // if (countGet == countTotal) return; //如果取得所有壽司就跳出
        if (final.alpha == 0)
        {

        // 遊戲時間 遞減 一禎的時間
        gameTime -= Time.deltaTime;

            // 遊戲時間 = 數學.夾住(浮點數名稱,最小值,最大值)
            gameTime = Mathf.Clamp(gameTime, 0, 99);


            //更新倒數時間介面  float.ToString("f小數點位數") 轉為指定小數點位數的字串
            textTime.text = "倒數時間：" + gameTime.ToString("f1");

            Lose();

        }
    }
   
    
    /*
    public void TimeSubstract() 
    
    {
        gameTime -= 5;
        Mathf.Clamp(gameTime, 0, 99);
        textTime.text = "倒數時間：" + gameTime.ToString("f1");
        
    }
    public void itemGet(int item)
    {
        countGet = item;
        textGet.text = "道具數量："+ countGet + "/" + countTotal;
    }
    */ 
    
    public void itemGet(string prop)
    {
        if (prop == "食物")
        {
        countGet ++;
        textGet.text = "道具數量："+ countGet + "/" + countTotal;
            Win(); //呼叫勝利方法

        }
        else if (prop == "酒")
        {
            if (final.alpha == 0)
            {
                gameTime -= 5;
                textTime.text = "倒數時間：" + gameTime.ToString("f1");
            }
        }

    }

    /// <summary>
    /// 勝利
    /// </summary>
    private void Win()
    {
        if (countGet == countTotal) //如果壽司數量 = 壽司總數
        {
            final.alpha = 1; //顯示結束畫面、啟動互動、啟動遮擋
            final.interactable = true;
            final.blocksRaycasts = true;
            textTital.text = "恭喜你吃完所有壽司惹～";
        }
    }
    
    
    /// <summary>
    /// 失敗
    /// </summary>
    private void Lose() 
    { 
        if(gameTime <= 0)
        {
            final.alpha = 1; //顯示結束畫面、啟動互動、啟動遮擋
            final.interactable = true;
            final.blocksRaycasts = true;
            textTital.text = "輸惹 QAQ～";
            FindObjectOfType<Player>().enabled = false; // 取得玩家.啟動 = false


        }

    }
    
    /// <summary>
    /// 重新遊戲
    /// </summary>
    public void ReStart() 
    {
        SceneManager.LoadScene("遊戲場景");




    }

    /// <summary>
    /// 離開遊戲
    /// </summary>

    public void Quit() 
    {
        Application.Quit(); //應用程式.離開()

    }








    #endregion




    #region event


    private void Start()
    {
        countTotal = CreateProp(porps[0], 9); // 道具總數 = 生成道具(道具1號,指定數量)
        textGet.text = "道具數量：" + countGet + "/" + countTotal;
        gameTime = 5 + 4*countTotal;


        CreateProp(porps[1], 10); //  生成道具(道具2號,指定數量)

    }
    #endregion
    private void Update()
    {

        CountTime();
    }

}
