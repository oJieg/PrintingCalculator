using Microsoft.EntityFrameworkCore;
using printing_calculator.DataBase;
using printing_calculator.DataBase.setting;

namespace printing_calculator.Models
{
    public interface ISettingCalculation
    {
        public PaperCatalog[] PaperCatalog { get;  }
        public SizePaper[] PaperSizes { get;  }
        public Lamination[] Laminations { get;  }
        public PrintingMachineSetting PrintingsMachine { get;  }
        public PosMachinesSetting[] PosMachines { get;  }
        public CommonToAllMarkup[] CommonToAllMarkups { get;  }
        public SpringBrochureSetting SpringBrochureSetting { get; }
    }
}
