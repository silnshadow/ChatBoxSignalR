using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatBox.SignalServer
{
    [HubName("ChatHub")]
    public class ChatBoxHub : Hub
    {
        public string getdetails(string user)
        {
            return "Hi " + user;
        }
    }
}
