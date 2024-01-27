using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Timeline;

public class PlayerAnimator : MonoBehaviour
{
    [SerializeField] private SpriteRenderer face;
    //[SerializeField] private SpriteRenderer face;
    //[SerializeField] private SpriteRenderer faceUp;
    [SerializeField] private Sprite faceSides;
    [SerializeField] private Sprite faceUp;
    [SerializeField] private Sprite faceDown;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeSprite(Vector2 aimDir){
        //print(aimDir[0]);
        float aimAngle = Vector2.Angle(aimDir, Vector2.right);
        print(aimAngle);
        Debug.DrawLine(Vector2.zero, aimDir, Color.magenta);
        Debug.DrawLine(Vector2.zero, Vector2.right, Color.blue);
        //print(aimAngle * Mathf.Sign(aimDir[1]));
        aimAngle = aimAngle * Mathf.Sign(aimDir[1]);
        //print(aimDir[1]);
        //Debug.DrawLine(Vector2.zero, Vector2.aimAngle, Color.green);
        if (-45 < aimAngle && aimAngle < 45){
            face.sprite = faceSides;
            face.flipX = true;

        }else if (45 < aimAngle && aimAngle < 135){
            face.sprite = faceUp;
        //} else if (135 < aimAngle && aimAngle < 225){
        } else if ((135 < aimAngle && aimAngle <= 180) || (aimAngle < -135 && aimAngle >= -180)){
            face.sprite = faceSides;
            face.flipX = false;

        }else if (-135 < aimAngle && aimAngle < -45){
        //}else if (225 < aimAngle && aimAngle < 315){
            print("do i happen");
            face.sprite = faceDown;


        }
        /*
        if (aimingDirection[0] > 0){
            face.flipX = true;
        }else{
            face.flipX = false;
        }*/


    }
}
