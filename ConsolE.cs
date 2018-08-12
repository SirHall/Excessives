using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Excessives
{

    static class ConsolE
    {
        //Queue of stuff to print
        static List<string> queue = new List<string>();
        #region Public Interface
        public static void Write(string message)
        {
            isQueueLocked = true;
            queue.Add(message);
            isQueueLocked = false;

            Run();

        }

        public static void WriteLine(string message)
        {
            Write(message + '\n');
        }
        #endregion

        static bool isQueueLocked = false;

        static void Run()
        {
            if (printThread != null)
                return;

            printThread = new Thread(PrintAll);
            printThread.Start();
        }

        static Thread printThread;

        static void PrintAll()
        {
            while (queue.Count > 0)
            {
                for (int i = 0; i < queue.Count; i++)
                {
                    Console.Write(queue[i]);
                }
                queue.Clear();
            }


        }
    }
}

