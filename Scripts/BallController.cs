using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]

public class BallController : MonoBehaviour
{
    public static BallController instance;

    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private float maxForce, forceMod; //limite e modificador de força (multiplicador)
    [SerializeField] private LayerMask rayLayer;
    [SerializeField] private float velRed; // vel de redução da bola ao entrar no buraco

    private float force;
    private Rigidbody rb;
    private Vector3 startPos, endPos;
    private bool canShoot = false;
    private bool ballStatic = true;
    private Vector3 direction; //direção que a bola pode ter
    private bool isShrinking = false;
    AudioControllerGame audioManager;


    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioControllerGame>();
        if(instance == null)
        {
            instance = this;
        }

        else
        {
            Destroy(gameObject); //impede que tenha multiplos controllers a cada level
        }

        rb = GetComponent<Rigidbody>();
    }

    void Start()
    {
       CameraFollowController.instance.SetAlvo(gameObject);
    }

    
    void Update()
    {

        if(Mouse.current.leftButton.wasPressedThisFrame && !canShoot) //checo se posso clicar e escolher direção
        {
            startPos = ClickedPoint();
            lineRenderer.gameObject.SetActive(true);
            lineRenderer.SetPosition(0, lineRenderer.transform.localPosition);
        }

        if(Mouse.current.leftButton.isPressed) 
        {
            endPos = ClickedPoint();
            endPos.y = lineRenderer.transform.position.y;
            force = Mathf.Clamp(Vector3.Distance(endPos, startPos) * forceMod, 0, maxForce);
            lineRenderer.SetPosition(1, transform.InverseTransformPoint(endPos)); 
        
        }

        if(Mouse.current.leftButton.wasReleasedThisFrame)
        {
            audioManager.PlaySFX(audioManager.hit);
            canShoot = true;
            lineRenderer.gameObject.SetActive(false);

        }


        if(rb.linearVelocity == Vector3.zero && !ballStatic)
        {
            ballStatic = true;
            rb.angularVelocity = Vector3.zero;
        }

        if(isShrinking == true) //deletar
        {
            transform.localScale = Vector3.Lerp(transform.localScale, Vector3.zero, velRed * Time.deltaTime);
            rb.linearVelocity = Vector3.zero;
        }
        if(transform.localScale.x < 0.01)
        {
                Destroy(gameObject);
        }

    }

    private void FixedUpdate()
    {
        if(canShoot)
        {
            canShoot = false;
            direction = startPos - endPos;
            rb.AddForce(direction * force, ForceMode.Impulse);
            force = 0;
            startPos = endPos = Vector3.zero;
            ballStatic = false;
        }
    }

    Vector3 ClickedPoint()
    {
        Vector3 pos = Vector3.zero;
        var ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
        RaycastHit hit = new RaycastHit();
        if(Physics.Raycast(ray, out hit, Mathf.Infinity, rayLayer))
        {
            pos = hit.point;
        }
        return pos; 
    }

    private void OnTriggerStay(Collider other)
    { //pensar em outra forma
        if(other.CompareTag("Hole"))
        {
            isShrinking = true;
        }
    }


}
