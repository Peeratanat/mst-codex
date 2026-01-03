using System;
using System.Collections.Generic;
using System.Text;

namespace Base.DTOs.CMS
{
    public class SpecMaterialExcelDTO
    {
        public int Success { get; set; }
        public int Error { get; set; }
        public List<string> ErrorMessages { get; set; }
        public List<ImportError> ErrorData { get; set; }
    }

    public class ImportError {
        public Guid ProjectID { get; set; }
        public string SpecMaterialCollectionName { get; set; }
        public string SpecMaterialGroup { get; set; }
        public string SpecMaterialType { get; set; }
        public string SpecMaterialItem { get; set; }
        public string ErrorDescription { get; set; }
        public string SpecMaterialTypeEN { get; set; }
        public string SpecMaterialItemEN { get; set; }
        public string ModelName { get; set; }
    }
}
