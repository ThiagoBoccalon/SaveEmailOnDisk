namespace SaveEmailOnLocalDisk.Services
{
    public interface ISaveMessageService
    {
        Task SaveEmailAsync(string emailFrom, string emailTo, string subject, string body);
    }
}
