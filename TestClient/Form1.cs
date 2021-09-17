using Microsoft.AspNet.SignalR.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestClient
{
    public partial class Form1 : Form
    {
        public HubConnection hubConnection = null;
        public IHubProxy HubProxy = null;

        public Form1()
        {
            InitializeComponent();
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            try
            {
                var url = ConfigurationManager.AppSettings["ChatHubEndPoint"];
                hubConnection = new HubConnection(url);
                HubProxy = hubConnection.CreateHubProxy("ChatBoxHub");
                await hubConnection.Start();
                Execute();

                var testResult = HubProxy.Invoke<string>("getdetails", "Silnshadow").Result;
                string caption = "  Connected !  ";
                MessageBox.Show(testResult,caption);
            }
            
            catch (Exception ex)
            {
                Console.WriteLine(ex.InnerException.Message);
            }
        }
        private void Execute()
        {

            hubConnection.Start().ContinueWith(task =>
            {
                if (task.IsFaulted)
                {
                    Console.WriteLine("There was an error opening the connection:{0}",
                                      task.Exception.GetBaseException());

                    return;
                }
                else
                {
                    Console.WriteLine("Connected to Server.The ConnectionID is:" + hubConnection.ConnectionId);

                }

            }).Wait();
        }
    }
}
