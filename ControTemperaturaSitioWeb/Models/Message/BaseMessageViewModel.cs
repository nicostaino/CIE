using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ControlTemperaturaSitioWeb.Models.Message
{
    [Serializable]
    public class BaseMessageViewModel
    {
        public string Title { get; set; }
        public string Message { get; set; }

        public BaseMessageViewModel(string title, string message)
        {
            Title = title;
            Message = message;
        }

        public BaseMessageViewModel()
        {

        }
    }
}