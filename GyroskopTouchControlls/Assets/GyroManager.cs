using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GyroManager : MonoBehaviour
{
   #region Instance

   private static GyroManager instance;

   public static GyroManager Instance //important to have Instance written in upper case
   {
      get //basically a singleton, makes sure that there is only one instance of the gyroscope
      {
         if (instance == null)
         {
            instance = FindObjectOfType<GyroManager>();
            if (instance == null)
            {
               instance = new GameObject("Spawned Gyromanager", typeof(GyroManager)).GetComponent<GyroManager>();
            }
         }

         return instance;
      }
      set { instance = value; }
   }

   #endregion

   [Header("Logic")] 
   private Gyroscope gyro;
   private Quaternion rotation;
   private bool gyroActive;

   public void EnableGyro()
   {
      if (gyroActive)
      {
         return;
      }

      if (SystemInfo.supportsGyroscope)
      {
         gyro = Input.gyro;
         gyro.enabled = true;
         gyroActive = gyro.enabled;
      }
      else
      {
         Debug.Log("Gyro is not supported");
      }

      
   }

   private void Update()
   {
      if (gyroActive)
      {
         rotation = gyro.attitude;
      }
   }

   public Quaternion GetGyroRotation()
   {
      return rotation;
   }
}





