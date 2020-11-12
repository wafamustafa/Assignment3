using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Assignment3_N01450753_WafaMustafa_5101B.Areas.HelpPage.ModelDescriptions
{
    public class EnumTypeModelDescription : ModelDescription
    {
        public EnumTypeModelDescription()
        {
            Values = new Collection<EnumValueDescription>();
        }

        public Collection<EnumValueDescription> Values { get; private set; }
    }
}