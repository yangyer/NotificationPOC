using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;

namespace SolarWindDemo.Mvc
{
    public class MyHub : Hub
    {
        private readonly Responder _instance;

        public MyHub()
            : this(Responder.Instance)
        {

        }

        public MyHub(Responder instance)
        {
            _instance = instance;
        }

        public void Send(string message)
        {
            Clients.All.hello(message);
        }
    }

    public class Responder
    {
        private readonly static Lazy<Responder> _instance = new Lazy<Responder>(() => new Responder(GlobalHost.ConnectionManager.GetHubContext<MyHub>().Clients));
        private readonly object _stateLock = new object();

        public static Responder Instance
        {
            get
            {
                return _instance.Value;
            }
        }

        private IHubConnectionContext<dynamic> Clients
        {
            get;
            set;
        }

        public Responder(IHubConnectionContext<dynamic> clients)
        {
            Clients = clients;
        }

        public void NotifyClients(string msg)
        {
            lock (_stateLock)
            {
                Clients.All.hello(msg);

            }
        }
    }
}