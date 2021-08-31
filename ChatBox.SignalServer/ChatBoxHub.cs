using ChatBox.SignalServer.BaseClasses;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatBox.SignalServer
{

    [HubName("ChatHub")]
    public class ChatHub : Hub<IClient>
    {
        public string getdetails(string user)
        {
            return "Hi " + user;
        }
    }
}
