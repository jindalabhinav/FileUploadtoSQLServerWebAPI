using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FileUploadWebApp
{
    public static class ServiceConstants
    {
        public const string ConnectionString = "DapperPOC:ConnectionString";

        public const string ImageUploadSP = "uspInsertImage";

        public const string GetNamesSP = "uspGetImageNames";

        public const string GetNamesAscSP = "uspGetImageNamesAsc";
        public const string GetNamesDesSP = "uspGetImageNamesDes";
        public const string SearchImagesSP = "uspSearchImageNames";
    }
}
