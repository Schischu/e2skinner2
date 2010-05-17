using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace e2skinner2.Logic
{
    public class cCommandQueue
    {
        private LinkedList<cCommand> pQueue;
        private LinkedListNode<cCommand> pLastAction;

        // A delegate type for hooking up change notifications.
        public delegate void EventHandler(cCommand sender, EventArgs e);

        public class cCommand
        {
            // An event that clients can use to be notified
            public event EventHandler DoEvent;
            public event EventHandler UndoEvent;

            public Object Helper;

            public Object From;
            public Object To;

            public virtual String toString()
            {
                // Enter here your command
                Console.WriteLine("CQ: toString");
                return "unkown";
            }

            public bool doCmd()
            {
                Console.WriteLine("CQ: doCmd");

                DoEvent(this, EventArgs.Empty);

                return false;
            }

            public bool undoCmd()
            {
                Console.WriteLine("CQ: undoCmd");

                UndoEvent(this, EventArgs.Empty);

                return false;
            }
        }

        public cCommandQueue()
        {
            pQueue = new LinkedList<cCommand>();
            pLastAction = pQueue.First;
        }

        public void addCmd(cCommand cmd)
        {
            while (pQueue.Last != pLastAction)
                pQueue.RemoveLast(); ;

            pQueue.AddLast(cmd);

            pLastAction = pQueue.Last;
            pLastAction.Value.doCmd();
        }

        public void undoCmd()
        {
            if (pLastAction != null)
            {
                pLastAction.Value.undoCmd();
                pLastAction = pLastAction.Previous;
            }
        }

        public void redoCmd()
        {
            if (pLastAction == null)
            {
                pLastAction = pQueue.First;
                pLastAction.Value.doCmd();
            }
            else if (pLastAction != null && pLastAction.Next != null)
            {
                pLastAction = pLastAction.Next;
                pLastAction.Value.doCmd();
            }
        }

        public bool isUndoPossible()
        {
            if (pLastAction != null)
                return true;
            return false;
        }

        public bool isRedoPossible()
        {
            if (pLastAction != null && pLastAction.Next == null)
                return false;
            return true;
        }

        public void clear()
        {
            pQueue.Clear();
            pLastAction = pQueue.First;
        }
    }
}
