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
    public class ChatBoxHub : Hub<IClient>
    {
        private static ConcurrentDictionary<string, User> ChatClients = new ConcurrentDictionary<string, User>();
        public ChatBoxHub()
        {
            User newUser = new User { Name = "Naga", ID = "123", Photo = null };
            ChatClients.TryAdd("Naga",newUser);
        }

        /// <summary>
        /// This is only used for testing connection
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public string getdetails(string user)
        {
            return "Hi" + user;
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            var userName = ChatClients.SingleOrDefault((c) => c.Value.ID == Context.ConnectionId).Key;
            if (userName != null)
            {
                Clients.Others.ParticipantDisconnection(userName);
                Console.WriteLine($"<> {userName} disconnected");
            }
            return base.OnDisconnected(stopCalled);
        }

        public override Task OnReconnected()
        {
            var userName = ChatClients.SingleOrDefault((c) => c.Value.ID == Context.ConnectionId).Key;
            if (userName != null)
            {
                Clients.Others.ParticipantReconnection(userName);
                Console.WriteLine($"== {userName} reconnected");
            }
            return base.OnReconnected();
        }

        public List<User> Login(string name, byte[] photo)
        {
            try
            {
                if (name != null)
                {
                    if (!ChatClients.ContainsKey(name))
                    {
                        Console.WriteLine($"++ {name} logged in");
                        List<User> users = new List<User>(ChatClients.Values);
                        User newUser = new User { Name = name, ID = Context.ConnectionId, Photo = photo };
                        var added = ChatClients.TryAdd(name, newUser);
                        if (!added) return null;
                        Clients.CallerState.UserName = name;
                        Clients.Others.ParticipantLogin(newUser);
                        return users;
                    }
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.InnerException.Message);
            }
            return null;
        }

        public void Logout()
        {
            var name = Clients.CallerState.UserName;
            if (!string.IsNullOrEmpty(name))
            {
                User client = new User();
                ChatClients.TryRemove(name, out client);
                Clients.Others.ParticipantLogout(name);
                Console.WriteLine($"-- {name} logged out");
            }
        }

        public void BroadcastTextMessage(string message)
        {
            var name = Clients.CallerState.UserName;
            if (!string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(message))
            {
                Clients.Others.BroadcastTextMessage(name, message);
            }
        }

        public void BroadcastImageMessage(byte[] img)
        {
            var name = Clients.CallerState.UserName;
            if (img != null)
            {
                Clients.Others.BroadcastPictureMessage(name, img);
            }
        }

        public void UnicastTextMessage(string recepient, string message)
        {
            var sender = Clients.CallerState.UserName;
            if (!string.IsNullOrEmpty(sender) && recepient != sender &&
                !string.IsNullOrEmpty(message) && ChatClients.ContainsKey(recepient))
            {
                User client = new User();
                ChatClients.TryGetValue(recepient, out client);
                Clients.Client(client.ID).UnicastTextMessage(sender, message);
            }
        }

        public void UnicastImageMessage(string recepient, byte[] img)
        {
            var sender = Clients.CallerState.UserName;
            if (!string.IsNullOrEmpty(sender) && recepient != sender &&
                img != null && ChatClients.ContainsKey(recepient))
            {
                User client = new User();
                ChatClients.TryGetValue(recepient, out client);
                Clients.Client(client.ID).UnicastPictureMessage(sender, img);
            }
        }

        public void Typing(string recepient)
        {
            try
            {
                if (string.IsNullOrEmpty(recepient)) return;
                var sender = Clients.CallerState.UserName;
                User client = new User();
                ChatClients.TryGetValue(recepient, out client);
                Clients.Client(client.ID).ParticipantTyping(sender);
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
