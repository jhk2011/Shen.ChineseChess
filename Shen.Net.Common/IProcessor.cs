using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Shen.Net.Common {
    public interface IProcessor {
        bool Process(ProcessContext context);
        bool Close(TcpHelper helper);
    }


    public class ServiceHandler : IProcessor {

        object service;

        public ServiceHandler(object service) {
            this.service = service;
        }

        public bool Close(TcpHelper helper) {
            return false;
        }

        public bool Process(ProcessContext context) {

            var helper = context.Helper;

            var message = context.Message;

            string service = message.GetValue<string>("service");

            if (service == null) return false;

            string action = message.GetValue<string>("action");

            if (action == null) return false;

            MethodInfo mi = service.GetType().GetMethod(action);

            var parameters = mi.GetParameters();

            object[] args = new object[parameters.Length];

            for (int i = 0; i < parameters.Length; i++) {
                object value;
                if (message.Values.TryGetValue(parameters[i].Name, out value)) {
                    args[i] = value;
                }
            }

            Message reply = new Message();

            object result = mi.Invoke(service, args);

            reply.Values["service"] = service;
            reply.Values["action"] = action;


            for (int i = 0; i < parameters.Length; i++) {
                var p = parameters[i];

                if (!p.IsOut) continue;

                reply.Values[p.Name] = args[i];
            }

            helper.SendMessageAsync(reply);

            return true;
        }
    }

    public interface ICalc {
        void Add(double x, double y);
    }

    public class Calc : ICalc {
        public void Add(double x, double y) {
            throw new NotImplementedException();
        }
    }

    public class ServiceProxy : BaseRealProxy {

        ServiceProxyHandler serviceProxyHandler;

        public string service { get; set; }

        public ServiceProxy(Type type) : base(type) {
            
        }

        public override void OnInvoke(InvocationContext context) {
            var ps = context.Method.GetParameters();

            Dictionary<string, object> args = new Dictionary<string, object>();

            for (int i = 0; i < ps.Length; i++) {
                var p = ps[i];
                args.Add(p.Name, context.Arguments[i]);
            }

            serviceProxyHandler.Invoke(service, context.Method.Name, args);
        }
    }

    public class ServiceProxyHandler {

        TcpHelper helper;

        public void Invoke(string service, string action, Dictionary<string, object> args) {
            Message message = new Message();

            message.Values["id"] = Guid.NewGuid();
            message.Values["service"] = service;
            message.Values["action"] = action;
            foreach (var item in args) {
                message.Values.Add(item.Key, item.Value);
            }
            helper.SendMessageAsync(message);
        }
    }


}
