namespace Base.DTOs.SystemMessage
{
    public static class CommonMessage
    {
        public const string Success = "Success";
        public const string Failure = "Failure!";
        public const string NotFound = "ไม่พบข้อมูล!";
        public const string Registered = "Registered";
        public const string Unauthorized = "Unauthorized!";
        public const string NotPermission = "คุณไม่ได้รับสิทธิ์ กรุณาติดต่อ Admin space เพื่อขอสิทธิ์!";
        public const string Error = "Error!";
        public const string UpdateFail = "Update ข้อมูลไม่สำเร็จ!";
        public const string DeleteFail = "Delete ข้อมูลไม่สำเร็จ!";
        public const string DontMoveToLevel = "ไม่สามารถ Move file ไป Folder นี้ได้!";
    }

    public static class ErrMessage
    {
        public const string RequestParameter = "Request parameter!";
        public const string InvalidItemType = "Invalid item type!";
        public const string FolderNameDuplicate = "ไม่สามารถใช้ Folder ชื่อซ้ำกันได้!";
        public const string InvalidParentItem = "Invalid parent item!";
        public const string UnauthorizedUserInfo = "ไม่พบข้อมูล User ในระบบ Authorization กรุณาติดต่อ IT Support!";
        public const string UnableAddFileUnderFile = "ไม่สามารถ Add file ซ้อนกันได้!";
        public const string FileNameDuplicate = "ไม่สามารถใช้ File ชื่อซ้ำกันได้!";
        public const string UploadFileFail = "Upload file ไม่สำเร็จ!";
        public const string ReqRefPermissionID = "Rquest reference permission identity!";
        public const string RequestSpaceID = "Request space identity!";
        public const string NotAddFileThisLevel = "ไม่สามารถ Add file ใน Folder นี้ได้!";
        public const string NotAddFolderThisLevel = "ไม่สามารถ Add folder เพิ่มได้!";
        public const string FileNotFound = "ไม่พบ File!";
        public const string FolderNotFound = "ไม่พบ Folder!";
        public const string NotMoveToOtherSpace = "ไม่สามารถ Move file ไป Space อื่นได้!";
        public const string CopyPermissionFail = "คัดลอกสิทธิ์ไม่สำเร็จ!";
        public const string CantCopyPermissionCrossSpace = "ไม่สามารถคัดลอกสิทธิ์ข้าม Space ได้!";
        public const string RequestItemID = "Request item identity!";
        public const string SpaceIsDuplicate = "ไม่สามารถใช้ Space ชื่อซ้ำกันได้!";
        public const string SpaceInvalid = "Space ไม่ถูกต้อง!";
        public const string CleanPermissionChildGenerations = "Update สิทธิ์ Folder ย่อย ไม่สำเร็จ!";
        public const string EmailNotFound = "ไม่พบ Email ของ User!";
    }

    public static class WarningMessage
    {
        public const string FileNotFound = "File หรือ Folder โดน Delete ไปแล้ว หรือคุณไม่ได้รับสิทธิ์ให้เข้าถึงข้อมูล กรุณาติดต่อ Admin Space!";
    }
}
