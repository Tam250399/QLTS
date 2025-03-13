
using SoapCore.Extensibility;
using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
namespace GS.WebApi.Infrastructure
{
    public class CustomFaultExceptionTransformer : IFaultExceptionTransformer
    {
        public Message ProvideFault(Exception exception, MessageVersion messageVersion)
        {
            return Message.CreateMessage(messageVersion, exception.Message);
        }

        public FaultException Transform(Exception exception)
        {
            return new FaultException();
        }
    }
}
