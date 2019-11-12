// <auto-generated>
//     Generated by the protocol buffer compiler.  DO NOT EDIT!
//     source: speak.proto
// </auto-generated>
#pragma warning disable 0414, 1591
#region Designer generated code

using grpc = global::Grpc.Core;

namespace GrpcService {
  /// <summary>
  ///定义服务
  /// </summary>
  public static partial class Speak
  {
    static readonly string __ServiceName = "Speak.Speak";

    static readonly grpc::Marshaller<global::Google.Protobuf.WellKnownTypes.Empty> __Marshaller_google_protobuf_Empty = grpc::Marshallers.Create((arg) => global::Google.Protobuf.MessageExtensions.ToByteArray(arg), global::Google.Protobuf.WellKnownTypes.Empty.Parser.ParseFrom);
    static readonly grpc::Marshaller<global::GrpcService.SpeakResult> __Marshaller_Speak_SpeakResult = grpc::Marshallers.Create((arg) => global::Google.Protobuf.MessageExtensions.ToByteArray(arg), global::GrpcService.SpeakResult.Parser.ParseFrom);
    static readonly grpc::Marshaller<global::GrpcService.ChatRequest> __Marshaller_Speak_ChatRequest = grpc::Marshallers.Create((arg) => global::Google.Protobuf.MessageExtensions.ToByteArray(arg), global::GrpcService.ChatRequest.Parser.ParseFrom);
    static readonly grpc::Marshaller<global::GrpcService.ChatResponse> __Marshaller_Speak_ChatResponse = grpc::Marshallers.Create((arg) => global::Google.Protobuf.MessageExtensions.ToByteArray(arg), global::GrpcService.ChatResponse.Parser.ParseFrom);
    static readonly grpc::Marshaller<global::GrpcService.CountResult> __Marshaller_Speak_CountResult = grpc::Marshallers.Create((arg) => global::Google.Protobuf.MessageExtensions.ToByteArray(arg), global::GrpcService.CountResult.Parser.ParseFrom);

    static readonly grpc::Method<global::Google.Protobuf.WellKnownTypes.Empty, global::GrpcService.SpeakResult> __Method_SpeakL = new grpc::Method<global::Google.Protobuf.WellKnownTypes.Empty, global::GrpcService.SpeakResult>(
        grpc::MethodType.Unary,
        __ServiceName,
        "SpeakL",
        __Marshaller_google_protobuf_Empty,
        __Marshaller_Speak_SpeakResult);

    static readonly grpc::Method<global::GrpcService.ChatRequest, global::GrpcService.ChatResponse> __Method_Chat = new grpc::Method<global::GrpcService.ChatRequest, global::GrpcService.ChatResponse>(
        grpc::MethodType.DuplexStreaming,
        __ServiceName,
        "Chat",
        __Marshaller_Speak_ChatRequest,
        __Marshaller_Speak_ChatResponse);

    static readonly grpc::Method<global::Google.Protobuf.WellKnownTypes.Empty, global::GrpcService.CountResult> __Method_Count = new grpc::Method<global::Google.Protobuf.WellKnownTypes.Empty, global::GrpcService.CountResult>(
        grpc::MethodType.Unary,
        __ServiceName,
        "Count",
        __Marshaller_google_protobuf_Empty,
        __Marshaller_Speak_CountResult);

    /// <summary>Service descriptor</summary>
    public static global::Google.Protobuf.Reflection.ServiceDescriptor Descriptor
    {
      get { return global::GrpcService.SpeakReflection.Descriptor.Services[0]; }
    }

    /// <summary>Base class for server-side implementations of Speak</summary>
    [grpc::BindServiceMethod(typeof(Speak), "BindService")]
    public abstract partial class SpeakBase
    {
      /// <summary>
      ///定义方法
      ///说话方法
      /// </summary>
      /// <param name="request">The request received from the client.</param>
      /// <param name="context">The context of the server-side call handler being invoked.</param>
      /// <returns>The response to send back to the client (wrapped by a task).</returns>
      public virtual global::System.Threading.Tasks.Task<global::GrpcService.SpeakResult> SpeakL(global::Google.Protobuf.WellKnownTypes.Empty request, grpc::ServerCallContext context)
      {
        throw new grpc::RpcException(new grpc::Status(grpc::StatusCode.Unimplemented, ""));
      }

      /// <summary>
      ///定义聊天双向流rpc
      /// </summary>
      /// <param name="requestStream">Used for reading requests from the client.</param>
      /// <param name="responseStream">Used for sending responses back to the client.</param>
      /// <param name="context">The context of the server-side call handler being invoked.</param>
      /// <returns>A task indicating completion of the handler.</returns>
      public virtual global::System.Threading.Tasks.Task Chat(grpc::IAsyncStreamReader<global::GrpcService.ChatRequest> requestStream, grpc::IServerStreamWriter<global::GrpcService.ChatResponse> responseStream, grpc::ServerCallContext context)
      {
        throw new grpc::RpcException(new grpc::Status(grpc::StatusCode.Unimplemented, ""));
      }

      /// <summary>
      ///定义统计简单rpc
      /// </summary>
      /// <param name="request">The request received from the client.</param>
      /// <param name="context">The context of the server-side call handler being invoked.</param>
      /// <returns>The response to send back to the client (wrapped by a task).</returns>
      public virtual global::System.Threading.Tasks.Task<global::GrpcService.CountResult> Count(global::Google.Protobuf.WellKnownTypes.Empty request, grpc::ServerCallContext context)
      {
        throw new grpc::RpcException(new grpc::Status(grpc::StatusCode.Unimplemented, ""));
      }

    }

    /// <summary>Creates service definition that can be registered with a server</summary>
    /// <param name="serviceImpl">An object implementing the server-side handling logic.</param>
    public static grpc::ServerServiceDefinition BindService(SpeakBase serviceImpl)
    {
      return grpc::ServerServiceDefinition.CreateBuilder()
          .AddMethod(__Method_SpeakL, serviceImpl.SpeakL)
          .AddMethod(__Method_Chat, serviceImpl.Chat)
          .AddMethod(__Method_Count, serviceImpl.Count).Build();
    }

    /// <summary>Register service method with a service binder with or without implementation. Useful when customizing the  service binding logic.
    /// Note: this method is part of an experimental API that can change or be removed without any prior notice.</summary>
    /// <param name="serviceBinder">Service methods will be bound by calling <c>AddMethod</c> on this object.</param>
    /// <param name="serviceImpl">An object implementing the server-side handling logic.</param>
    public static void BindService(grpc::ServiceBinderBase serviceBinder, SpeakBase serviceImpl)
    {
      serviceBinder.AddMethod(__Method_SpeakL, serviceImpl == null ? null : new grpc::UnaryServerMethod<global::Google.Protobuf.WellKnownTypes.Empty, global::GrpcService.SpeakResult>(serviceImpl.SpeakL));
      serviceBinder.AddMethod(__Method_Chat, serviceImpl == null ? null : new grpc::DuplexStreamingServerMethod<global::GrpcService.ChatRequest, global::GrpcService.ChatResponse>(serviceImpl.Chat));
      serviceBinder.AddMethod(__Method_Count, serviceImpl == null ? null : new grpc::UnaryServerMethod<global::Google.Protobuf.WellKnownTypes.Empty, global::GrpcService.CountResult>(serviceImpl.Count));
    }

  }
}
#endregion
