﻿using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.Service.Hubs
{
    //HubName 这个特性是为了让客户端知道如何建立与服务器端对应服务的代理对象，
    //如果没有设定该属性，则以服务器端的服务类名字作为 HubName 的缺省值
    //[HubName("chat")]
    public class ChatHub : Hub
    {
        public override Task OnConnectedAsync()
        {
            //建立连接
            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            //断开连接
            return base.OnDisconnectedAsync(exception);
        }

        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }
    }
}
