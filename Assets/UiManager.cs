using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    public static UiManager Instance;
    [SerializeField]
    public Text txtDollar;
    [SerializeField]
    public Text txtDollarCan;
    [SerializeField]
    public Text txtTime;
    [SerializeField]
    public Text txtTitle;
    public int timeRate;
    public bool isTimeRun = false;

    public void Awake() {
        Instance = this;
    }
    void Start()
    {
        SetDollarMucTieu(GameManagerMent.Instance.dollarCan);
    }

    // Update is called once per frame
    void Update()
    {
        
        if(GameManagerMent.Instance.isGamePlay){
            if(isTimeRun){
                timeRate = GameManagerMent.Instance.timePlayGame;
                StartCoroutine(DemThoiGian());
                isTimeRun = false;
            }
        }
        
    }
    public AudioClip aTimeUp;
    IEnumerator DemThoiGian(){
        while(timeRate > 0){
            timeRate -= 1;
            txtTime.text = timeRate.ToString() + "s";
            if(timeRate < 10){
                AudioManager.Instance.audioSource.PlayOneShot(aTimeUp);
                txtTime.color = Color.red;
            }
            else{
                txtTime.color = Color.white;
            }
            yield return new WaitForSeconds(1f);
        }
        if(timeRate <= 0){
            GameManagerMent.Instance.isGamePlay = false;
            GameManagerMent.Instance.isPlay = false;
        }
    }
    public void SetDollar(int dollar){
        txtDollar.text = $"${dollar}";
    }
    public void SetDollarMucTieu(int dollar){
        txtDollarCan.text = $"${dollar}";
    }
}
