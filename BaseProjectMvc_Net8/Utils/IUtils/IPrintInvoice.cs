namespace BaseProjectMvc_Net8.Utils.IUtils
{
    public interface IPrintInvoice
    {
        Task<byte[]> PDF(int id);
    }
}
