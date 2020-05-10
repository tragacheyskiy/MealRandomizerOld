using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace MealRandomizer.Service
{
    public static class DeviceCulture
    {
        private static readonly CultureInfo russian = new CultureInfo("ru-RU");
        private static readonly CultureInfo deviceCulture = CultureInfo.CurrentCulture;

        public static bool IsRussian => russian.LCID == deviceCulture.LCID;
    }
}
