using System;
using System.Collections.Generic;
using System.Text;

namespace Server
{
    /// <summary>
    /// 统一消息
    /// </summary>
    public class MyMessage
    {
        public int id;

        public string handler;

        public string method;

        public byte[] bytes;



        private List<byte> tempBytes = new List<byte>();

        private int cur = 0;

        public byte[] ToBytes()
        {

            WriteInt(id);
            WriteString(handler);
            WriteString(method);
            WriteBytes(bytes);
            return tempBytes.ToArray();
        }

        public void Read(byte[] bytes)
        {

            tempBytes = new List<byte>();
            tempBytes.AddRange(bytes);
            id = ReadInt();
            handler = ReadString();
            method = ReadString();
            this.bytes = ReadBytes();
        }


        public new string ToString()
        {
            return "{id:" + id + "  handler:" + handler + "  method:" + method + " length:" + bytes.Length + "}";
        }


        int ReadInt()
        {
            byte[] bs = ReadSize(4);
            int i = BitConverter.ToInt32(bs);
            return i;
        }

        string ReadString()
        {
            int length = ReadInt();
            byte[] bs = ReadSize(length);
            string str = Encoding.Default.GetString(bs);
            return str;
        }

        byte[] ReadBytes()
        {
            int length = ReadInt();
            byte[] bs = ReadSize(length);
            return bs;
        }


        byte[] ReadSize(int size)
        {
            byte[] rb = new byte[size];

            tempBytes.CopyTo(cur, rb, 0, size);
            cur = cur + size;
            return rb;
        }


        void WriteInt(int i)
        {
            byte[] bs = System.BitConverter.GetBytes(id);
            tempBytes.AddRange(bs);
        }

        void WriteString(string str)
        {
            byte[] bs = Encoding.Default.GetBytes(str);
            tempBytes.AddRange(BitConverter.GetBytes(bs.Length));
            tempBytes.AddRange(bs);
        }


        void WriteBytes(byte[] bytes)
        {
            tempBytes.AddRange(BitConverter.GetBytes(bytes.Length));
            tempBytes.AddRange(bytes);
        }
    }
}
