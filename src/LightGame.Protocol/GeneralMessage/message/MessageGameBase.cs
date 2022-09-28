// <auto-generated>
//     Generated by the protocol buffer compiler.  DO NOT EDIT!
//     source: message_game_base.proto
// </auto-generated>
#pragma warning disable 1591, 0612, 3021, 8981
#region Designer generated code

using pb = global::Google.Protobuf;
using pbc = global::Google.Protobuf.Collections;
using pbr = global::Google.Protobuf.Reflection;
using scg = global::System.Collections.Generic;
namespace LightGame.Protocol {

  /// <summary>Holder for reflection information generated from message_game_base.proto</summary>
  public static partial class MessageGameBaseReflection {

    #region Descriptor
    /// <summary>File descriptor for message_game_base.proto</summary>
    public static pbr::FileDescriptor Descriptor {
      get { return descriptor; }
    }
    private static pbr::FileDescriptor descriptor;

    static MessageGameBaseReflection() {
      byte[] descriptorData = global::System.Convert.FromBase64String(
          string.Concat(
            "ChdtZXNzYWdlX2dhbWVfYmFzZS5wcm90bxISTGlnaHRHYW1lLlByb3RvY29s",
            "IpgBCgVMR01zZxIQCghBY3Rpb25JZBgBIAEoBRIOCgZVc2VySWQYAiABKAMS",
            "DQoFTXNnSWQYAyABKAUSDQoFVG9rZW4YBCABKAkSCgoCU3QYBSABKAMSDwoH",
            "Q29udGVudBgGIAEoDBIMCgRTaWduGAcgASgJEhEKCUVycm9yQ29kZRgKIAEo",
            "BRIRCglFcnJvckluZm8YCyABKAkiQwoKTEdSZXNwb25zZRIRCglFcnJvckNv",
            "ZGUYASABKAUSEQoJRXJyb3JJbmZvGAIgASgJEg8KB0NvbnRlbnQYAyABKAxi",
            "BnByb3RvMw=="));
      descriptor = pbr::FileDescriptor.FromGeneratedCode(descriptorData,
          new pbr::FileDescriptor[] { },
          new pbr::GeneratedClrTypeInfo(null, null, new pbr::GeneratedClrTypeInfo[] {
            new pbr::GeneratedClrTypeInfo(typeof(global::LightGame.Protocol.LGMsg), global::LightGame.Protocol.LGMsg.Parser, new[]{ "ActionId", "UserId", "MsgId", "Token", "St", "Content", "Sign", "ErrorCode", "ErrorInfo" }, null, null, null, null),
            new pbr::GeneratedClrTypeInfo(typeof(global::LightGame.Protocol.LGResponse), global::LightGame.Protocol.LGResponse.Parser, new[]{ "ErrorCode", "ErrorInfo", "Content" }, null, null, null, null)
          }));
    }
    #endregion

  }
  #region Messages
  /// <summary>
  ///tcp
  /// </summary>
  public sealed partial class LGMsg : pb::IMessage<LGMsg>
  #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
      , pb::IBufferMessage
  #endif
  {
    private static readonly pb::MessageParser<LGMsg> _parser = new pb::MessageParser<LGMsg>(() => new LGMsg());
    private pb::UnknownFieldSet _unknownFields;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public static pb::MessageParser<LGMsg> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::LightGame.Protocol.MessageGameBaseReflection.Descriptor.MessageTypes[0]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public LGMsg() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public LGMsg(LGMsg other) : this() {
      actionId_ = other.actionId_;
      userId_ = other.userId_;
      msgId_ = other.msgId_;
      token_ = other.token_;
      st_ = other.st_;
      content_ = other.content_;
      sign_ = other.sign_;
      errorCode_ = other.errorCode_;
      errorInfo_ = other.errorInfo_;
      _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public LGMsg Clone() {
      return new LGMsg(this);
    }

    /// <summary>Field number for the "ActionId" field.</summary>
    public const int ActionIdFieldNumber = 1;
    private int actionId_;
    /// <summary>
    ///Request
    /// </summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public int ActionId {
      get { return actionId_; }
      set {
        actionId_ = value;
      }
    }

    /// <summary>Field number for the "UserId" field.</summary>
    public const int UserIdFieldNumber = 2;
    private long userId_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public long UserId {
      get { return userId_; }
      set {
        userId_ = value;
      }
    }

    /// <summary>Field number for the "MsgId" field.</summary>
    public const int MsgIdFieldNumber = 3;
    private int msgId_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public int MsgId {
      get { return msgId_; }
      set {
        msgId_ = value;
      }
    }

    /// <summary>Field number for the "Token" field.</summary>
    public const int TokenFieldNumber = 4;
    private string token_ = "";
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public string Token {
      get { return token_; }
      set {
        token_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    /// <summary>Field number for the "St" field.</summary>
    public const int StFieldNumber = 5;
    private long st_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public long St {
      get { return st_; }
      set {
        st_ = value;
      }
    }

    /// <summary>Field number for the "Content" field.</summary>
    public const int ContentFieldNumber = 6;
    private pb::ByteString content_ = pb::ByteString.Empty;
    /// <summary>
    ///合并数据包
    /// </summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public pb::ByteString Content {
      get { return content_; }
      set {
        content_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    /// <summary>Field number for the "Sign" field.</summary>
    public const int SignFieldNumber = 7;
    private string sign_ = "";
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public string Sign {
      get { return sign_; }
      set {
        sign_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    /// <summary>Field number for the "ErrorCode" field.</summary>
    public const int ErrorCodeFieldNumber = 10;
    private int errorCode_;
    /// <summary>
    ///Response
    /// </summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public int ErrorCode {
      get { return errorCode_; }
      set {
        errorCode_ = value;
      }
    }

    /// <summary>Field number for the "ErrorInfo" field.</summary>
    public const int ErrorInfoFieldNumber = 11;
    private string errorInfo_ = "";
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public string ErrorInfo {
      get { return errorInfo_; }
      set {
        errorInfo_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public override bool Equals(object other) {
      return Equals(other as LGMsg);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public bool Equals(LGMsg other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (ActionId != other.ActionId) return false;
      if (UserId != other.UserId) return false;
      if (MsgId != other.MsgId) return false;
      if (Token != other.Token) return false;
      if (St != other.St) return false;
      if (Content != other.Content) return false;
      if (Sign != other.Sign) return false;
      if (ErrorCode != other.ErrorCode) return false;
      if (ErrorInfo != other.ErrorInfo) return false;
      return Equals(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public override int GetHashCode() {
      int hash = 1;
      if (ActionId != 0) hash ^= ActionId.GetHashCode();
      if (UserId != 0L) hash ^= UserId.GetHashCode();
      if (MsgId != 0) hash ^= MsgId.GetHashCode();
      if (Token.Length != 0) hash ^= Token.GetHashCode();
      if (St != 0L) hash ^= St.GetHashCode();
      if (Content.Length != 0) hash ^= Content.GetHashCode();
      if (Sign.Length != 0) hash ^= Sign.GetHashCode();
      if (ErrorCode != 0) hash ^= ErrorCode.GetHashCode();
      if (ErrorInfo.Length != 0) hash ^= ErrorInfo.GetHashCode();
      if (_unknownFields != null) {
        hash ^= _unknownFields.GetHashCode();
      }
      return hash;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public override string ToString() {
      return pb::JsonFormatter.ToDiagnosticString(this);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public void WriteTo(pb::CodedOutputStream output) {
    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
      output.WriteRawMessage(this);
    #else
      if (ActionId != 0) {
        output.WriteRawTag(8);
        output.WriteInt32(ActionId);
      }
      if (UserId != 0L) {
        output.WriteRawTag(16);
        output.WriteInt64(UserId);
      }
      if (MsgId != 0) {
        output.WriteRawTag(24);
        output.WriteInt32(MsgId);
      }
      if (Token.Length != 0) {
        output.WriteRawTag(34);
        output.WriteString(Token);
      }
      if (St != 0L) {
        output.WriteRawTag(40);
        output.WriteInt64(St);
      }
      if (Content.Length != 0) {
        output.WriteRawTag(50);
        output.WriteBytes(Content);
      }
      if (Sign.Length != 0) {
        output.WriteRawTag(58);
        output.WriteString(Sign);
      }
      if (ErrorCode != 0) {
        output.WriteRawTag(80);
        output.WriteInt32(ErrorCode);
      }
      if (ErrorInfo.Length != 0) {
        output.WriteRawTag(90);
        output.WriteString(ErrorInfo);
      }
      if (_unknownFields != null) {
        _unknownFields.WriteTo(output);
      }
    #endif
    }

    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    void pb::IBufferMessage.InternalWriteTo(ref pb::WriteContext output) {
      if (ActionId != 0) {
        output.WriteRawTag(8);
        output.WriteInt32(ActionId);
      }
      if (UserId != 0L) {
        output.WriteRawTag(16);
        output.WriteInt64(UserId);
      }
      if (MsgId != 0) {
        output.WriteRawTag(24);
        output.WriteInt32(MsgId);
      }
      if (Token.Length != 0) {
        output.WriteRawTag(34);
        output.WriteString(Token);
      }
      if (St != 0L) {
        output.WriteRawTag(40);
        output.WriteInt64(St);
      }
      if (Content.Length != 0) {
        output.WriteRawTag(50);
        output.WriteBytes(Content);
      }
      if (Sign.Length != 0) {
        output.WriteRawTag(58);
        output.WriteString(Sign);
      }
      if (ErrorCode != 0) {
        output.WriteRawTag(80);
        output.WriteInt32(ErrorCode);
      }
      if (ErrorInfo.Length != 0) {
        output.WriteRawTag(90);
        output.WriteString(ErrorInfo);
      }
      if (_unknownFields != null) {
        _unknownFields.WriteTo(ref output);
      }
    }
    #endif

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public int CalculateSize() {
      int size = 0;
      if (ActionId != 0) {
        size += 1 + pb::CodedOutputStream.ComputeInt32Size(ActionId);
      }
      if (UserId != 0L) {
        size += 1 + pb::CodedOutputStream.ComputeInt64Size(UserId);
      }
      if (MsgId != 0) {
        size += 1 + pb::CodedOutputStream.ComputeInt32Size(MsgId);
      }
      if (Token.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeStringSize(Token);
      }
      if (St != 0L) {
        size += 1 + pb::CodedOutputStream.ComputeInt64Size(St);
      }
      if (Content.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeBytesSize(Content);
      }
      if (Sign.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeStringSize(Sign);
      }
      if (ErrorCode != 0) {
        size += 1 + pb::CodedOutputStream.ComputeInt32Size(ErrorCode);
      }
      if (ErrorInfo.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeStringSize(ErrorInfo);
      }
      if (_unknownFields != null) {
        size += _unknownFields.CalculateSize();
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public void MergeFrom(LGMsg other) {
      if (other == null) {
        return;
      }
      if (other.ActionId != 0) {
        ActionId = other.ActionId;
      }
      if (other.UserId != 0L) {
        UserId = other.UserId;
      }
      if (other.MsgId != 0) {
        MsgId = other.MsgId;
      }
      if (other.Token.Length != 0) {
        Token = other.Token;
      }
      if (other.St != 0L) {
        St = other.St;
      }
      if (other.Content.Length != 0) {
        Content = other.Content;
      }
      if (other.Sign.Length != 0) {
        Sign = other.Sign;
      }
      if (other.ErrorCode != 0) {
        ErrorCode = other.ErrorCode;
      }
      if (other.ErrorInfo.Length != 0) {
        ErrorInfo = other.ErrorInfo;
      }
      _unknownFields = pb::UnknownFieldSet.MergeFrom(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public void MergeFrom(pb::CodedInputStream input) {
    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
      input.ReadRawMessage(this);
    #else
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, input);
            break;
          case 8: {
            ActionId = input.ReadInt32();
            break;
          }
          case 16: {
            UserId = input.ReadInt64();
            break;
          }
          case 24: {
            MsgId = input.ReadInt32();
            break;
          }
          case 34: {
            Token = input.ReadString();
            break;
          }
          case 40: {
            St = input.ReadInt64();
            break;
          }
          case 50: {
            Content = input.ReadBytes();
            break;
          }
          case 58: {
            Sign = input.ReadString();
            break;
          }
          case 80: {
            ErrorCode = input.ReadInt32();
            break;
          }
          case 90: {
            ErrorInfo = input.ReadString();
            break;
          }
        }
      }
    #endif
    }

    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    void pb::IBufferMessage.InternalMergeFrom(ref pb::ParseContext input) {
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, ref input);
            break;
          case 8: {
            ActionId = input.ReadInt32();
            break;
          }
          case 16: {
            UserId = input.ReadInt64();
            break;
          }
          case 24: {
            MsgId = input.ReadInt32();
            break;
          }
          case 34: {
            Token = input.ReadString();
            break;
          }
          case 40: {
            St = input.ReadInt64();
            break;
          }
          case 50: {
            Content = input.ReadBytes();
            break;
          }
          case 58: {
            Sign = input.ReadString();
            break;
          }
          case 80: {
            ErrorCode = input.ReadInt32();
            break;
          }
          case 90: {
            ErrorInfo = input.ReadString();
            break;
          }
        }
      }
    }
    #endif

  }

  /// <summary>
  ///http
  /// </summary>
  public sealed partial class LGResponse : pb::IMessage<LGResponse>
  #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
      , pb::IBufferMessage
  #endif
  {
    private static readonly pb::MessageParser<LGResponse> _parser = new pb::MessageParser<LGResponse>(() => new LGResponse());
    private pb::UnknownFieldSet _unknownFields;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public static pb::MessageParser<LGResponse> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::LightGame.Protocol.MessageGameBaseReflection.Descriptor.MessageTypes[1]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public LGResponse() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public LGResponse(LGResponse other) : this() {
      errorCode_ = other.errorCode_;
      errorInfo_ = other.errorInfo_;
      content_ = other.content_;
      _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public LGResponse Clone() {
      return new LGResponse(this);
    }

    /// <summary>Field number for the "ErrorCode" field.</summary>
    public const int ErrorCodeFieldNumber = 1;
    private int errorCode_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public int ErrorCode {
      get { return errorCode_; }
      set {
        errorCode_ = value;
      }
    }

    /// <summary>Field number for the "ErrorInfo" field.</summary>
    public const int ErrorInfoFieldNumber = 2;
    private string errorInfo_ = "";
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public string ErrorInfo {
      get { return errorInfo_; }
      set {
        errorInfo_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    /// <summary>Field number for the "Content" field.</summary>
    public const int ContentFieldNumber = 3;
    private pb::ByteString content_ = pb::ByteString.Empty;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public pb::ByteString Content {
      get { return content_; }
      set {
        content_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public override bool Equals(object other) {
      return Equals(other as LGResponse);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public bool Equals(LGResponse other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (ErrorCode != other.ErrorCode) return false;
      if (ErrorInfo != other.ErrorInfo) return false;
      if (Content != other.Content) return false;
      return Equals(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public override int GetHashCode() {
      int hash = 1;
      if (ErrorCode != 0) hash ^= ErrorCode.GetHashCode();
      if (ErrorInfo.Length != 0) hash ^= ErrorInfo.GetHashCode();
      if (Content.Length != 0) hash ^= Content.GetHashCode();
      if (_unknownFields != null) {
        hash ^= _unknownFields.GetHashCode();
      }
      return hash;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public override string ToString() {
      return pb::JsonFormatter.ToDiagnosticString(this);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public void WriteTo(pb::CodedOutputStream output) {
    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
      output.WriteRawMessage(this);
    #else
      if (ErrorCode != 0) {
        output.WriteRawTag(8);
        output.WriteInt32(ErrorCode);
      }
      if (ErrorInfo.Length != 0) {
        output.WriteRawTag(18);
        output.WriteString(ErrorInfo);
      }
      if (Content.Length != 0) {
        output.WriteRawTag(26);
        output.WriteBytes(Content);
      }
      if (_unknownFields != null) {
        _unknownFields.WriteTo(output);
      }
    #endif
    }

    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    void pb::IBufferMessage.InternalWriteTo(ref pb::WriteContext output) {
      if (ErrorCode != 0) {
        output.WriteRawTag(8);
        output.WriteInt32(ErrorCode);
      }
      if (ErrorInfo.Length != 0) {
        output.WriteRawTag(18);
        output.WriteString(ErrorInfo);
      }
      if (Content.Length != 0) {
        output.WriteRawTag(26);
        output.WriteBytes(Content);
      }
      if (_unknownFields != null) {
        _unknownFields.WriteTo(ref output);
      }
    }
    #endif

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public int CalculateSize() {
      int size = 0;
      if (ErrorCode != 0) {
        size += 1 + pb::CodedOutputStream.ComputeInt32Size(ErrorCode);
      }
      if (ErrorInfo.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeStringSize(ErrorInfo);
      }
      if (Content.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeBytesSize(Content);
      }
      if (_unknownFields != null) {
        size += _unknownFields.CalculateSize();
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public void MergeFrom(LGResponse other) {
      if (other == null) {
        return;
      }
      if (other.ErrorCode != 0) {
        ErrorCode = other.ErrorCode;
      }
      if (other.ErrorInfo.Length != 0) {
        ErrorInfo = other.ErrorInfo;
      }
      if (other.Content.Length != 0) {
        Content = other.Content;
      }
      _unknownFields = pb::UnknownFieldSet.MergeFrom(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public void MergeFrom(pb::CodedInputStream input) {
    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
      input.ReadRawMessage(this);
    #else
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, input);
            break;
          case 8: {
            ErrorCode = input.ReadInt32();
            break;
          }
          case 18: {
            ErrorInfo = input.ReadString();
            break;
          }
          case 26: {
            Content = input.ReadBytes();
            break;
          }
        }
      }
    #endif
    }

    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    void pb::IBufferMessage.InternalMergeFrom(ref pb::ParseContext input) {
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, ref input);
            break;
          case 8: {
            ErrorCode = input.ReadInt32();
            break;
          }
          case 18: {
            ErrorInfo = input.ReadString();
            break;
          }
          case 26: {
            Content = input.ReadBytes();
            break;
          }
        }
      }
    }
    #endif

  }

  #endregion

}

#endregion Designer generated code
