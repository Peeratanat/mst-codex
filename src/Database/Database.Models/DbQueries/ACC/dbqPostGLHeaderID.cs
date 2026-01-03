using Base.DbQueries;
using System;

namespace Database.Models.DbQueries.ACC
{
    public class dbqPostGLHeaderID 
    {
        public Guid? PostGLHeaderID { get; set; }// sal.TransferID 
        public string MsgError { get; set; }
    }
}
