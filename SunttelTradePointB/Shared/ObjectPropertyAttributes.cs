using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SunttelTradePointB.Shared
{
    public class ObjectPropertyAttributes: System.Attribute
    {

       public enum EditModes
        {
            Text = 0,
            Numeric = 1,
            Date = 2,
            FromSelector = 3,
            FromSelectorWithNew = 4,
            Details = 5,
            Address = 6
        }
        public EditModes EditMode { get; set; }

        public ObjectPropertyAttributes(EditModes editMode)
        {

            EditMode = editMode;    

        }
    }
}
