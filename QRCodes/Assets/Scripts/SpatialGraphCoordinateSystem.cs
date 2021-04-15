using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if WINDOWS_UWP
using Windows.Perception.Spatial;
#endif
using Microsoft.MixedReality.Toolkit.Utilities;

namespace QRTracking
{
    public class SpatialGraphCoordinateSystem : MonoBehaviour
    {
#if WINDOWS_UWP
        private SpatialCoordinateSystem CoordinateSystem = null;
#endif
        private System.Guid id;
        public System.Guid Id
        {
            get
            {
                return id;
            }

            set
            {
                id = value;
#if WINDOWS_UWP
                CoordinateSystem = Windows.Perception.Spatial.Preview.SpatialGraphInteropPreview.CreateCoordinateSystemForNode(id);
                if (CoordinateSystem == null)
                {
                    Debug.Log("Id= " + id + " Failed to acquire coordinate system");
                }
#endif
            }
        }

        void Awake()
        {
        }

        // Use this for initialization
        void Start()
        {
#if WINDOWS_UWP
            if (CoordinateSystem == null)
            {
                CoordinateSystem = Windows.Perception.Spatial.Preview.SpatialGraphInteropPreview.CreateCoordinateSystemForNode(id);
                if (CoordinateSystem == null)
                {
                    Debug.Log("Id= " + id + " Failed to acquire coordinate system");
                }
            }
#endif
        }

        private void UpdateLocation()
        {
            {
#if WINDOWS_UWP
                if (CoordinateSystem == null)
                {
                    CoordinateSystem = Windows.Perception.Spatial.Preview.SpatialGraphInteropPreview.CreateCoordinateSystemForNode(id);

                    if (CoordinateSystem == null)
                    {
                        Debug.Log("Id= " + id + " Failed to acquire coordinate system");
                    }
                }

                if (CoordinateSystem != null)
                {
                    Quaternion rotation = Quaternion.identity;
                    Vector3 translation = new Vector3(0.0f, 0.0f, 0.0f);
                    
                    System.IntPtr rootCoordnateSystemPtr = UnityEngine.XR.WindowsMR.WindowsMREnvironment.OriginSpatialCoordinateSystem;
                    SpatialCoordinateSystem rootSpatialCoordinateSystem = (SpatialCoordinateSystem)System.Runtime.InteropServices.Marshal.GetObjectForIUnknown(rootCoordnateSystemPtr);

                    // Get the relative transform from the unity origin
                    System.Numerics.Matrix4x4? relativePose = CoordinateSystem.TryGetTransformTo(rootSpatialCoordinateSystem);

                    if (relativePose != null)
                    {
                        System.Numerics.Vector3 scale;
                        System.Numerics.Quaternion rotation1;
                        System.Numerics.Vector3 translation1;
       
                        System.Numerics.Matrix4x4 newMatrix = relativePose.Value;

                        // Platform coordinates are all right handed and unity uses left handed matrices. so we convert the matrix
                        // from rhs-rhs to lhs-lhs 
                        // Convert from right to left coordinate system
                        newMatrix.M13 = -newMatrix.M13;
                        newMatrix.M23 = -newMatrix.M23;
                        newMatrix.M43 = -newMatrix.M43;

                        newMatrix.M31 = -newMatrix.M31;
                        newMatrix.M32 = -newMatrix.M32;
                        newMatrix.M34 = -newMatrix.M34;

                        System.Numerics.Matrix4x4.Decompose(newMatrix, out scale, out rotation1, out translation1);
                        translation = new Vector3(translation1.X, translation1.Y, translation1.Z);
                        rotation = new Quaternion(rotation1.X, rotation1.Y, rotation1.Z, rotation1.W);
                        Pose pose = new Pose(translation, rotation);

                        // If there is a parent to the camera that means we are using teleport and we should not apply the teleport
                        // to these objects so apply the inverse
                        if (CameraCache.Main.transform.parent != null)
                        {
                            pose = pose.GetTransformedBy(CameraCache.Main.transform.parent);
                        }

                        gameObject.transform.SetPositionAndRotation(pose.position, pose.rotation);
                        //Debug.Log("Id= " + id + " QRPose = " +  pose.position.ToString("F7") + " QRRot = "  +  pose.rotation.ToString("F7"));
                    }
                    else
                    {
                       // Debug.Log("Id= " + id + " Unable to locate qrcode" );
                    }
                }
                else
                {
                   gameObject.SetActive(false);
                }
#endif
            }
        }
        // Update is called once per frame
        void Update()
        {
            UpdateLocation();
        }
    }
}