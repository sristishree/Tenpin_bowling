using UnityEngine;
using System.Collections;
public class Skittles : MonoBehaviour {
    GameObject[] skittle;
    Vector3 in_pos0, in_pos1, in_pos2, in_pos3, in_pos4, in_pos5, in_pos6, in_pos7, in_pos8, in_pos9;
    Vector3 in_rot0, in_rot1, in_rot2, in_rot3, in_rot4, in_rot5, in_rot6, in_rot7, in_rot8, in_rot9;
    Vector3 rot0, rot1, rot2, rot3, rot4, rot5, rot6, rot7, rot8, rot9;
    public Quaternion r0, r1, r2, r3, r4, r5, r6, r7, r8, r9;
    bool[] fallen=new bool[10];
    
    public int counter = 0;
    // Use this for initialization
    public void initial() {
        skittle = GameObject.FindGameObjectsWithTag("skittle");
        in_pos0 = skittle[0].transform.position;
        in_pos1 = skittle[1].transform.position;
        in_pos2 = skittle[2].transform.position;
        in_pos3 = skittle[3].transform.position;
        in_pos4 = skittle[4].transform.position;
        in_pos5 = skittle[5].transform.position;
        in_pos6 = skittle[6].transform.position;
        in_pos7 = skittle[7].transform.position;
        in_pos8 = skittle[8].transform.position;
        in_pos9 = skittle[9].transform.position;

        r0 = skittle[0].transform.rotation;
        r1 = skittle[1].transform.rotation;
        r2 = skittle[2].transform.rotation;
        r3 = skittle[3].transform.rotation;
        r4 = skittle[4].transform.rotation;
        r5 = skittle[5].transform.rotation;
        r6 = skittle[6].transform.rotation;
        r7 = skittle[7].transform.rotation;
        r8 = skittle[8].transform.rotation;
        r9 = skittle[9].transform.rotation;

        in_rot0 = skittle[0].transform.eulerAngles;
        in_rot1 = skittle[1].transform.eulerAngles;
        in_rot2 = skittle[2].transform.eulerAngles;
        in_rot3 = skittle[3].transform.eulerAngles;
        in_rot4 = skittle[4].transform.eulerAngles;
        in_rot5 = skittle[5].transform.eulerAngles;
        in_rot6 = skittle[6].transform.eulerAngles;
        in_rot7 = skittle[7].transform.eulerAngles;
        in_rot8 = skittle[8].transform.eulerAngles;
        in_rot9 = skittle[9].transform.eulerAngles;
    }
    public void reset()
    {
        skittle[0].transform.position = in_pos0;
        skittle[1].transform.position = in_pos1;
        skittle[2].transform.position = in_pos2;
        skittle[3].transform.position = in_pos3;
        skittle[4].transform.position = in_pos4;
        skittle[5].transform.position = in_pos5;
        skittle[6].transform.position = in_pos6; 
        skittle[7].transform.position = in_pos7;
        skittle[8].transform.position = in_pos8;
        skittle[9].transform.position = in_pos9;

        
        skittle[0].transform.rotation = r0;
        skittle[1].transform.rotation = r1;
        skittle[2].transform.rotation = r2;
        skittle[3].transform.rotation = r3;
        skittle[4].transform.rotation = r4;
        skittle[5].transform.rotation = r5;
        skittle[6].transform.rotation = r6;
        skittle[7].transform.rotation = r7;
        skittle[8].transform.rotation = r8;
        skittle[9].transform.rotation = r9;
        
        skittle[0].transform.eulerAngles = in_rot0;
        skittle[1].transform.eulerAngles = in_rot1;
        skittle[2].transform.eulerAngles = in_rot2;
        skittle[3].transform.eulerAngles = in_rot3;
        skittle[4].transform.eulerAngles = in_rot4;
        skittle[5].transform.eulerAngles = in_rot5;
        skittle[6].transform.eulerAngles = in_rot6;
        skittle[7].transform.eulerAngles = in_rot7;
        skittle[8].transform.eulerAngles = in_rot8;
        skittle[9].transform.eulerAngles = in_rot9;

        print("Reset successful");
    }
    public void count()
    {
        rot0 = skittle[0].transform.eulerAngles;
        rot1 = skittle[1].transform.eulerAngles;
        rot2 = skittle[2].transform.eulerAngles;
        rot3 = skittle[3].transform.eulerAngles;
        rot4 = skittle[4].transform.eulerAngles;
        rot5 = skittle[5].transform.eulerAngles;
        rot6 = skittle[6].transform.eulerAngles;
        rot7 = skittle[7].transform.eulerAngles;
        rot8 = skittle[8].transform.eulerAngles;
        rot9 = skittle[9].transform.eulerAngles;

        if (Mathf.Abs(rot0.x - in_rot0.x) > 70 && fallen[0]==false)
        {
            print(++counter);
            fallen[0] = true;
        }
        if (Mathf.Abs(rot1.x - in_rot1.x) > 50 && fallen[1] == false)
        {
            print(++counter);
            fallen[1] = true;
        }
        if (Mathf.Abs(rot2.x - in_rot2.x) > 50 && fallen[2] == false)
        {
            print(++counter);
            fallen[2] = true;
        }
        if (Mathf.Abs(rot3.x - in_rot3.x) > 50 && fallen[3] == false)
        {
            print(++counter);
            fallen[3] = true;
        }
        if (Mathf.Abs(rot4.x - in_rot4.x) > 50 && fallen[4] == false)
        {
            print(++counter);
            fallen[4] = true;
        }
        if (Mathf.Abs(rot5.x - in_rot5.x) > 50 && fallen[5] == false)
        {
            print(++counter);
            fallen[5] = true;
        }
        if (Mathf.Abs(rot6.x - in_rot6.x) > 50 && fallen[6] == false)
        {
            print(++counter);
            fallen[6] = true;
        }
        if (Mathf.Abs(rot7.x - in_rot7.x) > 50 && fallen[7] == false)
        {
            print(++counter);
            fallen[7] = true;
        }
        if (Mathf.Abs(rot8.x - in_rot8.x) > 50 && fallen[8] == false)
        {
            print(++counter);
            fallen[8] = true;
        }
        if (Mathf.Abs(rot9.x - in_rot9.x) > 50 && fallen[9] == false)
        {
            print(++counter);
            fallen[9] = true;
        }
        PlayerPrefs.SetInt("Counter", counter);

    }
}
