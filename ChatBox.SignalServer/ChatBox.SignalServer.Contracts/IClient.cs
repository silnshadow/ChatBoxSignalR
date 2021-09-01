using ChatBox.SignalServer.BaseClasses;
using System.Threading.Tasks;

namespace ChatBox.SignalServer
{
    public interface IClient
    {
        void BroadcastImageMessage(byte[] img);
        void BroadcastTextMessage(dynamic name, string message);
        System.Collections.Generic.List<User> Login(string name, byte[] photo);
        void Logout();
        Task OnDisconnected(bool stopCalled);
        Task OnReconnected();
        void Typing(string recepient);
        void UnicastImageMessage(string recepient, byte[] img);
        void UnicastTextMessage(string recepient, string message);
        void ParticipantDisconnection(string userName);
        void ParticipantReconnection(string userName);
        void ParticipantLogin(User newUser);
        void ParticipantLogout(dynamic name);
        void BroadcastPictureMessage(dynamic name, byte[] img);
        void UnicastPictureMessage(dynamic sender, byte[] img);
        void ParticipantTyping(dynamic sender);
    }
}