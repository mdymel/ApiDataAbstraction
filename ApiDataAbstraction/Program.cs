using ApiDataAbstraction.Model;

namespace ApiDataAbstraction
{
    class Program
    {
        static void Main(string[] args)
        {
            var properties = ProductProperty.CreateDefaultConfiguration();
            var configuration = CarPropertyMapper.Map<CarConfiguration>(properties);
        }
    }
}
