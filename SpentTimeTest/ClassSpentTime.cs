using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
namespace SpentTimeTest
{
    //ClassSpentTimeStruct is the saving struct of ClassSpentTime.
    public class ClassSpentTimeStruct
    {
        public string m_RecordName { get; set; }
        public string m_RecordTime { get; set; }
        public ClassSpentTimeStruct(string f_RecordName, string f_RecordTime)
        {
            m_RecordName = f_RecordName;
            m_RecordTime = f_RecordTime;
        }
    }
    //ClassSpentTime is use in the single thread :
    //ClassSpentTime t_Temp = new ClassSpentTime(File Path and File Name)
    //t_Temp.StartRecord(string File Name)
    //t_Temp.EndRecord(string File Name)
    //folowing up we must get the pair
    class ClassSpentTime
    {
        Stack<ClassSpentTimeStruct> m_StackSpentTime = new Stack<ClassSpentTimeStruct>();
        //寫入檔案串流
        //The file writing stream
        string m_RecordFileName;

        private void WriteToFile(string f_Content)
        {
            using (System.IO.StreamWriter t_StreamWriter = new System.IO.StreamWriter(m_RecordFileName, true, Encoding.Default))
            {
                t_StreamWriter.WriteLine(f_Content);
            }
        }
        private string  GetCurrentTimeString()
        {
            return DateTime.Now.ToString("yyyy:MM:dd" + "\t" + "HH:mm:ss.fff");
        }
        public ClassSpentTime(string f_RecordFileName)
        {
            m_RecordFileName = f_RecordFileName;
            m_StackSpentTime.Clear();
            try
            {
                WriteToFile("===" + GetCurrentTimeString() + "===");
            }
            catch(System.IO.IOException e)
            {
                System.Diagnostics.Debug.WriteLine(e.Message);
            }
        }
        
        //Set the Record File at which file
        public void SetFileName(string f_RecordFileName)
        {
            m_RecordFileName = f_RecordFileName;
            try
            {
                WriteToFile("===" + GetCurrentTimeString() + "===");
            }
            catch(System.IO.IOException e)
            {
                System.Diagnostics.Debug.WriteLine(e.Message);
            }
        }
        public void RecordStartTime(string f_RecordName)
        {   try
            {
                WriteToFile("Begin" + "\t" + f_RecordName + "\t" + GetCurrentTimeString());
                m_StackSpentTime.Push(new ClassSpentTimeStruct(f_RecordName, GetCurrentTimeString()));
            }
            catch(Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.Message);
            }
        }
        public void RecordEndTime(string f_RecordName)
        {
            try
            {
                if (string.Compare(m_StackSpentTime.Pop().m_RecordName, f_RecordName) != 0)
                {
                    WriteToFile("Error" + "\t" + f_RecordName + "\t" + GetCurrentTimeString());
                }
                else
                {
                    WriteToFile("End" + "\t" + f_RecordName + "\t" + GetCurrentTimeString());
                }
            }
            catch (System.IO.IOException e)
            {
                System.Diagnostics.Debug.WriteLine(e.Message);
            }
            catch(Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.Message);
            }
        }
        ~ClassSpentTime()
        {
            try
            {
                //m_StreamWriter = new System.IO.StreamWriter(m_RecordFileName, true);

                if (m_StackSpentTime.Count != 0)
                {
                    ClassSpentTimeStruct t_ClassSpentTimeStruct;
                    while ((t_ClassSpentTimeStruct = m_StackSpentTime.Pop()) != null)
                    {
                        WriteToFile(t_ClassSpentTimeStruct.m_RecordName + "\t" + t_ClassSpentTimeStruct.m_RecordTime);
                    }
                    m_StackSpentTime.Clear();
                    m_StackSpentTime = null;
                }
            }
            catch (Exception e)
            {
                 System.Diagnostics.Debug.WriteLine(e.Message);
            }
        }
    }
}
