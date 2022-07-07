using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderPlayer : MonoBehaviour
{
    public static ColliderPlayer Instance;
    public AudioClip anDollar;
    private Transform Rob;
    private void Awake() {
        Instance = this;
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == Config.TAG_GOLD || other.gameObject.tag == Config.TAG_MOUSE || other.gameObject.tag == Config.TAG_BOOM){
            // Pob.Instance.dollar += other.GetComponent<Rob>().dollar;
            // UiManager.Instance.SetDollar(Pob.Instance.dollar);
        }
    }
    public void Am(){
        AudioManager.Instance.audioSource.PlayOneShot(anDollar);
    }
}
