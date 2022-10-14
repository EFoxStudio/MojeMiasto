using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using EFox.ApiConnection.Toolkit;
using MojeMiasto.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace MojeMiasto.ViewModels
{
    internal partial class ChatViewModel
    {
        Connection<Chat> user_conn = new Connection<Chat>("https://api.efox.com.pl/mycity/");
        Connection<Chat> chat_conn = new Connection<Chat>("https://api.efox.com.pl/mycity/");

        public ChatViewModel()
        {
            user_conn.AddHeader("ApiKey", "g84@RRGA%!bP8vNzK7p&uLXz&");
            chat_conn.AddHeader("ApiKey", "g84@RRGA%!bP8vNzK7p&uLXz&");
        }
    }
}
