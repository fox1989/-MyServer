using Google.Protobuf;
using Google.Protobuf.Collections;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using pb = global::Google.Protobuf;


public sealed partial class Message : pb::IMessage<Message>
{

    /// <summary>
    /// 
    /// </summary>
    /// <param name="o">int long float double bool string Imessage </param>
    public void AddParam(object o)
    {
        byte[] bytes = null;

        if (o is int)
        {
            int length = CodedOutputStream.ComputeInt32Size((int)o);
            bytes = new byte[length];
            CodedOutputStream cos = new CodedOutputStream(bytes);
            cos.WriteInt32((int)o);
        }
        else if (o is long)
        {
            int length = CodedOutputStream.ComputeInt64Size((long)o);
            bytes = new byte[length];
            CodedOutputStream cos = new CodedOutputStream(bytes);
            cos.WriteInt64((long)o);
        }
        else if (o is float)
        {
            int length = CodedOutputStream.ComputeFloatSize((float)o);
            bytes = new byte[length];
            CodedOutputStream cos = new CodedOutputStream(bytes);
            cos.WriteFloat((float)o);
        }
        else if (o is double)
        {
            int length = CodedOutputStream.ComputeDoubleSize((double)o);
            bytes = new byte[length];
            CodedOutputStream cos = new CodedOutputStream(bytes);
            cos.WriteDouble((double)o);
        }
        else if (o is bool)
        {
            int length = CodedOutputStream.ComputeBoolSize((bool)o);
            bytes = new byte[length];
            CodedOutputStream cos = new CodedOutputStream(bytes);
            cos.WriteBool((bool)o);
        }
        else if (o is string)
        {
            int length = CodedOutputStream.ComputeStringSize((string)o);
            bytes = new byte[length];
            CodedOutputStream cos = new CodedOutputStream(bytes);
            cos.WriteString((string)o);
        }
        else if (o is IMessage)
        {
            int length = CodedOutputStream.ComputeMessageSize((IMessage)o);
            bytes = new byte[length];
            CodedOutputStream cos = new CodedOutputStream(bytes);
            cos.WriteMessage((IMessage)o);
        }

        if (bytes != null)
            AddParam(o.GetType().FullName, bytes);
    }



    public void AddParam(string name, byte[] bytes)
    {
        Types.Param param = new Types.Param();
        param.Data = ByteString.CopyFrom(bytes);
        param.Name = name;
        Params.Add(param);

    }



    public Dictionary<Type, object> ReadParam()
    {

        Dictionary<Type, object> pairs = new Dictionary<Type, object>();

        foreach (var item in Params)
        {
            object o = null;
            Type t = Type.GetType(item.Name);

            if (t != null)
            {
                CodedInputStream cis = new CodedInputStream(item.Data.ToByteArray());
                if (t == typeof(int))
                {
                    o = cis.ReadInt32();
                }
                else if (t == typeof(long))
                {
                    o = cis.ReadInt64();

                }
                else if (t == typeof(float))
                {
                    o = cis.ReadFloat();

                }
                else if (t == typeof(double))
                {
                    o = cis.ReadDouble();

                }
                else if (t == typeof(bool))
                {
                    o = cis.ReadBool();

                }
                else if (t == typeof(string))
                {
                    o = cis.ReadString();

                }
                else if (typeof(IMessage).IsAssignableFrom(t))
                {
                    o = Activator.CreateInstance(t);
                    cis.ReadMessage((IMessage)o);
                }

                pairs.Add(t, o);
            }

        }

        return pairs;
    }


    public static Type GetTypeByName(string name)
    {
        return Type.GetType(name);
    }

}

