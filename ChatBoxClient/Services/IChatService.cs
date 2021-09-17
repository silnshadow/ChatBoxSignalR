using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ChatBoxClient.Enums;
using ChatBoxClient.Models;

namespace ChatBoxClient.Services
{
    public interface IChatService
    {
        event Action ConnectionClosed;
        event Action ConnectionReconnected;
        event Action ConnectionReconnecting;
        event Action<string, byte[], MessageType> NewImageMessage;
        event Action<string, string, MessageType> NewTextMessage;
        event Action<string> ParticipantDisconnected;
        event Action<User> ParticipantLoggedIn;
        event Action<string> ParticipantLoggedOut;
        event Action<string> ParticipantReconnected;
        event Action<string> ParticipantTyping;

        Task ConnectAsync();
        Task<List<User>> LoginAsync(string name, byte[] photo);
        Task LogoutAsync();
        Task SendBroadcastMessageAsync(byte[] img);
        Task SendBroadcastMessageAsync(string msg);
        Task SendUnicastMessageAsync(string recepient, byte[] img);
        Task SendUnicastMessageAsync(string recepient, string msg);
        Task TypingAsync(string recepient);
    }
}