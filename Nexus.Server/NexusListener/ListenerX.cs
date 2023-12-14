using Google.Protobuf;
using Grpc.Core;
using Grpc.Net.Client;
using System.Net.Http;
using System.Collections;
using System.Runtime.Intrinsics.X86;
using System.Security.Cryptography;
using Grpc.AspNetCore.Server;
using Microsoft.AspNetCore.Mvc;
using NexusServergRPC;
using Nexus.Server.Models;
using Nexus.Server.Encryption;
using Nexus.Server.Entity;

namespace Nexus.Server.NexusListener
{
    public class ListenerX
    {
        private const int Keysize = 256;
        private const int DerivationIterations = 1000;
        private static string? Name { get; set; }
        private static byte[]? UserAvatar {  get; set; }
        private GrpcChannel? channel;

        private static bool? IsLoggedIn = null;
        private static bool? IsSignedUp = null;
        private static bool IsConnected = false;
        private static bool? HasContacts= null;
        private static bool? HasMessages = null;

        private static List<ContactX>? contacts = null;
        private static List<MessageX>? messages = null;

        private static string? responseMessage = "";

        private static Nexus.NexusClient? client = null;
        private static NexusDb.NexusDbClient? clientDb = null;
        private static AsyncDuplexStreamingCall<ConnectedX, CreateMsgRequest>? call_messages = null;
        private static AsyncDuplexStreamingCall<SignUpRequest, SignUpResponse>? call_signup = null;
        private static AsyncDuplexStreamingCall<LoginUserRequest, LoginUserResponse>? call_login = null;
        private static AsyncDuplexStreamingCall<GetContactsRequest, GetContactsResponse>? call_contacts = null;

        private static Thread? t_call_messages = null;
        private static Thread? t_call_login = null;
        private static Thread? t_call_signup = null;
        private static Thread? t_call_contacts = null;

        private static MessageX? messageX = null;

        private static CancellationTokenSource t_call_messages_cancellationTokenSource = new CancellationTokenSource();
        private static CancellationTokenSource t_call_login_cancellationTokenSource = new CancellationTokenSource();
        private static CancellationTokenSource t_call_signup_cancellationTokenSource = new CancellationTokenSource();

        public bool SetListenerUserData(string name, string email)
        {
            return true;
        }

        private static void StartThread_Login()
        {
            t_call_login = new Thread(async () =>
            {
                while (true)
                {
                    if (call_login != null)
                    {
                        try
                        {
                            await foreach (var response in call_login.ResponseStream.ReadAllAsync())
                            {
                                IsLoggedIn = response.Response;
                                responseMessage = response.ResponseMessage;
                                messageX = response.Message;
                                UserAvatar = response.UserAvatar.ToByteArray();

                                await Console.Out.WriteLineAsync($"Received from: {response.Message.SenderName} +  {response.Message.MsgText}");
                            }
                        }
                        catch
                        {

                        }
                    }
                    else
                    {
                        //await Console.Out.WriteLineAsync("Null login");
                    }
                }
            });

            t_call_login.Start();
        }

        private static void StartThread_SignUp()
        {
            t_call_signup = new Thread(async () =>
            {
                while (true)
                {
                    if (call_signup != null)
                    {
                        try
                        {
                            await foreach (var response in call_signup.ResponseStream.ReadAllAsync())
                            {
                                IsSignedUp = response.Response;
                                responseMessage = response.ResponseMessage;
                                await Console.Out.WriteLineAsync($"SignUp: {response.Response}");
                            }
                        }
                        catch
                        {

                        }
                    }
                    else
                    {
                        //await Console.Out.WriteLineAsync("Null login");
                    }
                }
            });

            t_call_signup.Start();
        }

        private static void StartThread_Contacts()
        {
            t_call_contacts = new Thread(async () =>
            {
                while (true)
                {
                    if (call_contacts != null)
                    {
                        try
                        {
                            await foreach (var response in call_contacts.ResponseStream.ReadAllAsync())
                            {
                                foreach (var item in response.ContactList)
                                    contacts?.Add(item);
                            }
                        }
                        catch
                        {

                        }
                    }
                    else
                    {
                        //await Console.Out.WriteLineAsync("Null login");
                    }
                }
            });

            t_call_contacts.Start();
        }

        private static void StartThread_Messages()
        {
            t_call_messages = new Thread(async () =>
            {
                while (true)
                {
                    if (call_messages != null)
                    {
                        try
                        {
                            await foreach (var response in call_messages.ResponseStream.ReadAllAsync())
                            {
                                
                            }
                        }
                        catch
                        {

                        }
                    }
                    else
                    {
                        //await Console.Out.WriteLineAsync("Null login");
                    }
                }
            });

            t_call_messages.Start();
        }

        private static void TerminateThread_Login()
        {
            try
            {
                t_call_login.Abort();
            }
            catch {
                Console.WriteLine("X - uwux");
            }
            
        }

        private static void TerminateThread_SignUp()
        {
            try
            {
                t_call_signup.Abort();
            }
            catch
            {
                Console.WriteLine("X - uwux");
            }

        }

        private static void TerminateThread_Contacts()
        {
            try
            {
                t_call_contacts.Abort();
            }
            catch
            {
                Console.WriteLine("X - uwux");
            }

        }

        private async Task<bool> EstablishConnection()
        {
            try
            {
                await client.TestConnectionAsync(new EmptyX { }, deadline: DateTime.UtcNow.AddSeconds(3));
                return true;
            }
            catch
            {
                return false;
            }
        }

        public ListenerX() {
            try
            {
                try
                {
                    channel = GrpcChannel.ForAddress("https://localhost:7046");
                    client = new Nexus.NexusClient(channel);
                    clientDb = new NexusDb.NexusDbClient(channel);

                    Task.Run(async () => { await EstablishConnection(); });

                  

                    t_call_messages = new Thread(async () =>
                    {
                        while (true)
                        {
                            
                            if (call_messages != null)
                            {
                                try
                                {
                                    await foreach (var response in call_messages.ResponseStream.ReadAllAsync())
                                    {
                                        await Console.Out.WriteLineAsync($"Received: {response.ReceiverEmail}");
                                    }
                                }
                                catch
                                {

                                }
                              
                            }
                            else
                            {
                                //await Console.Out.WriteLineAsync("Null message");
                            }
                        }
                    });
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
                Task.Run(async () => await StartListen());
            }
            catch {  }
        }

        public async Task<bool> TestConnection()
        {
            try
            {
                var x = await client.TestConnectionAsync(new EmptyX(), deadline: DateTime.UtcNow.AddSeconds(6));
                Thread.Sleep(3000);
                return true;
            }
            catch (RpcException)
            {
                return false;
            }
        }

        private async Task StartListen()
        {
            Console.WriteLine(channel.State + " STATE");

            t_call_messages.Start();
        }

        public async Task Dispose()
        {
            try
            {
                await call_messages.RequestStream.CompleteAsync();
                t_call_messages.Abort();
            }
            catch (Exception) { }
            try
            {
                await call_login.RequestStream.CompleteAsync();
                t_call_login.Abort();
            }
            catch { }
            try
            {
                await call_signup.RequestStream.CompleteAsync();
                t_call_signup.Abort();
            }
            catch { }
            await Console.Out.WriteLineAsync("Connection Closed");
        }

        public static async Task SendMsg(string message)
        {
            var bytes = new byte[1];

            ByteString byteString = ByteString.CopyFrom(bytes);
            var x = new CreateMsgRequest
            {
                MsgText = "Nexus #1",
                ExtraContent = byteString,
                ReceiverEmail = message,
                ReceiverName = "Azure",
            };
           
            await client.SendMessageToStreamsAsync(x);

            await Console.Out.WriteLineAsync("Send");
        }

        public async Task InvokeSend(string message)
        {
            await SendMsg(message);
        }

        public async Task<SignUpResponse> SignUp(S_User user)
        {
            if(!await EstablishConnection())
                return new SignUpResponse { Response = false, ResponseMessage = "Can't connect to server :(" }; ;

            StartThread_SignUp();


            IsSignedUp = null;
           
            var metadata = new Metadata();
            metadata.Add("UserName", user.UserName);
            metadata.Add("UserEmail", user.UserEmail);
            metadata.Add("UserPassword", NexusCipher.ToSHA256(user.UserPassword));

            call_signup = clientDb.SignUpUser(metadata);

            int counter = 0;

            while (IsSignedUp == null && counter < 15)
            {
                await Console.Out.WriteLineAsync("Waiting for server response ^_^ <SignUp>");
                counter++;
                Thread.Sleep(500);
            }


            if (counter == 15)
                IsSignedUp = false;

            if (IsSignedUp == false)
            {
                await call_signup.RequestStream.CompleteAsync();
                TerminateThread_SignUp();
                return new SignUpResponse { Response = (bool)IsSignedUp, ResponseMessage = responseMessage };
            }

            await call_signup.RequestStream.WriteAsync(new SignUpRequest { });

            return new SignUpResponse { Response = true, ResponseMessage = responseMessage }; ;
        }



        public async Task<LoginUserResponse> Login(L_User user)
        {
            if (!await EstablishConnection())
                return new LoginUserResponse { Response = false, ResponseMessage = "Can't connect to server :("};

            StartThread_Login();

            IsLoggedIn = null;
            var metadata = new Metadata();
            metadata.Add("UserName", user.UserName);
            metadata.Add("UserPassword", NexusCipher.ToSHA256(user.UserPassword));

            call_login = clientDb.LoginUser(metadata);

            int counter = 0;
            while (IsLoggedIn == null && counter < 15)
            {
                await Console.Out.WriteLineAsync("Waiting for server response ^_^ <Login>");
                counter++;
                Thread.Sleep(500);
            }
            
            if (counter == 15)
                IsLoggedIn = false;

            if (IsLoggedIn == false)
            {
                await call_login.RequestStream.CompleteAsync();
                TerminateThread_Login();
                return new LoginUserResponse { Response = (bool)IsLoggedIn, ResponseMessage = responseMessage};
            }

            await call_login.RequestStream.WriteAsync(new LoginUserRequest { });

            Name = user.UserName;
 
            return new LoginUserResponse { Response = true, ResponseMessage = responseMessage };
        }


        public async Task<bool> Logout()
        {
            try
            {
                await Dispose();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<List<ContactX>?> GetContactsRPC(string userName)
        {
            if (!await EstablishConnection())
                return null;

            contacts = new List<ContactX>();

            var response = await clientDb.GetContactsAsync(new GetContactsRequest { UserName = userName });

            foreach (var item in response.ContactList)
            {
                contacts.Add(item);
            }

            return contacts;
        }

        public async Task<List<Message>?> GetMessagesRPC()
        {

            if (!await EstablishConnection())
                return null;

            return null;
        }

        public async Task<CurrentUser_Model> GetCurrentUserRPC()
        {
            await Console.Out.WriteLineAsync("");
            return new CurrentUser_Model { UserAvatar = UserAvatar, UserName = Name };
        }

        public async Task<bool> SendMessageToUser(Message_Model message)
        {
            ByteString bs = ByteString.CopyFrom(new byte[1]);
            MessageU m = new MessageU
            {
                MsgText = message.TextContent,
                ExtraContent = bs,
                ReceiverName = message.Receiver,
                SenderName = message.Sender,
            };
           
            var response = await client.SendMessageToUserAsync(new SendRequest
            {
                Message = m
            }); ;

            return response.IsSended;
        }
    }
}

