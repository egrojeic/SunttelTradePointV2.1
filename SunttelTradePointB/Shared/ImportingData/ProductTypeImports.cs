using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace SunttelTradePointB.Shared.ImportingData
{
    public class ProductTypeImports
    {
        public string LegacyId { get; set; }
        public string LegacyIdObjectID { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public int HasPackingRecipe { get; set; }

        public int HasProductionSpecs { get; set; }
    }

    public class ProcessImports
    {

        public int IDTipos { get; set; }
        public string LegacyId { get; set; }
        public string LegacyIdObjectID { get; set; }
        public string Nombre { get; set; }
        public string Codigo { get; set; }
        public double RegularPrice { get; set; }
        public double HoliDayPrice { get; set; }
        public int StemsInit { get; set; }
        public int StemsFin { get; set; }
        public int IDTiposProcesosTransformLegacyId { get; set; }
        public string IDTiposProcesosTransformLegacyIdObjectID { get; set; }
        public string ProcesosTransformType { get; set; }
        public string IDChargeableUnitsTypeLegacyId { get; set; }
        public string IDChargeableUnitsTypeLegacyIdObjectID { get; set; }
        public string ChargeableUnitsType { get; set; }


    }


    public class CharacteristicsImports
    {

        public int IDTipos { get; set; }
        public string LegacyId { get; set; }
        public string LegacyIdObjectID { get; set; }
        public string Nombre { get; set; }


    }


    public class CharacteristicsPossibleValuesImports
    {
        public string LegacyId { get; set; }
        public string LegacyIdObjectID { get; set; }
        public int IDCharacteristics { get; set; }
        public string Nombre { get; set; }
        public string AdditionalDesc { get; set; }

    }

    public class RecipeModifierImports {

        public int idType { get; set; }
        public string LegacyId { get; set; }
        public string LegacyIdObjectID { get; set; }
        public string Nombre { get; set; }

        public string AdditionalDesc { get; set; }

    }


    public class RecipeModifierImportsPossibleValues
    {

        public string LegacyId { get; set; }
        public string IDModifier { get; set; }
        public string LegacyIdObjectID { get; set; }
        public string Nombre { get; set; }

    }


    public class QualityParameter
    {
        public int idType { get; set; }
        public string LegacyId { get; set; }
        public string LegacyIdObjectID { get; set; }
        public string Nombre { get; set; }

        public string DefaultValue { get; set; }
        public string AdditionalDesc { get; set; }

    }


}