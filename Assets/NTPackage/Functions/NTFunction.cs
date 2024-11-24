using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NTPackage.Functions{
    public class NTFunction : NTBehaviour
    {
        public virtual void CheckNull(object variable){
            if (variable == null){
                Debug.LogWarning("Cant load: "+variable.ToString());
            }
        }

        public static void ClearChild(Transform trans){
            int count = trans.childCount;
            for (int i = count-1; i >= 0; i--)
            {
                Transform child = trans.GetChild(i);
                child.gameObject.SetActive(false);
                DestroyImmediate(child.gameObject);
            }
        }

        public static void SetActiveFalse(Transform trans){
            foreach (Transform transChild in trans)
            {
                transChild.gameObject.SetActive(false);
            }
        }

        public static void ResetPosition(Transform trans){
            trans.localPosition = new Vector3(0,0,0);
            trans.localRotation = Quaternion.identity;
            trans.localScale = new Vector3(1,1,1);
        }

        public static string FormatNumber(double num)
        {
            string[] suffix = {"", "K", "M", "B", "T", "Q"};
            int index = 0;
            while (num >= 1000.0f && index < suffix.Length - 1)
            {
                num /= 1000.0f;
                index++;
            }
            return $"{num:G3}{suffix[index]}";
        }

        public static int GetCurrentDateNumber(){
            DateTime date = DateTime.Now;
            int number = date.Year* 10000;
            number += date.Month * 100;
            number += date.Day;
            return number;
        }

        public static string GetCurrentFomatDate(){
            DateTime date = DateTime.Now;
            int dd = date.Day;
            int mm = date.Month;
            int yy = date.Year;
            return dd+"/"+mm+"/"+yy;
        }
        
        //Math
        public static float NextFibonacci(float lv, float start, float raise){
            float result = start + raise*lv*(lv+1)/2;
            return result;
        }

        public static Color StringHexToColor(string color){
            float red =  System.Convert.ToInt32(color.Substring(0,2),16)/255f;
            float green =  System.Convert.ToInt32(color.Substring(2,2),16)/255f;
            float blue =  System.Convert.ToInt32(color.Substring(4,2),16)/255f;
            return new Color(red, green, blue);
        }

        public static string FormatTimeHour(long time){
            int second = (int)time;
            int minus = second/60;
            int hour = minus/60;
            minus -= hour*60;
            second -= (minus*60+hour*3600);
            string sec;
            if(second < 10){
                sec = "0"+second.ToString();
            }else{
                sec = second.ToString();
            }
            string min;
            if(minus < 10){
                min = "0"+minus.ToString();
            }else{
                min = minus.ToString();
            }
            return hour+":"+min+":"+sec;
        }
        public static string FormatTimeMinus(float time){
            int second = (int)time;
            int minus = second/60;
            second = second - minus*60;
            string sec;
            if(second < 10){
                sec = "0"+second.ToString();
            }else{
                sec = second.ToString();
            }
            string min;
            if(minus < 10){
                min = "0"+minus.ToString();
            }else{
                min = minus.ToString();
            }
            return min+":"+sec;
        }

        //1702539405
        public static long GetUtcTimestamp()
        {
            DateTimeOffset nowUtcOffset = DateTimeOffset.UtcNow;
            long timestamp = nowUtcOffset.ToUnixTimeSeconds();
            return timestamp;
        }

        public static string GenerateId()
        {
            var timestamp = DateTime.UtcNow.Ticks / 10000L; // get the current timestamp in milliseconds
            var guidBytes = Guid.NewGuid().ToByteArray(); // get a new guid as a byte array
            var objectIdBytes = new byte[12]; // create a new byte array to hold the final ObjectId

            // Copy the timestamp bytes into the objectIdBytes array in big-endian order
            Array.Copy(BitConverter.GetBytes(timestamp), 0, objectIdBytes, 0, 4);
            // Copy the guid bytes into the objectIdBytes array in little-endian order
            Array.Copy(guidBytes, 0, objectIdBytes, 4, 8);

            // Convert the objectIdBytes array to a string in hexadecimal format
            var objectIdString = BitConverter.ToString(objectIdBytes).Replace("-", "").ToLower();

            return objectIdString;
        }

        public static IEnumerator WaiteSecond(float time, Action callback){
            yield return new WaitForSeconds(time);
            callback.Invoke();
        }

        //Enum
        public static T ParseEnumFromString<T>(string name){
            try
            {
                return (T)Enum.Parse(typeof(T), name);
            }
            catch (System.Exception)
            {
                return default;
            }
        }
    }
}
