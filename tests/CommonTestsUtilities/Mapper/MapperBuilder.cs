using AutoMapper;
using CoBudget.Application.AutoMapper;

namespace CommonTestsUtilities.Mapper;
public class MapperBuilder
{
    public static IMapper Build()
    {
        var mapper = new MapperConfiguration(config =>
        {
            config.AddProfile(new AutoMapping());
        }, null);

        return mapper.CreateMapper();
    }
}
