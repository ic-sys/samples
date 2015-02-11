// ------------------------------------------------------------------------------
//  <autogenerated>
//      This code was generated by a tool.
//      Mono Runtime Version: 4.0.30319.17020
// 
//      Changes to this file may cause incorrect behavior and will be lost if 
//      the code is regenerated.
//  </autogenerated>
// ------------------------------------------------------------------------------

namespace send.testei.sst.dk {
    
    
    /// <remarks/>
    [System.Web.Services.WebServiceBinding(Name="FrontendSoap", Namespace="https://ei.sst.dk/")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class Frontend : System.Web.Services.Protocols.SoapHttpClientProtocol {
        
        private System.Threading.SendOrPostCallback SendPacketsWithValidationOperationCompleted;
        
        private System.Threading.SendOrPostCallback SendPacketsOperationCompleted;
        
        private System.Threading.SendOrPostCallback SendACKPacketsOperationCompleted;
        
        private System.Threading.SendOrPostCallback GetPacketsOperationCompleted;
        
        private System.Threading.SendOrPostCallback GetPackets2OperationCompleted;
        
        public Frontend() {
            this.Url = "https://ei.sst.dk/test-ei/Frontend.asmx";
        }
        
        public Frontend(string url) {
            this.Url = url;
        }
        
        public event SendPacketsWithValidationCompletedEventHandler SendPacketsWithValidationCompleted;
        
        public event SendPacketsCompletedEventHandler SendPacketsCompleted;
        
        public event SendACKPacketsCompletedEventHandler SendACKPacketsCompleted;
        
        public event GetPacketsCompletedEventHandler GetPacketsCompleted;
        
        public event GetPackets2CompletedEventHandler GetPackets2Completed;
        
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("https://ei.sst.dk/SendPacketsWithValidation", RequestNamespace="https://ei.sst.dk/", ResponseNamespace="https://ei.sst.dk/", ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped, Use=System.Web.Services.Description.SoapBindingUse.Literal)]
        public SendPacketsWithValidationResult SendPacketsWithValidation([System.Xml.Serialization.XmlArrayItem(IsNullable=false)] SoapPacket[] Packets) {
            object[] results = this.Invoke("SendPacketsWithValidation", new object[] {
                        Packets});
            return ((SendPacketsWithValidationResult)(results[0]));
        }
        
        public System.IAsyncResult BeginSendPacketsWithValidation(SoapPacket[] Packets, System.AsyncCallback callback, object asyncState) {
            return this.BeginInvoke("SendPacketsWithValidation", new object[] {
                        Packets}, callback, asyncState);
        }
        
        public SendPacketsWithValidationResult EndSendPacketsWithValidation(System.IAsyncResult asyncResult) {
            object[] results = this.EndInvoke(asyncResult);
            return ((SendPacketsWithValidationResult)(results[0]));
        }
        
        public void SendPacketsWithValidationAsync(SoapPacket[] Packets) {
            this.SendPacketsWithValidationAsync(Packets, null);
        }
        
        public void SendPacketsWithValidationAsync(SoapPacket[] Packets, object userState) {
            if ((this.SendPacketsWithValidationOperationCompleted == null)) {
                this.SendPacketsWithValidationOperationCompleted = new System.Threading.SendOrPostCallback(this.OnSendPacketsWithValidationCompleted);
            }
            this.InvokeAsync("SendPacketsWithValidation", new object[] {
                        Packets}, this.SendPacketsWithValidationOperationCompleted, userState);
        }
        
        private void OnSendPacketsWithValidationCompleted(object arg) {
            if ((this.SendPacketsWithValidationCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.SendPacketsWithValidationCompleted(this, new SendPacketsWithValidationCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("https://ei.sst.dk/SendPackets", RequestNamespace="https://ei.sst.dk/", ResponseNamespace="https://ei.sst.dk/", ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped, Use=System.Web.Services.Description.SoapBindingUse.Literal)]
        public string[] SendPackets([System.Xml.Serialization.XmlArrayItem(IsNullable=false)] SoapPacket[] Packets) {

			object[] results = this.Invoke("SendPackets", new object[] {
                        Packets});
            return ((string[])(results[0]));
        }
        
        public System.IAsyncResult BeginSendPackets(SoapPacket[] Packets, System.AsyncCallback callback, object asyncState) {
            return this.BeginInvoke("SendPackets", new object[] {
                        Packets}, callback, asyncState);
        }
        
        public string[] EndSendPackets(System.IAsyncResult asyncResult) {
            object[] results = this.EndInvoke(asyncResult);
            return ((string[])(results[0]));
        }
        
        public void SendPacketsAsync(SoapPacket[] Packets) {
            this.SendPacketsAsync(Packets, null);
        }
        
        public void SendPacketsAsync(SoapPacket[] Packets, object userState) {
            if ((this.SendPacketsOperationCompleted == null)) {
                this.SendPacketsOperationCompleted = new System.Threading.SendOrPostCallback(this.OnSendPacketsCompleted);
            }
            this.InvokeAsync("SendPackets", new object[] {
                        Packets}, this.SendPacketsOperationCompleted, userState);
        }
        
        private void OnSendPacketsCompleted(object arg) {
            if ((this.SendPacketsCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.SendPacketsCompleted(this, new SendPacketsCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("https://ei.sst.dk/SendACKPackets", RequestNamespace="https://ei.sst.dk/", ResponseNamespace="https://ei.sst.dk/", ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped, Use=System.Web.Services.Description.SoapBindingUse.Literal)]
        public bool SendACKPackets([System.Xml.Serialization.XmlArrayItem(IsNullable=false)] ACKPacket[] Packets) {
            object[] results = this.Invoke("SendACKPackets", new object[] {
                        Packets});
            return ((bool)(results[0]));
        }
        
        public System.IAsyncResult BeginSendACKPackets(ACKPacket[] Packets, System.AsyncCallback callback, object asyncState) {
            return this.BeginInvoke("SendACKPackets", new object[] {
                        Packets}, callback, asyncState);
        }
        
        public bool EndSendACKPackets(System.IAsyncResult asyncResult) {
            object[] results = this.EndInvoke(asyncResult);
            return ((bool)(results[0]));
        }
        
        public void SendACKPacketsAsync(ACKPacket[] Packets) {
            this.SendACKPacketsAsync(Packets, null);
        }
        
        public void SendACKPacketsAsync(ACKPacket[] Packets, object userState) {
            if ((this.SendACKPacketsOperationCompleted == null)) {
                this.SendACKPacketsOperationCompleted = new System.Threading.SendOrPostCallback(this.OnSendACKPacketsCompleted);
            }
            this.InvokeAsync("SendACKPackets", new object[] {
                        Packets}, this.SendACKPacketsOperationCompleted, userState);
        }
        
        private void OnSendACKPacketsCompleted(object arg) {
            if ((this.SendACKPacketsCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.SendACKPacketsCompleted(this, new SendACKPacketsCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("https://ei.sst.dk/GetPackets", RequestNamespace="https://ei.sst.dk/", ResponseNamespace="https://ei.sst.dk/", ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped, Use=System.Web.Services.Description.SoapBindingUse.Literal)]
        [return: System.Xml.Serialization.XmlArrayItem(IsNullable=false)]
        public SoapPacket[] GetPackets(string[] PacketIDs) {
            object[] results = this.Invoke("GetPackets", new object[] {
                        PacketIDs});
            return ((SoapPacket[])(results[0]));
        }
        
        public System.IAsyncResult BeginGetPackets(string[] PacketIDs, System.AsyncCallback callback, object asyncState) {
            return this.BeginInvoke("GetPackets", new object[] {
                        PacketIDs}, callback, asyncState);
        }
        
        public SoapPacket[] EndGetPackets(System.IAsyncResult asyncResult) {
            object[] results = this.EndInvoke(asyncResult);
            return ((SoapPacket[])(results[0]));
        }
        
        public void GetPacketsAsync(string[] PacketIDs) {
            this.GetPacketsAsync(PacketIDs, null);
        }
        
        public void GetPacketsAsync(string[] PacketIDs, object userState) {
            if ((this.GetPacketsOperationCompleted == null)) {
                this.GetPacketsOperationCompleted = new System.Threading.SendOrPostCallback(this.OnGetPacketsCompleted);
            }
            this.InvokeAsync("GetPackets", new object[] {
                        PacketIDs}, this.GetPacketsOperationCompleted, userState);
        }
        
        private void OnGetPacketsCompleted(object arg) {
            if ((this.GetPacketsCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.GetPacketsCompleted(this, new GetPacketsCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("https://ei.sst.dk/GetPackets2", RequestNamespace="https://ei.sst.dk/", ResponseNamespace="https://ei.sst.dk/", ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped, Use=System.Web.Services.Description.SoapBindingUse.Literal)]
        [return: System.Xml.Serialization.XmlArrayItem(IsNullable=false)]
        public SoapPacket[] GetPackets2(string[] PacketIDs, string[] OriginalMethodNames) {
            object[] results = this.Invoke("GetPackets2", new object[] {
                        PacketIDs,
                        OriginalMethodNames});
            return ((SoapPacket[])(results[0]));
        }
        
        public System.IAsyncResult BeginGetPackets2(string[] PacketIDs, string[] OriginalMethodNames, System.AsyncCallback callback, object asyncState) {
            return this.BeginInvoke("GetPackets2", new object[] {
                        PacketIDs,
                        OriginalMethodNames}, callback, asyncState);
        }
        
        public SoapPacket[] EndGetPackets2(System.IAsyncResult asyncResult) {
            object[] results = this.EndInvoke(asyncResult);
            return ((SoapPacket[])(results[0]));
        }
        
        public void GetPackets2Async(string[] PacketIDs, string[] OriginalMethodNames) {
            this.GetPackets2Async(PacketIDs, OriginalMethodNames, null);
        }
        
        public void GetPackets2Async(string[] PacketIDs, string[] OriginalMethodNames, object userState) {
            if ((this.GetPackets2OperationCompleted == null)) {
                this.GetPackets2OperationCompleted = new System.Threading.SendOrPostCallback(this.OnGetPackets2Completed);
            }
            this.InvokeAsync("GetPackets2", new object[] {
                        PacketIDs,
                        OriginalMethodNames}, this.GetPackets2OperationCompleted, userState);
        }
        
        private void OnGetPackets2Completed(object arg) {
            if ((this.GetPackets2Completed != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.GetPackets2Completed(this, new GetPackets2CompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.17020")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="https://ei.sst.dk/")]
    public partial class SoapPacket {
        
        /// <remarks/>
        public string SoapData;
        
        /// <remarks/>
        public bool Found;
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.17020")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="https://ei.sst.dk/")]
    public partial class SendPacketsWithValidationResult {
        
        /// <remarks/>
        public string[] PacketIDs;
        
        /// <remarks/>
        public ErrorMessage[] ErrorMessages;
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.17020")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://ei.sst.dk/")]
    public partial class ErrorMessage {
        
        /// <remarks/>
        public int errorCode;
        
        /// <remarks/>
        public string errorString;
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.17020")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="https://ei.sst.dk/")]
    public partial class ACKPacket {
        
        /// <remarks/>
        public PacketPriority Priority;
        
        /// <remarks/>
        public string PacketID;
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.17020")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="https://ei.sst.dk/")]
    public enum PacketPriority {
        
        /// <remarks/>
        OneWay,
        
        /// <remarks/>
        High,
        
        /// <remarks/>
        Admin,
        
        /// <remarks/>
        OWAdmin,
    }
    
    public partial class SendPacketsWithValidationCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal SendPacketsWithValidationCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        public SendPacketsWithValidationResult Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((SendPacketsWithValidationResult)(this.results[0]));
            }
        }
    }
    
    public delegate void SendPacketsWithValidationCompletedEventHandler(object sender, SendPacketsWithValidationCompletedEventArgs args);
    
    public partial class SendPacketsCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal SendPacketsCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        public string[] Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((string[])(this.results[0]));
            }
        }
    }
    
    public delegate void SendPacketsCompletedEventHandler(object sender, SendPacketsCompletedEventArgs args);
    
    public partial class SendACKPacketsCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal SendACKPacketsCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        public bool Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((bool)(this.results[0]));
            }
        }
    }
    
    public delegate void SendACKPacketsCompletedEventHandler(object sender, SendACKPacketsCompletedEventArgs args);
    
    public partial class GetPacketsCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal GetPacketsCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        public SoapPacket[] Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((SoapPacket[])(this.results[0]));
            }
        }
    }
    
    public delegate void GetPacketsCompletedEventHandler(object sender, GetPacketsCompletedEventArgs args);
    
    public partial class GetPackets2CompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal GetPackets2CompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        public SoapPacket[] Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((SoapPacket[])(this.results[0]));
            }
        }
    }
    
    public delegate void GetPackets2CompletedEventHandler(object sender, GetPackets2CompletedEventArgs args);
}