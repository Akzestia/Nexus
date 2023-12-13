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

namespace Nexus.Server.NexusListener
{
    public class ListenerX
    {
        private const int Keysize = 256;
        private const int DerivationIterations = 1000;
        private string Name { get; set; }
        private string Email { get; set; }
        private GrpcChannel channel;
        private static bool? IsLoggedIn = null;
        private static bool? IsSignedUp = null;
        private static bool IsConnected = false;
        private static Nexus.NexusClient? client = null;
        private static NexusDb.NexusDbClient? clientDb = null;
        private static AsyncDuplexStreamingCall<ConnectedX, CreateMsgRequest>? call_messages = null;
        private static AsyncDuplexStreamingCall<SignUpRequest, SignUpResponse>? call_signup = null;
        private static AsyncDuplexStreamingCall<LoginUserRequest, LoginUserResponse>? call_login = null;

        private static Thread? t_call_messages = null;
        private static Thread? t_call_login = null;
        private static Thread? t_call_signup = null;

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
                                await Console.Out.WriteLineAsync($"Login: {response.Response}");
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

        private static void ResumeThread_Login()
        {
            t_call_login_cancellationTokenSource = new CancellationTokenSource();
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
                Name = "Azure";
                Email = "Azure@gmail.com";


                try
                {
                    channel = GrpcChannel.ForAddress("https://localhost:7046");
                    client = new Nexus.NexusClient(channel);
                    clientDb = new NexusDb.NexusDbClient(channel);

                    Task.Run(async () => { await EstablishConnection(); });
                    //t_call_login = new Thread(async () =>
                    //{
                    //    while (!t_call_login_cancellationTokenSource.Token.IsCancellationRequested)
                    //    {
                    //        if (call_login != null)
                    //        {
                    //            try
                    //            {
                    //                await foreach (var response in call_login.ResponseStream.ReadAllAsync())
                    //                {
                    //                    IsLoggedIn = response.Response;
                    //                    await Console.Out.WriteLineAsync($"Login: {response.Response}");
                    //                }
                    //            }
                    //            catch
                    //            {

                    //            }
                    //        }
                    //        else
                    //        {
                    //            //await Console.Out.WriteLineAsync("Null login");
                    //        }
                    //    }
                    //});

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
                                        await Console.Out.WriteLineAsync($"Sign Up: {response.Response}");
                                    }
                                }
                                catch
                                {

                                }
                               
                            }
                            else
                            {
                                //await Console.Out.WriteLineAsync("Null signup");
                            }
                        }
                    });

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

        public async Task<bool?> SignUp(S_User user)
        {
            if(!await EstablishConnection())
                return false;

            IsSignedUp = null;
            t_call_signup?.Start();
            var metadata = new Metadata();
            metadata.Add("UserName", user.UserName);
            metadata.Add("UserEmail", user.UserEmail);
            metadata.Add("UserPassword", NexusCipher.ToSHA256(user.UserPassword));

            call_signup = clientDb.SignUpUser(metadata);

            while (IsSignedUp == null)
            {
                await Console.Out.WriteLineAsync("Waiting for server response ^_^ <SignUp>");
                Thread.Sleep(500);
            }

            if (IsSignedUp == false)
                await call_signup.RequestStream.CompleteAsync();

            return IsSignedUp;
        }


        public async Task<bool?> Login(L_User user)
        {
            if (!await EstablishConnection())
                return false;

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
            }

            return IsLoggedIn;
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
    }
}

