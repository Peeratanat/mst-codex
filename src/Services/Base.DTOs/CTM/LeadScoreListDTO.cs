using System;
using System.Collections.Generic;
using System.Text;
using models = Database.Models;

namespace Base.DTOs.CTM
{
    public class LeadScoreListDTO
    {
        /// <summary>
        /// ID ของ Score
        /// </summary>
        public Guid? Id { get; set; }
        /// <summary>
        /// ID ของ Lead
        /// </summary>
        public Guid? LeadID { get; set; }
        /// <summary>
        /// ID ของ Lead scoring type (ชนิดของการให้คะแนน)
        /// </summary>
        public Guid? LeadScoringTypeID { get; set; }
        /// <summary>
        /// ลำดับ
        /// </summary>
        public int Order { get; set; }
        /// <summary>
        /// หัวข้อของชนิดของการให้คะแนน
        /// </summary>
        public string Topic { get; set; }
        /// <summary>
        /// คะแนนที่จะได้
        /// </summary>
        public double Score { get; set; }
        /// <summary>
        /// ได้คะแนนนี้หรือไม่ (true = ได้คะแนนหัวข้อนี้/ false = ไม่ได้คะแนน)
        /// </summary>
        public bool IsGetScore { get; set; }

        public static LeadScoreListDTO CreateFromModel(models.CTM.LeadScoring model)
        {
            if(model != null)
            {
                var result = new LeadScoreListDTO()
                {
                    Id = model.ID,
                    LeadID = model.LeadID,
                    LeadScoringTypeID = model.LeadScoringTypeID,
                    Order = model.LeadScoringType.Order,
                    Topic = model.LeadScoringType.Topic,
                    Score = model.Score,
                    IsGetScore = model.IsGetScore
                };

                return result;
            }
            else
            {
                return null;
            }
        }
    }
}
