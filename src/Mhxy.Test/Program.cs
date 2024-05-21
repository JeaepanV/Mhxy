// See https://aka.ms/new-console-template for more information
// Console.WriteLine("Hello, World!");

using Mhxy.App;

Dmsoft dm;

if (DmsoftRegister.RegisterDmSoftDllA())
{
    dm = new Dmsoft();
    var regCode = dm.Reg(DmsoftConfig.RegCode, DmsoftConfig.VerInfo);
    Console.Write($"大漠收费注册: {regCode}");
}
