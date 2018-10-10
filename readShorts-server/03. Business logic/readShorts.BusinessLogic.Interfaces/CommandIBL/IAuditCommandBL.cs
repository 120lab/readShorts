namespace readShorts.BusinessLogic.Interfaces
{
    public interface IAuditCommandBL :IBaseBL
    {
        void Add(Models.dbo.Audit audit);
    }
}
