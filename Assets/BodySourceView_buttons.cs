using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Kinect = Windows.Kinect;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class BodySourceView_buttons : MonoBehaviour
{
    public GameObject[] g;
    public Button pl;
    public Button ins;
    public Button ex;
    public Button bk;

    public Material BoneMaterial;
    public GameObject BodySourceManager;

    private Dictionary<ulong, GameObject> _Bodies = new Dictionary<ulong, GameObject>();
    private BodySourceManager _BodyManager;
    int count = 0;
    float lower = 0;
    float upper = 0;
    int y = 0;


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
        g = GameObject.FindGameObjectsWithTag("Panel");
        //g[1].SetActive(false);
        g[0].SetActive(false);
        count = 0;
    }

    void Update()
    {

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
                    count = 1;
                    Kinect.CameraSpacePoint pos_spine_mid = body.Joints[Kinect.JointType.SpineMid].Position;
                    Kinect.CameraSpacePoint pos_shoulder_right = body.Joints[Kinect.JointType.ShoulderRight].Position;
                    lower = pos_spine_mid.Y;
                    upper = pos_shoulder_right.Y;

                }


                if (!_Bodies.ContainsKey(body.TrackingId))
                    _Bodies[body.TrackingId] = CreateBodyObject(body.TrackingId);
                float val = height_right_hand(body);
                if (g[1].activeInHierarchy)
                {
                    if (val < lower)
                    {
                        y = 1;
                        enable(ex);
                        disable(pl);
                        disable(ins);
                    }
                    else if (val > upper)
                    {
                        y = 2;
                        enable(pl);
                        disable(ex);
                        disable(ins);
                    }
                    else
                    {
                        y = 3;
                        enable(ins);
                        disable(pl);
                        disable(ex);
                    }

                    int i = check_val(body);
                    if (i == 1)
                    {
                        i = 0;
                        //Invoke("Change_by_index", 50000);

                        if (y == 1)
                            click(ex);

                        else if (y == 3)
                        {
                            click(ins);

                        }
                        else if (y == 2)
                        {
                            click(pl);
                            StartCoroutine(corr());
                        }

                        //Change_by_index(3);

                    }
                }
                else if (g[0].activeInHierarchy)
                {
                    print("Instr");
                    y = 0;
                    disable(bk);
                    if (val < lower)
                    {
                        y = 4;
                        enable(bk);

                    }
                    int j = check_val(body);
                    if (j == 1)
                    {
                        //Invoke("Change_by_index", 50000);

                        if (y == 4)
                            click(bk);

                    }

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
        Kinect.CameraSpacePoint position = body.Joints[Kinect.JointType.HandRight].Position;
        Kinect.CameraSpacePoint shoulder = body.Joints[Kinect.JointType.ShoulderLeft].Position;
        float x_val = position.X;

        if (x_val <= shoulder.X)
        {
            //this.kinectSensor.Close();
            return 1;
        }
        else return 0;
    }


    private float height_right_hand(Kinect.Body body)
    {
        Kinect.CameraSpacePoint position = body.Joints[Kinect.JointType.HandRight].Position;
        return position.Y;
    }

    public void Change_by_index(int i)
    {
        SceneManager.LoadScene(i);
    }

    public IEnumerator corr2(Button g)
    {
        yield return new WaitForSeconds(3.0f);
        click(g);
    }
    public IEnumerator corr()
    {
        yield return new WaitForSeconds(1.0f);
        Change_by_index(1);
    }

    public void enable(Button g)
    {

        g.image.color = Color.blue;
    }
    public void disable(Button g)
    {
        g.image.color = Color.gray;
    }
    public void click(Button g)
    {
        g.onClick.Invoke();
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
