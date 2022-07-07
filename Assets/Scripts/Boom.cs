using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boom : Rob
{
    public GameObject fx;
    public Transform tr;
    Animator animator;
    public AudioClip aBomNo;
    private void Start() {
        animator = GetComponent<Animator>();
    }
    public void Bang(Vector3 pos, bool flag = false){
        var hits = Physics2D.CircleCastAll(pos, 3, Vector2.zero);
        foreach(var hit in hits){
            if(hit.collider == null) continue;
            if(hit.transform.tag == Config.TAG_GOLD || hit.transform.tag == Config.TAG_MOUSE){
                Destroy(hit.transform.gameObject);
                AudioManager.Instance.audioSource.PlayOneShot(aBomNo);
            }
            else{
                if(hit.transform.tag == Config.TAG_BOOM){
                    hit.transform.tag = Config.TAG_GOLD;
                    // StartCoroutine(Doi(hit));
                    hit.transform.GetComponent<Boom>().Bang(hit.point, true);
                }
            }
        }
        if(flag){
            // animator.Play("No");
            // Instantiate(fx, tr.position, transform.rotation);
            Destroy(gameObject);
        }
        animator.Play("No");
        Instantiate(fx, tr.position, transform.rotation);
    }
    // IEnumerator Doi(RaycastHit2D hit){
    //     yield return new WaitForSeconds(1f);
    //     hit.transform.GetComponent<Boom>().Bang(hit.point, true);
    // }
}
