using System.Collections.Generic;
using System.IO;
using UnityEngine;
using uOSC;
using VRM;

public class VRMController : MonoBehaviour
{
    VRMImporterContext Context = new VRMImporterContext();
    uOscClient OscClient;
    public List<GameObject> VirtualTrackers = new List<GameObject>();

    private void Start()
    {
        OscClient = GetComponent<uOscClient>();

        var path = @"AliciaSolid.vrm";
        var bytes = File.ReadAllBytes(path);

        Context.ParseGlb(bytes);
        Context.Load();
        Context.ShowMeshes();

        var animator = Context.Root.GetComponent<Animator>();

        var HeadTracker = new GameObject("HeadTracker");
        HeadTracker.transform.SetParent(animator.GetBoneTransform(HumanBodyBones.Head));
        HeadTracker.transform.localPosition = Vector3.zero;
        VirtualTrackers.Add(HeadTracker);

        var LeftHandTracker = new GameObject("LeftHandTracker");
        LeftHandTracker.transform.SetParent(animator.GetBoneTransform(HumanBodyBones.LeftHand));
        LeftHandTracker.transform.localPosition = Vector3.zero;
        VirtualTrackers.Add(LeftHandTracker);

        var RightHandTracker = new GameObject("RightHandTracker");
        RightHandTracker.transform.SetParent(animator.GetBoneTransform(HumanBodyBones.RightHand));
        RightHandTracker.transform.localPosition = Vector3.zero;
        VirtualTrackers.Add(RightHandTracker);

        var LeftLowerArmTracker = new GameObject("LeftLowerArmTracker");
        LeftLowerArmTracker.transform.SetParent(animator.GetBoneTransform(HumanBodyBones.LeftLowerArm));
        LeftLowerArmTracker.transform.localPosition = Vector3.zero;
        VirtualTrackers.Add(LeftLowerArmTracker);

        var RightLowerArmTracker = new GameObject("RightLowerArmTracker");
        RightLowerArmTracker.transform.SetParent(animator.GetBoneTransform(HumanBodyBones.RightLowerArm));
        RightLowerArmTracker.transform.localPosition = Vector3.zero;
        VirtualTrackers.Add(RightLowerArmTracker);

        var SpineTracker = new GameObject("SpineTracker");
        SpineTracker.transform.SetParent(animator.GetBoneTransform(HumanBodyBones.Spine));
        SpineTracker.transform.localPosition = Vector3.zero;
        VirtualTrackers.Add(SpineTracker);

        var LeftLowerLegTracker = new GameObject("LeftLowerLegTracker");
        LeftLowerLegTracker.transform.SetParent(animator.GetBoneTransform(HumanBodyBones.LeftLowerLeg));
        LeftLowerLegTracker.transform.localPosition = Vector3.zero;
        VirtualTrackers.Add(LeftLowerLegTracker);

        var RightLowerLegTracker = new GameObject("RightLowerLegTracker");
        RightLowerLegTracker.transform.SetParent(animator.GetBoneTransform(HumanBodyBones.RightLowerLeg));
        RightLowerLegTracker.transform.localPosition = Vector3.zero;
        VirtualTrackers.Add(RightLowerLegTracker);

        var LeftFootTracker = new GameObject("LeftFootTracker");
        LeftFootTracker.transform.SetParent(animator.GetBoneTransform(HumanBodyBones.LeftFoot));
        LeftFootTracker.transform.localPosition = Vector3.zero;
        VirtualTrackers.Add(LeftFootTracker);

        var RightFootTracker = new GameObject("RightFootTracker");
        RightFootTracker.transform.SetParent(animator.GetBoneTransform(HumanBodyBones.RightFoot));
        RightFootTracker.transform.localPosition = Vector3.zero;
        VirtualTrackers.Add(RightFootTracker);
    }

    private void OnDestroy()
    {
        Context.Dispose();
    }

    private void Update()
    {
        var bundle = new Bundle();

        foreach (var tracker in VirtualTrackers)
        {
            bundle.Add(new Message("/VMC/Ext/Tra/Pos", tracker.name,
                tracker.transform.position.x,
                tracker.transform.position.y,
                tracker.transform.position.z,
                tracker.transform.rotation.x,
                tracker.transform.rotation.y,
                tracker.transform.rotation.z,
                tracker.transform.rotation.w));
        }

        OscClient.Send(bundle);
    }
}
