﻿#region Using namespaces...
using System;
using System.Net;
using System.Runtime.Serialization;
#endregion

namespace GS.Api.Infrastructure.Api
{
    /// <summary>
    /// Api Exception
    /// </summary>
    [Serializable]
    [DataContract]
    public class ApiException : Exception, IApiExceptions
    {
        #region Public Serializable properties...
        [DataMember]
        public int ErrorCode { get; set; }
        [DataMember]
        public string ErrorDescription { get; set; }
        [DataMember]
        public HttpStatusCode HttpStatus { get; set; }

        string reasonPhrase = "ApiException";

        [DataMember]
        public string ReasonPhrase
        {
            get { return reasonPhrase; }
            set { reasonPhrase = value; }
        }
        #endregion
    }
}