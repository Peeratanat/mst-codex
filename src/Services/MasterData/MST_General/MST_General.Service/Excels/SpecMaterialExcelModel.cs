
using DateTimeExtensions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Commission.Services.Excels
{
    public class SpecMaterialExcelModel
    {
        public const int _collectionName = 0;
        public const int _projectNoIndex = 1;
        public const int _model = 2;
        public const int _materialGroup = 3;
        public const int _materialItem = 4;

        public const int _materialItemEN = 5;
        public const int _materialItemDescription = 6;
        public const int _materialItemDescriptionEN = 7;
        public const int _sheetName = 8;



        public string CollectionName { get; set; }
        public string ProjectNo { get; set; }
        public string MaterialGroup { get; set; }
        public string MaterialItem { get; set; }
        public string MaterialItemDescription { get; set; }
        public string MaterialItemEN { get; set; }
        public string MaterialItemDescriptionEN { get; set; }
        public string Model { get; set; }
        public string SheetName { get; set; }


        public static SpecMaterialExcelModel CreateFromDataRow(DataRow dr)
        {
            var result = new SpecMaterialExcelModel();

            result.CollectionName = dr[_collectionName]?.ToString();
            result.ProjectNo = dr[_projectNoIndex]?.ToString();
            result.MaterialGroup = dr[_materialGroup]?.ToString();
            result.MaterialItem = dr[_materialItem]?.ToString();
            result.MaterialItemDescription = dr[_materialItemDescription]?.ToString();
            result.MaterialItemEN = dr[_materialItemEN]?.ToString();
            result.MaterialItemDescriptionEN = dr[_materialItemDescriptionEN]?.ToString();
            result.Model = dr[_model]?.ToString();
            result.SheetName = dr[_sheetName]?.ToString();

            return result;
        }

    }
}
