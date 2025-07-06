using Microsoft.AspNetCore.Mvc;
using System;
using System.Net.WebSockets;
using System.Text;
using TE.TT.MarketApi.Abstarct;
using TE.TT.MarketApi.Migrations;
using static System.Net.Mime.MediaTypeNames;

namespace TE.TT.MarketApi.Controllers
{
    public class WebsoketController : ControllerBase
    {
        private WebSocket _webSocket;
        private IControlTokenService _controlTokenService;
        public WebsoketController(IControlTokenService controlTokenService)
        {
            _controlTokenService = controlTokenService;
        }
        [HttpGet("/ws")]
        public async Task Get()
        {
            if (HttpContext.WebSockets.IsWebSocketRequest)
            {

                using var webSocket = await HttpContext.WebSockets.AcceptWebSocketAsync();
                var buffer = new byte[1024 * 4];
                var bufferRevice = new byte[1024 * 4];
                var receiveResult = await webSocket.ReceiveAsync(
                    new ArraySegment<byte>(buffer), CancellationToken.None);
                string json = Encoding.UTF8.GetString(buffer, 0, receiveResult.Count);
                Console.WriteLine(json);
                //client socket
                Array.Clear(buffer, 0, buffer.Length);
                using ClientWebSocket ws = new();
                var token = await _controlTokenService.GetValidToken();
                await ws.ConnectAsync(new Uri($"wss://platform.fintacharts.com/api/streaming/ws/v1/realtime?token={token}"), CancellationToken.None);
                var result = await ws.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
                Console.WriteLine("-1" + Encoding.UTF8.GetString(buffer));
                Array.Clear(buffer, 0, buffer.Length);
                await ws.SendAsync(new ArraySegment<byte>(Encoding.UTF8.GetBytes(json)), WebSocketMessageType.Text, true, CancellationToken.None);
                Array.Clear(buffer, 0, buffer.Length);
                result = await ws.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
                Console.WriteLine("0" + Encoding.UTF8.GetString(buffer));
                try
                {
                    Task.Run(async () =>
                    {
                        while (!webSocket.CloseStatus.HasValue)
                        {
                            var receiveResult = await webSocket.ReceiveAsync(
                                new ArraySegment<byte>(bufferRevice), CancellationToken.None);
                            string json = Encoding.UTF8.GetString(bufferRevice, 0, receiveResult.Count);
                            await ws.SendAsync(new ArraySegment<byte>(Encoding.UTF8.GetBytes(json)), WebSocketMessageType.Text, true, CancellationToken.None);
                            if (receiveResult.CloseStatus.HasValue)
                            {
                                await webSocket.CloseAsync(
                                    receiveResult.CloseStatus.Value,
                                    receiveResult.CloseStatusDescription,
                                    CancellationToken.None);
                            }
                        }
                    });
                    while (!webSocket.CloseStatus.HasValue)
                    {
                        if (result.CloseStatus.HasValue)
                        {
                            Console.WriteLine("disconnecting from remote (ws)...");
                            await ws.CloseAsync(WebSocketCloseStatus.NormalClosure, "Session ended", CancellationToken.None);
                            break;
                        }
                        await webSocket.SendAsync(new ArraySegment<byte>(buffer), WebSocketMessageType.Text, true,
                            CancellationToken.None);
                        Console.WriteLine("1" + Encoding.UTF8.GetString(buffer));

                        Array.Clear(buffer, 0, buffer.Length);
                        result = await ws.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
                        Console.WriteLine("2" + Encoding.UTF8.GetString(buffer));

                    }
                    //сделать что-бы закрывался и клент 1;
                    //await webSocket.CloseAsync(
                    //WebSocketCloseStatus.NormalClosure,
                    //"Session ended",
                    //CancellationToken.None);
                    //Console.WriteLine("stop soket? server");

                    //await ws.CloseAsync(
                    //WebSocketCloseStatus.NormalClosure,
                    //"Session ended",
                    //CancellationToken.None);
                    //Console.WriteLine("stop soket? client");
                }
                catch (Exception ex)
                {   
                    Console.WriteLine("Error Soket" + ex);
                    return;
                }
                //var buffer = new byte[1024 * 4];
                //var receiveResult = await webSocket.ReceiveAsync(
                //    new ArraySegment<byte>(buffer), CancellationToken.None);
                //Console.WriteLine(Encoding.UTF8.GetString(buffer, 0, receiveResult.Count));

                //await webSocket.CloseAsync(
                //    receiveResult.CloseStatus.Value,
                //    receiveResult.CloseStatusDescription,
                //    CancellationToken.None);
                //await webSocket.CloseAsync(
                //    receiveResult.CloseStatus.Value,
                //    receiveResult.CloseStatusDescription,
                //    CancellationToken.None);
                //await webSocket.CloseAsync(
                //WebSocketCloseStatus.NormalClosure,
                //"Session ended",
                //CancellationToken.None);
                //Console.WriteLine("stop soket? server");
            }
        }
    }
}
