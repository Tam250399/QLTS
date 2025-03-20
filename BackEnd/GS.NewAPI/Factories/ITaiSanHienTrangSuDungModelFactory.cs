namespace GS.NewAPI.Factories
{
    public interface ITaiSanHienTrangSuDungModelFactory
    {
        void InsertHienTrangSuDungForBienDong(decimal bienDongId, decimal taiSanId, string jsonHienTrang);
    }
}
