﻿using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace GuiAutomationFramework.Framework.Enums
{
    /// <summary>
    /// Role types to test different privilegies.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum Roles
    {

        // TODO: We should have different login Roles (Teacher / Student / Admin.. etc ).

        Teacher,
        Admin,
        Student,
        None
    }
}
