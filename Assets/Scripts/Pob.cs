using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class Pob : MonoBehaviour
{
    public static Pob Instance;
    public enum PobState{
        ROTATION,
        SHOOT,
        REWIND
    }
    private Animator animator;
    public PobState pobState = PobState.ROTATION;
    private int angle;
    #region Serialize
    [SerializeField]
    private int rotateSpeed = 1;
    [SerializeField]
    private float speed;
    #endregion
    private Vector3 origin;
    [SerializeField]
    private Transform pointRob;
    public AudioClip aKeoTha;
    private void Awake() {
        Instance = this;
        animator = transform.root.GetComponent<Animator>();
        origin = transform.position;
    }
    void Update()
    {
        
    }
    private void FixedUpdate() {
        PlayerPrefs.SetInt("Diem", dollar);
        // Debug.Log(origin);
        switch(pobState){
            case PobState.ROTATION:
                animator.Play("Rotation");
                // InputMove();
                
                angle += rotateSpeed;
                if(angle > 70 || angle < -80){
                    rotateSpeed *= -1;
                }
                transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
                break;
            case PobState.SHOOT:
                animator.Play("Tha");
                
                transform.Translate(Vector3.down * speed * Time.deltaTime);
                if(Mathf.Abs(transform.position.x) > 8 || transform.position.y < -4)
                pobState = PobState.REWIND;
                break;
            case PobState.REWIND:
                animator.Play("Keo");
                transform.Translate(Vector3.up * (speed - slowDown) * Time.deltaTime);
                // if(Mathf.Floor(transform.position.x) == Mathf.Floor(origin.x) && Mathf.Floor(transform.position.y) == Mathf.Floor(origin.y)){
                //     slowDown = 0;
                //     transform.position = origin;
                //     pobState = PobState.ROTATION;
                // }
                
                if(Mathf.Floor(transform.position.y) == Mathf.Floor(origin.y)){
                    if(Rob != null){
                        slowDown = 0;
                        flagRob = false;
                        ColliderPlayer.Instance.Am();
                        Pob.Instance.dollar += Rob.GetComponent<Rob>().dollar;
                        UiManager.Instance.SetDollar(Pob.Instance.dollar);
                        Destroy(Rob.gameObject);
                    }
                    shoot = true;
                    transform.position = origin;
                    pobState = PobState.ROTATION;
                }
                
                // Debug.Log(Mathf.Floor(transform.position.x) + "," + Mathf.Floor(transform.position.y));
                break;
        }
    }
    // void InputMove(){
    //     if(GameManagerMent.Instance.isGamePlay && ButtonManager.Instance.isInput){
    //         // if(Input.GetKeyDown(KeyCode.Space)){
    //         //     // AudioManager.Instance.audioSource.PlayOneShot(aKeoTha); 
    //         //     pobState = PobState.SHOOT;
    //         // }
    //         // if(Input.GetMouseButtonDown(0)){
    //         //     pobState = PobState.SHOOT;
    //         // }
    //     }
    // }
    public bool shoot = true;
    public void SetShoot(){
        if(GameManagerMent.Instance.isGamePlay && ButtonManager.Instance.isInput){
            if(shoot){
                // AudioManager.Instance.audioSource.PlayOneShot(aKeoTha); 
                pobState = PobState.SHOOT;
                shoot = false;
            }
        }
    }
    public void SetREWIND(){
        pobState = PobState.REWIND;
    }
    private float slowDown;
    public int dollar;
    private Transform Rob;
    private bool flagRob;
    private void OnTriggerEnter2D(Collider2D other) {
        if(flagRob) return;
        flagRob = true;
        Rob = other.transform;
        
        if(other.tag == Config.TAG_BOOM){
            Rob.tag = this.tag;
            Rob.GetComponent<Boom>().Bang(Rob.position);
            // Destroy(other.gameObject);
            // return;
        }
        Rob.SetParent(transform);
        Rob.transform.localPosition = pointRob.localPosition;
        slowDown = Rob.GetComponent<Rob>().slowDown;
        // dollar += Rob.GetComponent<Rob>().dollar;
        // Destroy(Rob.GetComponent<Rob>());
        pobState = PobState.REWIND;
    }
}
