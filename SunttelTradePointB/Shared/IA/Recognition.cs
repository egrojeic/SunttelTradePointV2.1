using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SunttelTradePointB.Shared.IA
{
    public class IASpeechRecognition
    {
        public string Command { get; set; } = "";
        public string Page { get; set; } = "";
        public string Id { get; set; } = "";
        public string Code { get; set; } = "";
        public string Filter { get; set; } = "";
    }
}
