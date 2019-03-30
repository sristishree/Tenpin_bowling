using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Kinect = Windows.Kinect;
using UnityEngine.SceneManagement;


public class BodySourceView_pos: MonoBehaviour
{
    public GameObject g;
    public GameObject g2;

    public Material BoneMaterial;
    public GameObject BodySourceManager;

    private SliderControlPos SCS;
    private print_val svp;

    private Dictionary<ulong, GameObject> _Bodies = new Dictionary<ulong, GameObject>();
    private BodySourceManager _BodyManager;
    List<float> pos = new List<float>();
    int count = 0;
    float lower = 0;
    float upper = 0;
    float y = 0;
 

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
    void Start()
    {
        g = GameObject.FindGameObjectWithTag("Player");
        g2 = GameObject.FindGameObjectWithTag("text");
        SCS = g.GetComponent<SliderControlPos>();
        svp = g2.GetComponent<print_val>();
        svp.Set("Body has not been detected yet!!");

        count = 0;
    }

    void Update()
    {

        SCS = g.GetComponent<SliderControlPos>();
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
        foreach (var body in data)
        {
            if (body == null)
            {
                continue;
            }

            if (body.IsTracked)
            {
                trackedIds.Add(body.TrackingId);
            }
        }

        List<ulong> knownIds = new List<ulong>(_Bodies.Keys);

        // First delete untracked bodies
        foreach (ulong trackingId in knownIds)
        {
            if (!trackedIds.Contains(trackingId))
            {
                Destroy(_Bodies[trackingId]);
                _Bodies.Remove(trackingId);
            }
        }

        foreach (var body in data)
        {
            if (body == null)
            {
                continue;
            }

            if (body.IsTracked)
            {
                if (count == 0)
                {
                    svp.Set("Body detected");
                    count = 1;
                    Kinect.CameraSpacePoint pos_shoulder_left = body.Joints[Kinect.JointType.ShoulderLeft].Position;
                    Kinect.CameraSpacePoint pos_shoulder_right = body.Joints[Kinect.JointType.ShoulderRight].Position;
                    lower = pos_shoulder_left.X;
                    upper = pos_shoulder_right.X;
                    upper = upper + (upper - lower);
                    
                }
                

                if (!_Bodies.ContainsKey(body.TrackingId))
                    _Bodies[body.TrackingId] = CreateBodyObject(body.TrackingId);
                float val = height_right_hand(body);
                if (val < lower)
                {
                    SCS.Valuefun(-26);
                    svp.Set("-26.00000");
                }
                else if (val > upper)
                {
                    SCS.Valuefun(26);
                    svp.Set("26.00000");
                }
                else
                {
                    y = 26 + ((val - upper) * (52 / (upper - lower)));
                    SCS.Valuefun(y);
                    svp.Set(y.ToString());
                    
                    
                }
                int i = check_val2(body);
                if (i == 1)
                {
                    _BodyManager.OnApplicationQuit();
                    //Invoke("Change_by_index", 50000);
                    svp.Set("The final position has been set to " + y);
                    StartCoroutine(corr());
                    //Change_by_index(3);
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

            if (_BoneMap.ContainsKey(jt))
            {
                targetJoint = body.Joints[_BoneMap[jt]];
            }

            Transform jointObj = bodyObject.transform.Find(jt.ToString());
            jointObj.localPosition = GetVector3FromJoint(sourceJoint);

            LineRenderer lr = jointObj.GetComponent<LineRenderer>();
            if (targetJoint.HasValue)
            {
                lr.SetPosition(0, jointObj.localPosition);
                lr.SetPosition(1, GetVector3FromJoint(targetJoint.Value));
                lr.SetColors(GetColorForState(sourceJoint.TrackingState), GetColorForState(targetJoint.Value.TrackingState));
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

        if (z_val <= (shoulder_left.Z-diff))
        {
            //this.kinectSensor.Close();
            return 1;
        }
        else return 0;
    }

    private float height_right_hand(Kinect.Body body)
    {
        Kinect.CameraSpacePoint position = body.Joints[Kinect.JointType.HandRight].Position;
        return position.X;
    }

    public void Change_by_index(int i)
    {
        SceneManager.LoadScene(i);
    }

    public IEnumerator corr()
    {
        yield return new WaitForSeconds(2.0f);
        Change_by_index(3);
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
