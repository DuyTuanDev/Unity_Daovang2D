using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManagerMent : MonoBehaviour
{
    public static GameManagerMent Instance;
    public int level;
    private const int countLevel = 7;
    public bool isEndLevel = false;
    public int timePlayGame;
    public bool isGameOver = false;
    public int dollarCan = 0;
    [SerializeField]
    private GameObject panelGameLv;
    public GameObject CanVas;
    [SerializeField]
    private Text txtTitlePanel;
    [SerializeField]
    private Text txtDollarPanel;
    [SerializeField]
    private Text txtDollarCanPanel;
    public Button btnGetLevel;
    public Button btnGetGameOver;
    public bool isGamePlay = false;
    public bool isPlay = false;
    public string[] scenes;
    GameObject Panel;
    public AudioClip aBtn;
    private void Awake() {
        Instance = this;
    }
    private void Start() {
        isGameOver = false;
        level = RandomLv();
        txtTitlePanel.text = $"Mục Tiêu";
        txtDollarPanel.text = "$" + Pob.Instance.dollar;
        txtDollarCanPanel.text = $"${dollarCan + 1500}";
        isEndLevel = false;
        isPlay = true;
        panelGameLv.SetActive(true);
        Time.timeScale = 0;
        StartCoroutine(LoadLevelOne());
    }
    public int RandomLv(){
        return Random.Range(1, countLevel+1);
    }
    private void Update() {
        UiManager.Instance.txtDollarCan.text = "$" + dollarCan;
        if(!isGamePlay && !isPlay){
            panelGameLv.SetActive(false);
            Time.timeScale = 1;
            KiemTra();
        }
    }
    public void KiemTra(){
        if(Pob.Instance.dollar < GameManagerMent.Instance.dollarCan){
            panelGameLv.SetActive(true);
            Time.timeScale = 0;
            isGameOver = true;
            if(isGameOver){
                txtTitlePanel.text = $"Game Over";
                txtDollarPanel.text = "$" + Pob.Instance.dollar;
                txtDollarPanel.color = Color.red;
                txtDollarCanPanel.text = "$" + dollarCan;
                btnGetGameOver.gameObject.SetActive(true);
                btnGetLevel.gameObject.SetActive(false);
            }
        }
        else{
            panelGameLv.SetActive(true);
            Time.timeScale = 0;
            isGameOver = false;
            if(!isGameOver){
                txtTitlePanel.text = $"YouWin (Next Level)";
                txtDollarPanel.text = "$" + Pob.Instance.dollar;
                txtDollarCanPanel.text = $"${dollarCan + 1500}";
            }
        }
    }
    IEnumerator LoadLevelOne(){
        SceneManager.LoadScene(level, LoadSceneMode.Additive);
        // SceneManager.LoadScene(6, LoadSceneMode.Additive);
        yield return new WaitForSeconds(0);
    }
    public void LoadLv(){
        AudioManager.Instance.audioSource.PlayOneShot(aBtn);
        StartCoroutine(LoadLvGame());
    }
    public Transform PobTf;
    public Transform PointGoc;
    IEnumerator LoadLvGame(){
        // countMucTieu++;
        panelGameLv.SetActive(false);
        Time.timeScale = 1;
        isPlay = true;
        yield return new WaitForSeconds(0f);
        // load dollar
        isGamePlay = true;
        SceneManager.UnloadSceneAsync(level);
        level = RandomLv();
        SceneManager.LoadScene(level, LoadSceneMode.Additive);
        dollarCan  += 1500;
        PobTf.position = PointGoc.position;
        
        txtDollarCanPanel.text = $"${dollarCan}";
        UiManager.Instance.isTimeRun = true;
    }
    public void GameOver(){
        AudioManager.Instance.audioSource.PlayOneShot(aBtn);
        SceneManager.LoadScene("GameOver");
    }
}
