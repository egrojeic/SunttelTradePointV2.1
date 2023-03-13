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
            None = -1,
            Text = 0,
            Numeric = 1,
            Date = 2,
            FromSelector = 3,
            FromSelectorWithNew = 4,
            Details = 5,
            Address = 6,
            LogAuditory = 7,
            Groups = 8,
            
        }
        public EditModes EditMode { get; set; }

        public ObjectPropertyAttributes(EditModes editMode)
        {

            EditMode = editMode;    

        }
    }
}
