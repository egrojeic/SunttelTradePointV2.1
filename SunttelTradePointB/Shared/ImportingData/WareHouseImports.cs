using SunttelTradePointB.Shared.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SunttelTradePointB.Shared.ImportingData
{
    public class WareHouseImports
    {

        public int ID {get;set;}
        public string ConceptObjectId { get; set; }
        public string Nombre {get;set;}
        public int IDBodegasPadre {get;set;}
        public int IDClientesTransformadores {get;set;}
        public int FlagExterna {get;set;}
        public int FlagTransformacion {get;set;}
        public int FlagSimulacionScan {get;set;}
        public int FlagDescuentaInvetario {get;set;}
        public int FlagMovePallets {get;set;}
        public string Address1 {get;set;}
        public string Address2 {get;set;}
        public int IDPaises {get;set;}
        public int IDEstados {get;set;}
        public int IDCiudades {get;set;}
        public string Zip {get;set;}
        public string Contact { get; set; }

        public string EntityTransform { get; set; }


    }
}
