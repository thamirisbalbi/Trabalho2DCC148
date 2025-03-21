using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class CameraFollowController : MonoBehaviour
{
   //camera segue bola
    public static CameraFollowController instance;
    [SerializeField] private ActiveVector activeVector; //classe que decide qual eixo vai ser seguido

    private GameObject followTarget; //target de referência para seguir
    private Vector3 offset; //entre camera e bola
    private Vector3 pos; //posição camera
    
    private void Awake()
    {
        if(instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }
    
    public void SetAlvo(GameObject target)
    {
        followTarget = target;
        offset = followTarget.transform.position - transform.position; //inicia offset
        pos = transform.position; //muda a pos camera
    }
    void LateUpdate()
    {
        if(followTarget)
        {
            if(activeVector.GetX()) //se permite mudança de pos da camera
            {
                pos.x = followTarget.transform.position.x - offset.x;
            }

            if(activeVector.GetY())
            {
                pos.y = followTarget.transform.position.y - offset.y;
            }

            if(activeVector.GetZ())
            {
                pos.z = followTarget.transform.position.z - offset.z;
            }

            transform.position = pos;
        }
    }
}

    [System.Serializable]
    public class ActiveVector
    {
        [SerializeField] private bool x,y,z;

        public bool GetX()
        {
            return x;
        }
        public bool GetY()
        {
            return y;
        }
        public bool GetZ()
        {
            return z;
        }
    }