using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Text;
using Dapper;

namespace Database.Models.DbQueries.PRM
{
    public class sqlDupSalePromotion
    {
        

        public static string QueryString = @"        
                    SELECT 
                    'MasterSalePromoItemID' = MP.ID,
                    'MasterSalePromoID' = MP.MasterSalePromotionID,
                    'MainPromotionItemID' = MP.MainPromotionItemID,
                    'PromoMaterialItemID' = MP.PromotionMaterialItemID
                    FROM PRM.MasterSalePromotionItem MP
                    WHERE 1 = 1
                    AND MP.ID NOT IN (
                                          SELECT MP2.MainPromotionItemID
                                         FROM PRM.MasterSalePromotionItem MP2
                                         WHERE MP2.MasterSalePromotionID = MP.MasterSalePromotionID
                                               AND MP2.MainPromotionItemID IS NOT NULL
                                     )
                    AND
                    (
                        MP.MainPromotionItemID NOT IN (
                                                          SELECT MP3.MainPromotionItemID
                                                          FROM PRM.MasterSalePromotionItem MP3
                                                          WHERE MP3.MasterSalePromotionID = MP.MasterSalePromotionID
                                                                AND MP3.MainPromotionItemID IS NOT NULL
                                                      )
                        OR MP.MainPromotionItemID IS NULL
                    )
                    AND MP.MasterSalePromotionID = @masterSalePromotionID
                    AND MP.IsDeleted = 0
                    AND MP.PromotionMaterialItemID NOT IN (
                                                              SELECT MP4.PromotionMaterialItemID
                                                              FROM PRM.MasterSalePromotionItem MP4
                                                              WHERE 1 = 1
                                                                    AND
                                                                    (
                                                                        MP4.ID IN (
                                                                                      SELECT MP5.MainPromotionItemID
                                                                                      FROM PRM.MasterSalePromotionItem MP5
                                                                                      WHERE MP5.MasterSalePromotionID = MP4.MasterSalePromotionID
                                                                                            AND MP5.MainPromotionItemID IS NOT NULL
                                                                                  )
                                                                        OR (MP4.MainPromotionItemID IN (
                                                                                                           SELECT MP6.ID
                                                                                                           FROM PRM.MasterSalePromotionItem MP6
                                                                                                           WHERE MP6.MasterSalePromotionID = MP4.MasterSalePromotionID
                                                                                                       )
                                                                           )
                                                                    )
                                                                    AND MP4.MasterSalePromotionID = MP.MasterSalePromotionID
                                                                    AND MP4.IsDeleted = 0
                                                          )";
         
        public static DynamicParameters QueryFilter(ref string QueryString , Guid masterSalePromotionID)
        {
            var ParamList = new DynamicParameters();
            string MasterSalePromotionID = masterSalePromotionID.ToString();
            if (!string.IsNullOrEmpty(MasterSalePromotionID))
            {
                ParamList.Add("masterSalePromotionID", MasterSalePromotionID);
                QueryString += " AND MP.MasterSalePromotionID = @masterSalePromotionID";
            }

            return ParamList;
        }

        public class QueryResult
        {
            public Guid? MasterSalePromoID { get; set; }
            public Guid? MasterSalePromoItemID { get; set; }
            public Guid? MainPromotionItemID { get; set; }
            public Guid? PromoMaterialItemID { get; set; }

        }
    }
}
