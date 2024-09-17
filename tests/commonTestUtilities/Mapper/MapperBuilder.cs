using AutoMapper;
using FinanceFlow.Application.AutoMapper;

namespace commonTestUtilities.Mapper;

public static class MapperBuilder
{
    public static IMapper Build()
    {
        var mapper = new MapperConfiguration(config =>
        {
            config.AddProfile(new AutoMapping());
        });

        return mapper.CreateMapper();
    }
}
