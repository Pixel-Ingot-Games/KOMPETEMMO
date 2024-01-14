using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityStandardAssets.Vehicles.Aeroplane;

public class NetworkedPlane : MonoBehaviourPunCallbacks,IPunObservable
{
    public float Vertical;
    public float Horizontal;
    public float Input1 { get; set; }
    public Vector3 Velocity { get; set; }
    public float Input2;
    public bool AirBreak;
    public Vector3 InitialPosition;
    public Vector3 InitialRotation;
    private Rigidbody m_Rigidbody;
    private PhotonView view;
    private AeroplaneController JetScript;
   

    // Start is called before the first frame update
    void Start()
    {
        view = GetComponent<PhotonView>();
        m_Rigidbody = GetComponent<Rigidbody>();
        JetScript = GetComponent<AeroplaneController>();
        InitialPosition = transform.position;
        InitialRotation = transform.eulerAngles;
        if (!photonView.IsMine)
        {
            this.enabled = false;
        }
    }
    void FixedUpdate()
    {
        PositionControl();
    }
    void PositionControl()
    {
        JetPosition();
    }
    void JetPosition()
    {
        Input1 = JetScript.RollInput;
        Input2 = JetScript.PitchInput;
        AirBreak = JetScript.AirBrakes;
        Horizontal = JetScript.YawInput;
        Vertical = JetScript.ThrottleInput;
    }
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
                stream.SendNext(Input1);
                stream.SendNext(Input2);
                stream.SendNext(AirBreak);
                stream.SendNext(Horizontal);
                stream.SendNext(Vertical);
        }
        else
        {
                Input1 = (float)stream.ReceiveNext();
                Input2 = (float)stream.ReceiveNext();
                AirBreak = (bool)stream.ReceiveNext();
                Horizontal = (float)stream.ReceiveNext();
                Vertical = (float)stream.ReceiveNext();
        }
    }
}
