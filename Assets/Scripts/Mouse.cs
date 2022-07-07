using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouse : Rob
{
    Vector3 start;
    Vector3 end;
    public float speed;
    public SpriteRenderer spriteRenderer;
    
    private void Start() {
        start = transform.position;
        start += Vector3.left * 2;
        end = transform.position;
        end += Vector3.right * 2;
    }
    void Update()
    {
        if(Check(start) || Check(end)){
            speed *= -1;
            spriteRenderer.flipX = !spriteRenderer.flipX;
        }
        transform.Translate(Vector3.right * speed * Time.deltaTime);
    }
    bool Check(Vector3 point){
        return Mathf.Floor(point.x) == Mathf.Floor(transform.position.x);
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "MocCau"){
            speed = 0;
        }
    }
}
