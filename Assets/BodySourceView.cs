using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Kinect = Windows.Kinect;
using UnityEngine.SceneManagement;


public class BodySourceView : MonoBehaviour 
{
    public GameObject g;
    public GameObject g2;

    public Material BoneMaterial;
    public GameObject BodySourceManager;

    private SliderControlSpeed SCS;
    private print_val svp;
    int sp = 0;

    private Dictionary<ulong, GameObject> _Bodies = new Dictionary<ulong, GameObject>();
    private BodySourceManager _BodyManager;
    List<float> pos = new List<float>();
    int count = 0;
    float lower = 0;
    float upper = 0;
    float step = 0;

    private Dictionary<Kinect.JointType, Kinect.JointType> _BoneMap = new Dictionary<Kinect.JointType, Kinect.JointType>()
    {
        { Kinect.JointType.FootLeft, Kinect.JointType.AnkleLeft },
        { Kinect.JointType.AnkleLeft, Kinect.JointType.KneeLeft },
        { Kinect.JointType.KneeLeft, Kinect.JointType.HipLeft },
        { Kinect.JointType.HipLeft, Kinect.JointType.SpineBase },
        
        { Kinect.JointType.FootRight, Kinect.JointType.AnkleRight },
        { Kinect.JointType.AnkleRight, Kinect.JointType.KneeRight },
        { Kinect.JointType.KneeRight, Kinect.JointType.HipRight },
        { Kinect.JointType.HipRight, Kinect.JointType.SpineBase },
        
        { Kinect.JointType.HandTipLeft, Kinect.JointType.HandLeft },
        { Kinect.JointType.ThumbLeft, Kinect.JointType.HandLeft },
        { Kinect.JointType.HandLeft, Kinect.JointType.WristLeft },
        { Kinect.JointType.WristLeft, Kinect.JointType.ElbowLeft },
        { Kinect.JointType.ElbowLeft, Kinect.JointType.ShoulderLeft },
        { Kinect.JointType.ShoulderLeft, Kinect.JointType.SpineShoulder },
        
        { Kinect.JointType.HandTipRight, Kinect.JointType.HandRight },
        { Kinect.JointType.ThumbRight, Kinect.JointType.HandRight },
        { Kinect.JointType.HandRight, Kinect.JointType.WristRight },
        { Kinect.JointType.WristRight, Kinect.JointType.ElbowRight },
        { Kinect.JointType.ElbowRight, Kinect.JointType.ShoulderRight },
        { Kinect.JointType.ShoulderRight, Kinect.JointType.SpineShoulder },
        
        { Kinect.JointType.SpineBase, Kinect.JointType.SpineMid },
        { Kinect.JointType.SpineMid, Kinect.JointType.SpineShoulder },
        { Kinect.JointType.SpineShoulder, Kinect.JointType.Neck },
        { Kinect.JointType.Neck, Kinect.JointType.Head },
    };
    void Start ()
    {
        g = GameObject.FindGameObjectWithTag("Player");
        g2 = GameObject.FindGameObjectWithTag("text");
        SCS = g.GetComponent<SliderControlSpeed>();
        svp = g2.GetComponent<print_val>();
        svp.Set("Body not yet detected!!");

        count = 0;
    }

    void Update () 
    {
        
        SCS = g.GetComponent<SliderControlSpeed>();
        if (BodySourceManager == null)
        {
            return;
        }
        
        _BodyManager = BodySourceManager.GetComponent<BodySourceManager>();
        if (_BodyManager == null)
        {
            return;
        }
        
        Kinect.Body[] data = _BodyManager.GetData();
        if (data == null)
        {
            return;
        }
        
        List<ulong> trackedIds = new List<ulong>();
        foreach(var body in data)
        {
            if (body == null)
            {
                continue;
              }
                
            if(body.IsTracked)
            {
                trackedIds.Add (body.TrackingId);
            }
        }

        List<ulong> knownIds = new List<ulong>(_Bodies.Keys);
        
        // First delete untracked bodies
        foreach(ulong trackingId in knownIds)
        {
            if(!trackedIds.Contains(trackingId))
            {
                Destroy(_Bodies[trackingId]);
                _Bodies.Remove(trackingId);
            }
        }

        foreach(var body in data)
        {
            if (body == null)
            {
                continue;
            }
            
            if(body.IsTracked)
            {
                if (count == 0)
                {
                    svp.Set("Body detected");
                    count = 1;
                    Kinect.CameraSpacePoint pos_spine_base = body.Joints[Kinect.JointType.SpineBase].Position;
                    Kinect.CameraSpacePoint pos_head = body.Joints[Kinect.JointType.Head].Position;
                    lower = pos_spine_base.Y;
                    upper = pos_head.Y;
                    step = (upper - lower) / 10;
                    //print(step);
                }
                //print("Step is ");
                //print(step);

                if (!_Bodies.ContainsKey(body.TrackingId))
                    _Bodies[body.TrackingId] = CreateBodyObject(body.TrackingId);
                float val = height_left_hand(body);
                //print(val);
                if (val < lower)
                {
                    SCS.Valuefun(0);
                    sp = 0;
                    svp.Set("Speed is set to 0");
                }
                if (val > upper)
                {
                    sp = 10;
                    SCS.Valuefun(10);
                    svp.Set("Speed is set to 10");
                }
                if (val > (lower) && val < (lower + step))
                {
                    sp = 1;
                    SCS.Valuefun(1);
                    svp.Set("Speed is set to 1");
                }

                if (val > (lower + step) && val < (lower + 2 * step))
                {
                    sp = 2;
                    SCS.Valuefun(2);
                    svp.Set("Speed is set to 2");
                }
                if (val > (lower + 2 * step) && val < (lower + 3 * step))
                {
                    sp = 3;
                    SCS.Valuefun(3);
                    svp.Set("Speed is set to 3");
                }
                if (val > (lower + 3 * step) && val < (lower + 4 * step))
                {
                    sp = 4;
                    SCS.Valuefun(4);
                    svp.Set("Speed is set to 4");
                }
                if (val > (lower + 4 * step) && val < (lower + 5 * step))
                {
                    sp = 5;
                    SCS.Valuefun(5);
                    svp.Set("Speed is set to 5");
                }
                if (val > (lower + 5 * step) && val < (lower + 6 * step))
                {
                    sp = 6;
                    SCS.Valuefun(6);
                    svp.Set("Speed is set to 6");
                }
                if (val > (lower + 6 * step) && val < (lower + 7 * step))
                {
                    sp = 7;
                    SCS.Valuefun(7);
                    svp.Set("Speed is set to 7");
                }
                if (val > (lower + 7 * step) && val < (lower + 8 * step))
                {
                    sp = 8;
                    SCS.Valuefun(8);
                    svp.Set("Speed is set to 8");
                }
                if (val > (lower + 8 * step) && val < (lower + 9 * step))
                {
                    sp = 9;
                    SCS.Valuefun(9);
                    svp.Set("Speed is set to 9");
                }
                if (val > (lower + 9 * step) && val < upper)
                {
                    sp = 9;
                    SCS.Valuefun(10);
                    svp.Set("Speed is set to 10");
                }
                
                int i = check_val2(body);
                if (i == 1)
                {
                    _BodyManager.OnApplicationQuit();
                    //Invoke("Change_by_index", 50000);
                    svp.Set("Final speed is " + sp);
                    StartCoroutine(corr());
                    //Change_by_index(2);
                }
                
                RefreshBodyObject(body, _Bodies[body.TrackingId]);
            }
        }
    }
    
    private GameObject CreateBodyObject(ulong id)
    {
        GameObject body = new GameObject("Body:" + id);
        
        for (Kinect.JointType jt = Kinect.JointType.SpineBase; jt <= Kinect.JointType.ThumbRight; jt++)
        {
            GameObject jointObj = GameObject.CreatePrimitive(PrimitiveType.Cube);
            
            LineRenderer lr = jointObj.AddComponent<LineRenderer>();
            lr.SetVertexCount(2);
            lr.material = BoneMaterial;
            lr.SetWidth(0.05f, 0.05f);
            
            jointObj.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
            jointObj.name = jt.ToString();
            jointObj.transform.parent = body.transform;
        }
        
        return body;
    }
    
    private void RefreshBodyObject(Kinect.Body body, GameObject bodyObject)
    {
        for (Kinect.JointType jt = Kinect.JointType.SpineBase; jt <= Kinect.JointType.ThumbRight; jt++)
        {
            Kinect.Joint sourceJoint = body.Joints[jt];
            Kinect.Joint? targetJoint = null;
            
            if(_BoneMap.ContainsKey(jt))
            {
                targetJoint = body.Joints[_BoneMap[jt]];
            }
            
            Transform jointObj = bodyObject.transform.Find(jt.ToString());
            jointObj.localPosition = GetVector3FromJoint(sourceJoint);
            
            LineRenderer lr = jointObj.GetComponent<LineRenderer>();
            if(targetJoint.HasValue)
            {
                lr.SetPosition(0, jointObj.localPosition);
                lr.SetPosition(1, GetVector3FromJoint(targetJoint.Value));
                lr.SetColors(GetColorForState (sourceJoint.TrackingState), GetColorForState(targetJoint.Value.TrackingState));
            }
            else
            {
                lr.enabled = false;
            }
        }
    }
    private int check_val(Kinect.Body body)
    {
        Kinect.CameraSpacePoint position = body.Joints[Kinect.JointType.HandLeft].Position;
        float z_val = position.Z;
        //print(z_val);
        if (z_val < 1.40)
        {
            //this.kinectSensor.Close();
            return 1;
        }
        else return 0;
    }
    private int check_val2(Kinect.Body body)
    {
        Kinect.CameraSpacePoint position = body.Joints[Kinect.JointType.HandLeft].Position;
        Kinect.CameraSpacePoint shoulder_left = body.Joints[Kinect.JointType.ShoulderLeft].Position;
        Kinect.CameraSpacePoint shoulder_right = body.Joints[Kinect.JointType.ShoulderRight].Position;
        float diff = shoulder_right.X - shoulder_left.X;
        float z_val = position.Z;

        if (z_val <= (shoulder_left.Z - diff))
        {
            //this.kinectSensor.Close();
            return 1;
        }
        else return 0;
    }

    private float height_left_hand(Kinect.Body body)
    {
        Kinect.CameraSpacePoint position = body.Joints[Kinect.JointType.HandRight].Position;
        //pos.Add(position.Y);
        return position.Y;
    }

    public void Change_by_index(int i)
    {
        SceneManager.LoadScene(i);
    }
    public IEnumerator corr()
    {
        yield return new WaitForSeconds(2.0f);
        Change_by_index(2);
    }

    private static Color GetColorForState(Kinect.TrackingState state)
    {
        switch (state)
        {
        case Kinect.TrackingState.Tracked:
            return Color.green;

        case Kinect.TrackingState.Inferred:
            return Color.red;

        default:
            return Color.black;
        }
    }
    
    private static Vector3 GetVector3FromJoint(Kinect.Joint joint)
    {
        return new Vector3(joint.Position.X * 10, joint.Position.Y * 10, joint.Position.Z * 10);
    }
}
