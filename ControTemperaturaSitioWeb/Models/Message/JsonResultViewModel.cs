using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ControlTemperaturaSitioWeb.Models.Message
{
    [Serializable]
    public class JsonResultViewModel : BaseMessageViewModel
    {
        public bool IsSuccess { get; set; }
        public string RedirectUrl { get; set; }
    }
}