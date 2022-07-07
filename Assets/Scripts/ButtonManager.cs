using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    public static ButtonManager Instance;
    public AudioClip aBtn;
    public bool isInput = true;
    private bool amThanh = true;
    public GameObject PausePanel;
    Vector3 start;
    public Text txtDiem;
    public GameObject anhSang;
    private void LateUpdate() {
        if(anhSang == null) return;
        anhSang.transform.Rotate(0, 0, 1f);
    }
    private void Awake() {
        // start = PausePanel.transform.position;
        Instance = this;
    }
    private void Start() {
        if(txtDiem !=null) txtDiem.text = "$" + PlayerPrefs.GetInt("Diem");
        // PausePanel.transform.position =  start * 50;
    }
    public void PlayGame(){
        AudioManager.Instance.audioSource.PlayOneShot(aBtn);
        SceneManager.LoadScene("MainGame");
        PlayerPrefs.SetInt("Diem", 0);
    }
    public void Facebook(){
        AudioManager.Instance.audioSource.PlayOneShot(aBtn);

        Application.OpenURL("https://www.facebook.com/phamduy.tuan.04092001");
    }
    public void AmThanh(){
        if(amThanh){
            AudioManager.Instance.audioSource.volume = 0;
            amThanh = !amThanh;
        }
        else{
            AudioManager.Instance.audioSource.volume = 1;
            AudioManager.Instance.audioSource.PlayOneShot(aBtn);
            amThanh = !amThanh;
        }
    }
    public void ExitGame(){
        AudioManager.Instance.audioSource.PlayOneShot(aBtn);
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
    public void LoadLvReload(){
        AudioManager.Instance.audioSource.PlayOneShot(aBtn);
        SceneManager.LoadScene("MainGame");
        PlayerPrefs.SetInt("Diem", 0);
        // panelGameLv.SetActive(false);
    }
    public void PauseGame(){
        AudioManager.Instance.audioSource.PlayOneShot(aBtn);
        PausePanel.SetActive(true);
        // PausePanel.transform.position = start;
        Time.timeScale = 0;
        isInput = false;
    }
    public void TiepTuc(){
        AudioManager.Instance.audioSource.PlayOneShot(aBtn);
        // PausePanel.transform.position = start * 50;
        PausePanel.SetActive(false);
        isInput = true;
        Time.timeScale = 1;
    }
    public void ReloadScene(){
        SceneManager.LoadScene("Loading");
    }
}
