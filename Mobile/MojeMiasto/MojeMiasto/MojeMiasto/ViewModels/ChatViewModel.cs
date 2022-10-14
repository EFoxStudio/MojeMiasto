using EFox.ApiConnection.Toolkit;
using MojeMiasto.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MojeMiasto.ViewModels
{
    internal partial class ChatViewModel
    {
        Connection<Chat> chat_conn = new Connection<Chat>("https://api.efox.com.pl/mycity/");
    }
}
