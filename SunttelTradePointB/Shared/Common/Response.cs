using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SunttelTradePointB.Shared.Common
{
    public class Response<T>
    {
        public bool Error { get; set; } = false;
        public string Message { get; set; }
        public T data { get; set; }

        public Response(bool _error, string _mensajeError, T _info)
        {
            this.Error = _error;
            this.Message = _mensajeError;
            this.data = _info;

        }
    }
}
