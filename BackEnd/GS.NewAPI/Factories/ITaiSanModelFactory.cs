namespace GS.NewAPI.Factories
{
    public interface ITaiSanModelFactory
    {
        bool CheckTenTaiSan(string ten, decimal? id = 0, decimal? donViId = 0);
    }
}
