using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SpentTimeTest
{
    class Program
    {
        static void Main(string[] args)
        {
            ClassSpentTime m_T1 = new ClassSpentTime("ss.txt");
            for (int i = 0; i < 100; i++)
            {
                m_T1.RecordStartTime("Step_1");
                m_T1.RecordEndTime("Step_1");
                m_T1.RecordStartTime("Step_2");
                m_T1.RecordStartTime("Step_3");
                m_T1.RecordEndTime("Step_3");
                m_T1.RecordEndTime("Step_2");
            }
            m_T1.RecordStartTime("Step_4");
            m_T1.RecordStartTime("Step_5");
            m_T1.RecordStartTime("Step_6");
            m_T1.RecordEndTime("Step_6");
            m_T1.RecordEndTime("Step_7");
            m_T1.RecordEndTime("Step_8");
        }
    }
}
