using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageProcessToolBox
{
    class Heap
    {
        private List<int> myList = new List<int>();
        private int myLen;
        public Heap(byte[] myList, int myLen)
        {
            this.myLen = myLen;
            foreach (int item in myList)
                this.myList.Add(item);
        }

        public Heap(List<int> myList, int myLen)
        {
            this.myLen = myLen;
            this.myList = myList;
        }
        public void heapsort()
        {
            int iValue;

            for (int i = myLen / 2; i >= 0; i--)
            {
                adjust(i, myLen - 1);
            }

            for (int i = myLen - 2; i >= 0; i--)
            {
                iValue = myList[i + 1];
                myList[i + 1] = myList[0];
                myList[0] = iValue;
                adjust(0, i);
            }
        }

        private void adjust(int i, int n)
        {
            int iPosition;
            int iChange;

            iPosition = myList[i];
            iChange = 2 * i;
            while (iChange <= n)
            {
                if (iChange < n && myList[iChange] < myList[iChange + 1])
                {
                    iChange++;
                }
                if (iPosition >= myList[iChange])
                {
                    break;
                }
                myList[iChange / 2] = myList[iChange];
                iChange *= 2;
            }
            myList[iChange / 2] = iPosition;
        }
        public List<int> get()
        {
            return myList;
        }
    }
}
