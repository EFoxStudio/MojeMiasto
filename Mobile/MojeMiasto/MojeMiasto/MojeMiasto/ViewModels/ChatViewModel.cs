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
    internal partial class ChatViewModel : BaseViewModel
    {
        Connection<Chat> chat_conn = new Connection<Chat>("https://api.efox.com.pl/mycity/");

        [ObservableProperty]
        ObservableCollection<Chat> chats;

        public ChatViewModel()
        {
            chat_conn.AddHeader("ApiKey", "g84@RRGA%!bP8vNzK7p&uLXz&");
        }

        [RelayCommand]
        public async void ShowChat()
        {
            
            int user_id = Preferences.Get("user_id", 0);
            if (user_id == null)
                return;
            List<Chat> chat = await chat_conn.GetList($"chat/user_id/{user_id}");
            Chats = new ObservableCollection<Chat>(chat);
        }

    }
}
