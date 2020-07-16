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
          "Cg1NZXNzYWdlLnByb3RvIjQKB01lc3NhZ2USCgoCaWQYASABKAUSDgoGbWV0",
          "aG9kGAIgASgJEg0KBXBhcmFtGAMgAygMYgZwcm90bzM="));
    descriptor = pbr::FileDescriptor.FromGeneratedCode(descriptorData,
        new pbr::FileDescriptor[] { },
        new pbr::GeneratedClrTypeInfo(null, null, new pbr::GeneratedClrTypeInfo[] {
          new pbr::GeneratedClrTypeInfo(typeof(global::Message), global::Message.Parser, new[]{ "Id", "Method", "Param" }, null, null, null, null)
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
    method_ = other.method_;
    param_ = other.param_.Clone();
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

  /// <summary>Field number for the "method" field.</summary>
  public const int MethodFieldNumber = 2;
  private string method_ = "";
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public string Method {
    get { return method_; }
    set {
      method_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
    }
  }

  /// <summary>Field number for the "param" field.</summary>
  public const int ParamFieldNumber = 3;
  private static readonly pb::FieldCodec<pb::ByteString> _repeated_param_codec
      = pb::FieldCodec.ForBytes(26);
  private readonly pbc::RepeatedField<pb::ByteString> param_ = new pbc::RepeatedField<pb::ByteString>();
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public pbc::RepeatedField<pb::ByteString> Param {
    get { return param_; }
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
    if (Method != other.Method) return false;
    if(!param_.Equals(other.param_)) return false;
    return Equals(_unknownFields, other._unknownFields);
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public override int GetHashCode() {
    int hash = 1;
    if (Id != 0) hash ^= Id.GetHashCode();
    if (Method.Length != 0) hash ^= Method.GetHashCode();
    hash ^= param_.GetHashCode();
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
    if (Method.Length != 0) {
      output.WriteRawTag(18);
      output.WriteString(Method);
    }
    param_.WriteTo(output, _repeated_param_codec);
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
    if (Method.Length != 0) {
      size += 1 + pb::CodedOutputStream.ComputeStringSize(Method);
    }
    size += param_.CalculateSize(_repeated_param_codec);
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
    if (other.Method.Length != 0) {
      Method = other.Method;
    }
    param_.Add(other.param_);
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
          Method = input.ReadString();
          break;
        }
        case 26: {
          param_.AddEntriesFrom(input, _repeated_param_codec);
          break;
        }
      }
    }
  }

}

#endregion


#endregion Designer generated code
