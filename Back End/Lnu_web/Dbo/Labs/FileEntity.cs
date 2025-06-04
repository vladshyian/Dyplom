namespace Lnu_web.Dbo.Labs
{
    public class FileEntity
    {
        public Guid Id { get; set; }
        public string UserId { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public DateTime UploadDate { get; set; }
    }
}
