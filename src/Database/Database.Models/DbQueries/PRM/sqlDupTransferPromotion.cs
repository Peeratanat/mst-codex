using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Text;
using Dapper;

namespace Database.Models.DbQueries.PRM
{
    public class sqlDupTransferPromotion
    {
        

        public static string QueryString = @"        
                            
                    SELECT 
                    'MasterTransferPromoItemID' = MTP.ID,
                    'MasterTransferPromoID' = MTP.MasterTransferPromotionID,
                    'MainPromotionItemID' = MTP.MainPromotionItemID,
                    'PromoMaterialItemID' = MTP.PromotionMaterialItemID
                    FROM PRM.MasterTransferPromotionItem MTP
                    WHERE 1 = 1
                    AND MTP.ID NOT IN (
                                          SELECT MTP2.MainPromotionItemID
                                         FROM PRM.MasterTransferPromotionItem MTP2
                                         WHERE MTP2.MasterTransferPromotionID = MTP.MasterTransferPromotionID
                                               AND MTP2.MainPromotionItemID IS NOT NULL
                                     )
                    AND
                    (
                        MTP.MainPromotionItemID NOT IN (
                                                          SELECT MTP3.MainPromotionItemID
                                                          FROM PRM.MasterTransferPromotionItem MTP3
                                                          WHERE MTP3.MasterTransferPromotionID = MTP.MasterTransferPromotionID
                                                                AND MTP3.MainPromotionItemID IS NOT NULL
                                                      )
                        OR MTP.MainPromotionItemID IS NULL
                    )
                    AND MTP.IsDeleted = 0
                    AND MTP.PromotionMaterialItemID NOT IN (
                                                              SELECT MTP4.PromotionMaterialItemID
                                                              FROM PRM.MasterTransferPromotionItem MTP4
                                                              WHERE 1 = 1
                                                                    AND
                                                                    (
                                                                        MTP4.ID IN (
                                                                                      SELECT MTP5.MainPromotionItemID
                                                                                      FROM PRM.MasterTransferPromotionItem MTP5
                                                                                      WHERE MTP5.MasterTransferPromotionID = MTP4.MasterTransferPromotionID
                                                                                            AND MTP5.MainPromotionItemID IS NOT NULL
                                                                                  )
                                                                        OR (MTP4.MainPromotionItemID IN (
                                                                                                           SELECT MTP6.ID
                                                                                                           FROM PRM.MasterTransferPromotionItem MTP6
                                                                                                           WHERE MTP6.MasterTransferPromotionID = MTP4.MasterTransferPromotionID
                                                                                                       )
                                                                           )
                                                                    )
                                                                    AND MTP4.MasterTransferPromotionID = MTP.MasterTransferPromotionID
                                                                    AND MTP4.IsDeleted = 0
                                                          )";
         
        public static DynamicParameters QueryFilter(ref string QueryString , Guid masterTransferPromotionID)
        {
            var ParamList = new DynamicParameters();
            string MasterTransferPromotionID = masterTransferPromotionID.ToString();
            if (!string.IsNullOrEmpty(MasterTransferPromotionID))
            {
                ParamList.Add("masterTransferPromotionID", MasterTransferPromotionID);
                QueryString += " AND MTP.MasterTransferPromotionID = @masterTransferPromotionID";
            }

            return ParamList;
        }

        public class QueryResult
        {
            public Guid? MasterTransferPromoID { get; set; }
            public Guid? MasterTransferPromoItemID { get; set; }
            public Guid? MainPromotionItemID { get; set; }
            public Guid? PromoMaterialItemID { get; set; }

        }
    }
}
