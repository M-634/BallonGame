using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class select : MonoBehaviour
{
    //Nameはステージ名が決まったら変更する

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void onClick_1Name()
    {
        SceneManager.LoadScene("SampleScene");
    }
    //ここから下のやつはステージ追加時にpulicにして追加する
    void onClick_2Name()
    {

    }
    void onClick_3Name()
    {

    }
}
