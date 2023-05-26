using System;
using System.IO;

namespace Neo4j.Samples.Application.Common.BaseModels
{
    public class ServiceResult<T> 
    {
        public T ResultObject { get; set; }
        public string Message { get; set; } = "Success";
        public MessageType MessageType { get; set; } = MessageType.Success;
        public int StatusCode { get; set; }

        public ServiceResult()
        {
          
        }

        public ServiceResult(T result)
        {
            ResultObject = result;
        }

        public ServiceResult(MessageType messageType, string message)
        {
            Message = message;
            MessageType = messageType;
        }
    }

    public enum MessageType
    {
        None,
        Info,
        Warning,
        Success,
        Danger
    }
}