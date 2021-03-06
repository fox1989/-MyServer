// <auto-generated>
//     Generated by the protocol buffer compiler.  DO NOT EDIT!
//     source: Message.proto
// </auto-generated>
#pragma warning disable 1591, 0612, 3021
#region Designer generated code

using pb = global::Google.Protobuf;
using pbc = global::Google.Protobuf.Collections;
using pbr = global::Google.Protobuf.Reflection;
using scg = global::System.Collections.Generic;
/// <summary>Holder for reflection information generated from Message.proto</summary>
public static partial class MessageReflection {

  #region Descriptor
  /// <summary>File descriptor for Message.proto</summary>
  public static pbr::FileDescriptor Descriptor {
    get { return descriptor; }
  }
  private static pbr::FileDescriptor descriptor;

  static MessageReflection() {
    byte[] descriptorData = global::System.Convert.FromBase64String(
        string.Concat(
          "Cg1NZXNzYWdlLnByb3RvInsKB01lc3NhZ2USCgoCaWQYASABKAUSDwoHaGFu",
          "ZGxlchgCIAEoCRIOCgZtZXRob2QYAyABKAkSHgoGcGFyYW1zGAQgAygLMg4u",
          "TWVzc2FnZS5QYXJhbRojCgVQYXJhbRIMCgRuYW1lGAEgASgJEgwKBGRhdGEY",
          "AiABKAxiBnByb3RvMw=="));
    descriptor = pbr::FileDescriptor.FromGeneratedCode(descriptorData,
        new pbr::FileDescriptor[] { },
        new pbr::GeneratedClrTypeInfo(null, null, new pbr::GeneratedClrTypeInfo[] {
          new pbr::GeneratedClrTypeInfo(typeof(global::Message), global::Message.Parser, new[]{ "Id", "Handler", "Method", "Params" }, null, null, null, new pbr::GeneratedClrTypeInfo[] { new pbr::GeneratedClrTypeInfo(typeof(global::Message.Types.Param), global::Message.Types.Param.Parser, new[]{ "Name", "Data" }, null, null, null, null)})
        }));
  }
  #endregion

}
#region Messages
public sealed partial class Message : pb::IMessage<Message> {
  private static readonly pb::MessageParser<Message> _parser = new pb::MessageParser<Message>(() => new Message());
  private pb::UnknownFieldSet _unknownFields;
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public static pb::MessageParser<Message> Parser { get { return _parser; } }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public static pbr::MessageDescriptor Descriptor {
    get { return global::MessageReflection.Descriptor.MessageTypes[0]; }
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  pbr::MessageDescriptor pb::IMessage.Descriptor {
    get { return Descriptor; }
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public Message() {
    OnConstruction();
  }

  partial void OnConstruction();

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public Message(Message other) : this() {
    id_ = other.id_;
    handler_ = other.handler_;
    method_ = other.method_;
    params_ = other.params_.Clone();
    _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public Message Clone() {
    return new Message(this);
  }

  /// <summary>Field number for the "id" field.</summary>
  public const int IdFieldNumber = 1;
  private int id_;
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public int Id {
    get { return id_; }
    set {
      id_ = value;
    }
  }

  /// <summary>Field number for the "handler" field.</summary>
  public const int HandlerFieldNumber = 2;
  private string handler_ = "";
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public string Handler {
    get { return handler_; }
    set {
      handler_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
    }
  }

  /// <summary>Field number for the "method" field.</summary>
  public const int MethodFieldNumber = 3;
  private string method_ = "";
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public string Method {
    get { return method_; }
    set {
      method_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
    }
  }

  /// <summary>Field number for the "params" field.</summary>
  public const int ParamsFieldNumber = 4;
  private static readonly pb::FieldCodec<global::Message.Types.Param> _repeated_params_codec
      = pb::FieldCodec.ForMessage(34, global::Message.Types.Param.Parser);
  private readonly pbc::RepeatedField<global::Message.Types.Param> params_ = new pbc::RepeatedField<global::Message.Types.Param>();
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public pbc::RepeatedField<global::Message.Types.Param> Params {
    get { return params_; }
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public override bool Equals(object other) {
    return Equals(other as Message);
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public bool Equals(Message other) {
    if (ReferenceEquals(other, null)) {
      return false;
    }
    if (ReferenceEquals(other, this)) {
      return true;
    }
    if (Id != other.Id) return false;
    if (Handler != other.Handler) return false;
    if (Method != other.Method) return false;
    if(!params_.Equals(other.params_)) return false;
    return Equals(_unknownFields, other._unknownFields);
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public override int GetHashCode() {
    int hash = 1;
    if (Id != 0) hash ^= Id.GetHashCode();
    if (Handler.Length != 0) hash ^= Handler.GetHashCode();
    if (Method.Length != 0) hash ^= Method.GetHashCode();
    hash ^= params_.GetHashCode();
    if (_unknownFields != null) {
      hash ^= _unknownFields.GetHashCode();
    }
    return hash;
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public override string ToString() {
    return pb::JsonFormatter.ToDiagnosticString(this);
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public void WriteTo(pb::CodedOutputStream output) {
    if (Id != 0) {
      output.WriteRawTag(8);
      output.WriteInt32(Id);
    }
    if (Handler.Length != 0) {
      output.WriteRawTag(18);
      output.WriteString(Handler);
    }
    if (Method.Length != 0) {
      output.WriteRawTag(26);
      output.WriteString(Method);
    }
    params_.WriteTo(output, _repeated_params_codec);
    if (_unknownFields != null) {
      _unknownFields.WriteTo(output);
    }
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public int CalculateSize() {
    int size = 0;
    if (Id != 0) {
      size += 1 + pb::CodedOutputStream.ComputeInt32Size(Id);
    }
    if (Handler.Length != 0) {
      size += 1 + pb::CodedOutputStream.ComputeStringSize(Handler);
    }
    if (Method.Length != 0) {
      size += 1 + pb::CodedOutputStream.ComputeStringSize(Method);
    }
    size += params_.CalculateSize(_repeated_params_codec);
    if (_unknownFields != null) {
      size += _unknownFields.CalculateSize();
    }
    return size;
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public void MergeFrom(Message other) {
    if (other == null) {
      return;
    }
    if (other.Id != 0) {
      Id = other.Id;
    }
    if (other.Handler.Length != 0) {
      Handler = other.Handler;
    }
    if (other.Method.Length != 0) {
      Method = other.Method;
    }
    params_.Add(other.params_);
    _unknownFields = pb::UnknownFieldSet.MergeFrom(_unknownFields, other._unknownFields);
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public void MergeFrom(pb::CodedInputStream input) {
    uint tag;
    while ((tag = input.ReadTag()) != 0) {
      switch(tag) {
        default:
          _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, input);
          break;
        case 8: {
          Id = input.ReadInt32();
          break;
        }
        case 18: {
          Handler = input.ReadString();
          break;
        }
        case 26: {
          Method = input.ReadString();
          break;
        }
        case 34: {
          params_.AddEntriesFrom(input, _repeated_params_codec);
          break;
        }
      }
    }
  }

  #region Nested types
  /// <summary>Container for nested types declared in the Message message type.</summary>
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public static partial class Types {
    public sealed partial class Param : pb::IMessage<Param> {
      private static readonly pb::MessageParser<Param> _parser = new pb::MessageParser<Param>(() => new Param());
      private pb::UnknownFieldSet _unknownFields;
      [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
      public static pb::MessageParser<Param> Parser { get { return _parser; } }

      [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
      public static pbr::MessageDescriptor Descriptor {
        get { return global::Message.Descriptor.NestedTypes[0]; }
      }

      [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
      pbr::MessageDescriptor pb::IMessage.Descriptor {
        get { return Descriptor; }
      }

      [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
      public Param() {
        OnConstruction();
      }

      partial void OnConstruction();

      [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
      public Param(Param other) : this() {
        name_ = other.name_;
        data_ = other.data_;
        _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
      }

      [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
      public Param Clone() {
        return new Param(this);
      }

      /// <summary>Field number for the "name" field.</summary>
      public const int NameFieldNumber = 1;
      private string name_ = "";
      [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
      public string Name {
        get { return name_; }
        set {
          name_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
        }
      }

      /// <summary>Field number for the "data" field.</summary>
      public const int DataFieldNumber = 2;
      private pb::ByteString data_ = pb::ByteString.Empty;
      [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
      public pb::ByteString Data {
        get { return data_; }
        set {
          data_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
        }
      }

      [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
      public override bool Equals(object other) {
        return Equals(other as Param);
      }

      [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
      public bool Equals(Param other) {
        if (ReferenceEquals(other, null)) {
          return false;
        }
        if (ReferenceEquals(other, this)) {
          return true;
        }
        if (Name != other.Name) return false;
        if (Data != other.Data) return false;
        return Equals(_unknownFields, other._unknownFields);
      }

      [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
      public override int GetHashCode() {
        int hash = 1;
        if (Name.Length != 0) hash ^= Name.GetHashCode();
        if (Data.Length != 0) hash ^= Data.GetHashCode();
        if (_unknownFields != null) {
          hash ^= _unknownFields.GetHashCode();
        }
        return hash;
      }

      [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
      public override string ToString() {
        return pb::JsonFormatter.ToDiagnosticString(this);
      }

      [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
      public void WriteTo(pb::CodedOutputStream output) {
        if (Name.Length != 0) {
          output.WriteRawTag(10);
          output.WriteString(Name);
        }
        if (Data.Length != 0) {
          output.WriteRawTag(18);
          output.WriteBytes(Data);
        }
        if (_unknownFields != null) {
          _unknownFields.WriteTo(output);
        }
      }

      [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
      public int CalculateSize() {
        int size = 0;
        if (Name.Length != 0) {
          size += 1 + pb::CodedOutputStream.ComputeStringSize(Name);
        }
        if (Data.Length != 0) {
          size += 1 + pb::CodedOutputStream.ComputeBytesSize(Data);
        }
        if (_unknownFields != null) {
          size += _unknownFields.CalculateSize();
        }
        return size;
      }

      [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
      public void MergeFrom(Param other) {
        if (other == null) {
          return;
        }
        if (other.Name.Length != 0) {
          Name = other.Name;
        }
        if (other.Data.Length != 0) {
          Data = other.Data;
        }
        _unknownFields = pb::UnknownFieldSet.MergeFrom(_unknownFields, other._unknownFields);
      }

      [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
      public void MergeFrom(pb::CodedInputStream input) {
        uint tag;
        while ((tag = input.ReadTag()) != 0) {
          switch(tag) {
            default:
              _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, input);
              break;
            case 10: {
              Name = input.ReadString();
              break;
            }
            case 18: {
              Data = input.ReadBytes();
              break;
            }
          }
        }
      }

    }

  }
  #endregion

}

#endregion


#endregion Designer generated code
