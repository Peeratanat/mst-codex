using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Text;
using Dapper;

namespace Database.Models.DbQueries.PRM
{
    public class sqlDupPreSalePromotion
    {
        

        public static string QueryString = @"        
                    SELECT 
                    'MasterPreSalePromoItemID' = MPS.ID,
                    'MasterPreSalePromoID' = MPS.MasterPreSalePromotionID,
                    'MainPromotionItemID' = MPS.MainPromotionItemID,
                    'PromoMaterialItemID' = MPS.PromotionMaterialItemID
                    FROM PRM.MasterPreSalePromotionItem MPS
                    WHERE 1 = 1
                    AND MPS.ID NOT IN (
                                          SELECT MPS2.MainPromotionItemID
                                         FROM PRM.MasterPreSalePromotionItem MPS2
                                         WHERE MPS2.MasterPreSalePromotionID = MPS.MasterPreSalePromotionID
                                               AND MPS2.MainPromotionItemID IS NOT NULL
                                     )
                    AND
                    (
                        MPS.MainPromotionItemID NOT IN (
                                                          SELECT MPS3.MainPromotionItemID
                                                          FROM PRM.MasterPreSalePromotionItem MPS3
                                                          WHERE MPS3.MasterPreSalePromotionID = MPS.MasterPreSalePromotionID
                                                                AND MPS3.MainPromotionItemID IS NOT NULL
                                                      )
                        OR MPS.MainPromotionItemID IS NULL
                    )
                    AND MPS.MasterPreSalePromotionID = @masterPreSalePromotionID
                    AND MPS.IsDeleted = 0
                    AND MPS.PromotionMaterialItemID NOT IN (
                                                              SELECT MPS4.PromotionMaterialItemID
                                                              FROM PRM.MasterPreSalePromotionItem MPS4
                                                              WHERE 1 = 1
                                                                    AND
                                                                    (
                                                                        MPS4.ID IN (
                                                                                      SELECT MPS5.MainPromotionItemID
                                                                                      FROM PRM.MasterPreSalePromotionItem MPS5
                                                                                      WHERE MPS5.MasterPreSalePromotionID = MPS4.MasterPreSalePromotionID
                                                                                            AND MPS5.MainPromotionItemID IS NOT NULL
                                                                                  )
                                                                        OR (MPS4.MainPromotionItemID IN (
                                                                                                           SELECT MPS6.ID
                                                                                                           FROM PRM.MasterPreSalePromotionItem MPS6
                                                                                                           WHERE MPS6.MasterPreSalePromotionID = MPS4.MasterPreSalePromotionID
                                                                                                       )
                                                                           )
                                                                    )
                                                                    AND MPS4.MasterPreSalePromotionID = MPS.MasterPreSalePromotionID
                                                                    AND MPS4.IsDeleted = 0
                                                          )";
         
        public static DynamicParameters QueryFilter(ref string QueryString , Guid masterPreSalePromotionID)
        {
            DynamicParameters ParamList = new DynamicParameters();
            string MasterPreSalePromotionID = masterPreSalePromotionID.ToString();
            if (!string.IsNullOrEmpty(MasterPreSalePromotionID))
            {
                ParamList.Add("masterPreSalePromotionID", MasterPreSalePromotionID);
                QueryString += " AND MPS.MasterPreSalePromotionID = @masterPreSalePromotionID";
            }

            return ParamList;
        }

        public class QueryResult
        {
            public Guid? MasterPreSalePromoID { get; set; }
            public Guid? MasterPreSalePromoItemID { get; set; }
            public Guid? MainPromotionItemID { get; set; }
            public Guid? PromoMaterialItemID { get; set; }

        }
    }
}
