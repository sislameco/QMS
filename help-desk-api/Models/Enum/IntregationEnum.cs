using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Enum
{
    public enum EnumExternalServiceModules
    {
        QsmartAPI = 1,
        HRAPI = 2
    }
    public enum CustomFieldType
    {
        TextInput = 1,
        TextArea = 2,
        Number = 3,
        Date = 4,
        DropdownList = 5,
        FileUpload = 6
    }
    public enum EnumDataSource
    {
        Department = 1,
        User = 2,
        Project = 3,
        Customer = 4,
        Scheme = 5,
        RootCause = 6,
        Resolution = 7
    }
    public enum EnumRootResolutionType
    {
        RootCause = 1,
        Resolution = 2
    }
}
