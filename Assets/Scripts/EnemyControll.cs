using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class EnemyControll : MonoBehaviour
{
    float direction = 1;
    float minX, maxX;
    public bool facingRight = true;
    public Animator am;
    public bool win;
    Transform menthitbox;
    public LayerMask layerMask;
    public float Mentspeed;
    public float maxPos;

    void Start()
    {
        minX = transform.position.x;
        maxX = minX + maxPos;
        am = GetComponent<Animator>();
        menthitbox = GameObject.Find(this.name + "/menthitbox").transform;
    }

    void Update()
    {
        win = Physics2D.Linecast(transform.position, menthitbox.position, layerMask);
        am.SetBool("winner", win);

        if (win)
        {
            direction = 0;
        }
            Vector3 currPos = transform.position;

            if (currPos.x >= maxX)
            {
                direction = -1*Mentspeed;
            }
            if (currPos.x <= minX)
            {
                direction = Mentspeed;
            }
            transform.Translate(new Vector3(direction * 5f * Time.deltaTime, 0, 0));
            if ((direction < 0) && (facingRight))
            {
                Flip();
            }
            if ((direction > 0) && (!facingRight))
            {
                Flip();
            }
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
